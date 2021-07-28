using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Servidor
{
    public partial class frmServidor : Form
    {
        // Creacion del cliente TCP
        public delegate void ClientCarrier(ComunicacionTCP conexionTcp);
        public event ClientCarrier OnClientConnected;
        public event ClientCarrier OnClientDisconnected;
        // Establecimiento de la conexion mediante la clase Comunicacion TCP con la cual se aceptaran los datos
        public delegate void DataRecieved(ComunicacionTCP conexionTcp, string data);
        public event DataRecieved OnDataRecieved;
        // Creacion del TCP listener
        private TcpListener _tcpListener;
        private Thread _acceptThread;
        // Creacion de lista de clientes conectados con el servidor
        private List<ComunicacionTCP> connectedClients = new List<ComunicacionTCP>();
        // Variables para almacenamiento de la direccion IP y puerto del cliente 
        public string direccion;
        public string puerto;
        //Variables para almacenar datos de dias y horarios de Hoy no Circula
        public string dias;
        public string horario;
        
        //private int idrespuesta = 0; 
        private string respuesta;


        public frmServidor()

        {
            InitializeComponent();
        }

        private void ServidorForm_Load(object sender, EventArgs e)
        {
;
            // Al cargar el formulario servidor esta ya se encuentra preparado para 
            // aceptar la conexion y recibir mensajes por parte del clientes
            // así como cerrar la conexion
            OnDataRecieved += MensajeRecibido;
            OnClientConnected += ConexionRecibida;
            OnClientDisconnected += ConexionCerrada;

            EscucharClientes(IPAddress.Any, 8081);
        }

        // Función para recibir los mensajes por parte del cliente
        private void MensajeRecibido(ComunicacionTCP conexionTcp, string datos) // Como argumentos de entrada se tiene la conexión y el contenido del mensaje del cliente
        {
            var pedido = new Pedido(datos); //Genearacion de los pedidos en base a los datos enviados por el cliente
            string solicitud = pedido.Solicitud; //Obtención de la solicitud
            if (solicitud == "fechas")//Respuesta para fechas
            {
                respuesta = "NO APLICA";
                string data = pedido.Datos; 
                //Obtención de los elementos del pedido
                List<string> valores = Contenido.Deserializar(data);
                UltimodigitoPlaca(valores[0]); //Obtención de la respuesta de fecha basado en el último digito de la placa
                Invoke(new Action(() => textBox1.Text = valores[0]));
                if (valores[1] == "0")
                    Invoke(new Action(() => textBox2.Text = "FECHA"));
                
                // Generación de la respuesta a enviar al cliente
                var msgPack = new Pedido("RESPUESTA FECHA",string.Format("{0},{1},{2}",dias,horario,valores[4]));
                conexionTcp.EnviarPaquete(msgPack);//Envio del mensaje al cliente
                //Presentación de datos del cliente en el formulario
                Invoke(new Action(() => textBox3.Text = string.Format("{0}", valores[2])));
                Invoke(new Action(() => textBox4.Text = string.Format("{0}", valores[3])));
                Invoke(new Action(() => txtIdIndicador.Text = string.Format("{0}", valores[4])));
                Invoke(new Action(() => txtIDMensaje.Text = string.Format("{0}", valores[5])));
                //Registrar información en la base de datos
                Conexion.llenarBase(Int32.Parse(valores[3]), valores[2], valores[0], solicitud, dias, horario, respuesta);
            }
            if (solicitud == "salvoconducto") //Respuesta para salvoconducto
            {
                string contenido = pedido.Datos;
                //Obtención de los elementos del pedido
                List<string> valores = Contenido.Deserializar(contenido);
                Invoke(new Action(() => textBox1.Text = valores[0]));
                bool resul = Salvoconducto();//Generación de la respuesta del salvoconducto basado en número randómico

                //Presentacion de datos del cliente en el formulario
                Invoke(new Action(() => textBox3.Text = string.Format("{0}", valores[2])));
                Invoke(new Action(() => textBox4.Text = string.Format("{0}", valores[3])));
                Invoke(new Action(() => txtIdIndicador.Text = string.Format("{0}", valores[4])));
                Invoke(new Action(() => txtIDMensaje.Text = string.Format("{0}", valores[5])));
                if (valores[1] == "1")
                    Invoke(new Action(() => textBox2.Text = "SALVOCONDUCTO"));
                if (resul == true)
                {
                    // Generación de la respuesta a enviar al cliente
                    respuesta = "OK";
                    var msgPack = new Pedido("SALVOCONDUCTO", string.Format("{0},{1}","OK", valores[4]));
                    conexionTcp.EnviarPaquete(msgPack);
                } else
                {
                    // Generación de la respuesta a enviar al cliente
                    respuesta = "NOK";
                    var msgPack = new Pedido("SALVOCONDUCTO", string.Format("{0},{1}", "NOK", valores[4])); ;
                    conexionTcp.EnviarPaquete(msgPack);
                }
                //Registrar información en la base de datos
                Conexion.llenarBase(Int32.Parse(valores[3]), valores[2], valores[0], solicitud, dias, horario, respuesta);
            }
        }

        //Metodo para antender a la conexiones de los clientes
        private void ConexionRecibida(ComunicacionTCP conexionTcp)
        {
            //Atención a los clientes mediante bloqueos
            lock (connectedClients)
                if (!connectedClients.Contains(conexionTcp))
                    connectedClients.Add(conexionTcp);//Se añaden los clientes a la lista de conexion
            Invoke(new Action(() => txtClientes.Text = string.Format("{0}", connectedClients.Count)));//Se muestran los clientes conectados

        }

        //Metodo que atiende la desconexión de los clientes
        private void ConexionCerrada(ComunicacionTCP conexionTcp)
        {
            //Atención a las desconexiones de los clientes mediante bloqueos
            lock (connectedClients)
                if (connectedClients.Contains(conexionTcp))
                {
                    //Se remueve al cliente de la lista de clientes conectados
                    int cliIndex = connectedClients.IndexOf(conexionTcp);
                    connectedClients.RemoveAt(cliIndex);
                }
            Invoke(new Action(() => label1.Text = string.Format("Clientes: {0}", connectedClients.Count)));
        }

        //Método para escuchar a los clientes
        private void EscucharClientes(IPAddress ipAddress, int port)
        {
            try
            {
                //Creación del TCPListener para atender a los clientes mediante su dirección IP y puerto
                _tcpListener = new TcpListener(ipAddress, port);
                _tcpListener.Start();
                //Creación de hilo de escucha para aceptar a los clientes
                _acceptThread = new Thread(AceptarClientes);
                _acceptThread.Start();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
            }
        }
        //Metodo para aceptar a los clientes
        private void AceptarClientes()
        {
            do
            {
                try
                {
                    //Aceptación de la conexión del cliente
                    var conexion = _tcpListener.AcceptTcpClient();
                    var srvClient = new ComunicacionTCP(conexion)
                    {
                        //Lectura de los datos recibidos
                        ReadThread = new Thread(LeerDatos)
                    };
                    srvClient.ReadThread.Start(srvClient);
                    //Comprobación de la conexión
                    if (OnClientConnected != null)
                        OnClientConnected(srvClient);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());
                }

            } while (true);
        }

        //Metodo para leer los datos del clietne
        private void LeerDatos(object client)
        {
            var cli = client as ComunicacionTCP;
            var charBuffer = new List<int>();

            do
            {
                try
                {
                    if (cli == null)
                        break;
                    if (cli.StreamReader.EndOfStream) //Fin de la lectura del mensaje
                        break;
                    int charCode = cli.StreamReader.Read(); //Lectura de la trama de datos 
                    if (charCode == -1)
                        break;
                    if (charCode != 0)
                    {
                        charBuffer.Add(charCode); //Se añade cada elemento a la lista de caracteres
                        continue;
                    }
                    if (OnDataRecieved != null)
                    {
                        // Se construye el mensaje con los caracteres 
                        var chars = new char[charBuffer.Count];
                        
                        // Conversión de los indices de los caracteres en los respectivos caracteres
                        for (int i = 0; i < charBuffer.Count; i++)
                        {
                            chars[i] = Convert.ToChar(charBuffer[i]);
                        }
                        //Convert the character array to a string
                        var message = new string(chars);

                        //llamada al evento
                        OnDataRecieved(cli, message);
                    }
                    charBuffer.Clear();//Limpieza del buffer
                }
                //Excepciones del tipo IO
                catch (IOException)
                {
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message.ToString());

                    break;
                }
            } while (true);

            if (OnClientDisconnected != null)
                OnClientDisconnected(cli);
        }

        //Cierre del formulario
        private void ServidorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //Generación de la respuesta del salvoconducto basado en número randómico
        public bool Salvoconducto()
        {
            int variable_random = new Random().Next(0,2);
            if (variable_random == 1)
            {
                return true;
            } else
            {
                return false;
            }
        }

        //Generación de la respuesta a fechas basado en el ultimo dígito de la placa
        public void UltimodigitoPlaca(string placa)
        {
            int longit = placa.Length;
            string ultimodig = placa;
            char ultimodig2 = ultimodig[longit-1];
            //Dias y horario usando un Switch y el último dígito de la placa
            Console.WriteLine(ultimodig2);
            switch (ultimodig2)
        {
            case '1':
                    dias = "Lunes y Viernes";
                    horario = "07:00 a 19:00";
                        break;
            case '2':
                    dias = "Lunes y Martes";
                    horario = "07:00 a 19:00";
                    ;
                        break;
            case '3':
                    dias = "Lunes y Martes";
                    horario = "07:00 a 19:00";
                    ;
                    break;   
            case '4':
                    dias = "Martes y Miercoles";
                    horario = "07:00 a 19:00";
                    ;
                        break;
            case '5':
                    dias = "Martes  y Miercoles";
                    horario = "07:00 a 19:00";
                    ;
                    break;
            case '6':
                    dias = "Miercoles y Jueves";
                    horario = "07:00 a 19:00";
                    ;
                    break;
            case '7':
                    dias = "Miercoles y Jueves";
                    horario = "07:00 a 19:00";
                    ;
                    break;
            case '8':
                    dias = "Jueves y Viernes";
                    horario = "07:00 a 19:00";
                    ;
                    break;
            case '9':
                    dias = "Jueves y Viernes";
                    horario = "07:00 a 19:00";
                    ;
                    break;
            case '0':
                    dias = "Lunes y Viernes";
                    horario = "07:00 a 19:00";
                    ;
                    break;
                default:
                    MessageBox.Show("Ingreso mal la placa");
                        break;
         }

        }
        //Actualización de DATA TABLE en la interfaz gráfica
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = Conexion.llenartabla();
        }
    }
}
