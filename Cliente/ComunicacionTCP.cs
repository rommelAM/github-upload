using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cliente
{
    //Clase para establecer la conexión por parte del cliente
    public class ComunicacionTCP
    {
        // Creación de cliente TCP
        public TcpClient TcpClient { get; private set; }
        // Objeto del tipo NetworkStream para la comucinación de mensajes
        public NetworkStream Stream { get; private set; }
        // Hilo para lectura de mensajes del servidor
        public Thread ReadThread { get; private set; }
        // Objeto del tipo StreamWriter para escribir los mensajes
        public StreamWriter Writer { get; private set; }

        // Delegado portador de datos
        public delegate void DataCarrier(string data);
        // Evento del Data Carriero para la recepción de Datos
        public event DataCarrier OnDataRecieved;

        // Delegado y evento para notificar la desconexión 
        public delegate void DisconnectNotify();
        public event DisconnectNotify OnDisconnect;

        public delegate void ErrorCarrier(Exception e);
        public event ErrorCarrier OnError;

        // Metodo de la conexión del cliente
        public bool Connectar(string direccionIp, int puerto)
        {
            try
            {   //Creación del TCP client
                TcpClient = new TcpClient();
                // Establecimiento de la conexión con el servidor
                TcpClient.Connect(IPAddress.Parse(direccionIp), puerto);
                // Obtencion de los datos del cliente
                Stream = TcpClient.GetStream();
                // Escritura de los datos desde el lado del cliente
                Writer = new StreamWriter(Stream);
                // Creación del hilo de escuha 
                ReadThread = new Thread(Escuchar);
                ReadThread.Start();
                return true;
            }
            // Excepción en caso de error en la conexión
            catch (Exception e)
            {
                if (OnError != null)
                    OnError(e);
                return false;
            }
        }

        private void Escuchar()
        {
            // Creación del lector de mensajes y la lista de caracteres del mensaje
            var lector = new StreamReader(Stream);
            var charBuffer = new List<int>();

            do
            {
                try
                {
                    if (lector.EndOfStream) //Fin de la lectura del mensaje
                        break;

                    int charCode = lector.Read(); //Lectura de la trama de datos
                    if (charCode == -1)
                        break;
                    if (charCode != 0)
                    {
                        charBuffer.Add(charCode); //Se añade cada elemento a la lista de caracteres
                        continue;
                    }
                    if (OnDataRecieved != null)
                    {
                        // Conversión de los indices de los caracteres en los respectivos caracteres
                        var chars = new char[charBuffer.Count];
                        for (int i = 0; i < charBuffer.Count; i++)
                        {
                            chars[i] = Convert.ToChar(charBuffer[i]);
                        }
                        // Se construye el mensaje con los caracteres 
                        var message = new string(chars);
                        OnDataRecieved(message); //Se lee el mensaje
                    }
                    charBuffer.Clear(); // Se limpia el buffer
                }

                // Excepción en caso de error de lectura
                catch (Exception e)
                {
                    if (OnError != null)
                        OnError(e);

                    break;
                }
            } while (true);
            if (OnDisconnect != null)
                OnDisconnect();
        }

        //Metodo para escribir mensajes por parte del cliente
        private void EscribirMsj(string mensaje)
        {
            try
            {
                //Escritura del mensaje a enviar al servidor
                Writer.Write(mensaje + "\0");
                Writer.Flush();
            }
            catch (Exception e)
            {
                if (OnError != null)
                    OnError(e);
            }
        }

        // Envio del pedido con el tipo de solicitud y el contenido del mensaje 
        public void EnviarPaquete(Pedido paquete)
        {
            EscribirMsj(paquete);
        }
    }
}
