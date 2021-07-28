using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cliente
{
    public partial class frmCliente : Form
    {
        //Creación de la comunicación TCP
        public static ComunicacionTCP conexionTcp = new ComunicacionTCP();
        //Constante para dirección y puerto del servidor
        public static string IPADDRESS = "127.0.0.1";
        public const int PORT = 8081;
        //Variables auxiliares
        public string direccion, puerto;
        public int indicador = 0, idmensaje = 0;

        public frmCliente()
        {
            InitializeComponent();
        }

        private void ClienteForm_Load(object sender, EventArgs e)
        {
            //Establecimiento de la conexión del cliente al servidor
            conexionTcp.OnDataRecieved += MensajeRecibido;

            if (!conexionTcp.Connectar(IPADDRESS, PORT))
            {
                MessageBox.Show("Error conectando con el servidor!");
                return;
            }
        }

        //Recepción del mensaje del servidor 
        private void MensajeRecibido(string datos)
        {
            var pedido = new Pedido(datos);
            string solicitud = pedido.Solicitud;
            if (solicitud == "SALVOCONDUCTO")
            {
                //Presentación de las respuestas del servidor a la solicitud del salvo conducto
                string contenido = pedido.Datos;
                List<string> valores = Contenido.Deserializar(contenido);
                Invoke(new Action(() => txtIndicador.Text = valores[1].ToString()));
                Invoke(new Action(() => txtTipoRespuesta.Text = string.Format(valores[0])));
                Invoke(new Action(() => txtRespuesta.Text = null));
            }
            if (solicitud == "RESPUESTA FECHA")
            {
                //Presentación de las respuestas del servidor a la solicitud de la consulta de fecha y hora
                string contenido = pedido.Datos;
                List<string> valores = Contenido.Deserializar(contenido);
                Invoke(new Action(() => txtTipoRespuesta.Text = string.Format(solicitud)));
                Invoke(new Action(() => txtRespuesta.Text = string.Format(valores[0] + " " + valores[1])));
                Invoke(new Action(() => txtIndicador.Text = valores[2].ToString()));
               ;
            }
        }

        private void ClienteForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        //Evento para enviar solicitud de Día y hora del Hoy no Circula
        private void btnVerificar_Click(object sender, EventArgs e)
        {
            if (conexionTcp.TcpClient.Connected)
            {
                indicador++; idmensaje++;
                //Obtención del la dirección y puerto del punto local
                string endponint = conexionTcp.TcpClient.Client.LocalEndPoint.ToString();
                int separador = endponint.IndexOf(":", StringComparison.Ordinal);
                direccion = endponint.Substring(0, separador);
                puerto = endponint.Substring(direccion.Length + 1);
                //Envio del mensaje al servidor
                var msgPack = new Pedido("fechas", string.Format("{0},{1},{2},{3},{4},{5}", txtPlaca.Text, 0,direccion,puerto,indicador,idmensaje));
                conexionTcp.EnviarPaquete(msgPack);
            }
        }

        //Evento para enviar solicitud de Salvoconducti
        private void btnSolicitar_Click(object sender, EventArgs e)
        {
            if (conexionTcp.TcpClient.Connected)
            {
                indicador++; idmensaje++;
                //Obtención del la dirección y puerto del punto local
                string endponint = conexionTcp.TcpClient.Client.LocalEndPoint.ToString();
                int separador = endponint.IndexOf(":", StringComparison.Ordinal);
                direccion = endponint.Substring(0, separador);
                puerto = endponint.Substring(direccion.Length + 1);
                //Envio del mensaje al servidor
                var msgPack = new Pedido("salvoconducto", string.Format("{0},{1},{2},{3},{4},{5}", txtPlaca.Text, 1, direccion, puerto, indicador, idmensaje));
                conexionTcp.EnviarPaquete(msgPack);
            }
        }
    }
}
