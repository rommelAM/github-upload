using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    //Clase para establecer la conexión por parte del servidor
    public class ComunicacionTCP
    {
        // Creación de cliente TCP
        public TcpClient TcpClient;
        // Objeto del tipo StreamReader para leer los mensajes
        public StreamReader StreamReader;
        // Objeto del tipo StreamWriter para escribir los mensajes
        public StreamWriter StreamWriter;
        // Hilo para lectura de mensajes del cliente 
        public Thread ReadThread;

        // Delegado portador de datos
        public delegate void DataCarrier(string data);
        // Evento del Data Carriero para la recepción de Datos
        public event DataCarrier OnDataRecieved;

        // Delegado y evento para notificar la desconexión 
        public delegate void DisconnectNotify();
        public event DisconnectNotify OnDisconnect;

        public delegate void ErrorCarrier(Exception e);
        public event ErrorCarrier OnError;

        // Constructor de la clase para establecimiento de la comunicación con el cliente
        public ComunicacionTCP(TcpClient client)
        {
            // Obtención del Stream del cliente
            var ns = client.GetStream();
            StreamReader = new StreamReader(ns); // Instanciamiento del StreamReader
            StreamWriter = new StreamWriter(ns); // Instanciamiento del StreamWriter
            TcpClient = client; // Instanciamiento del cliente
        }

        //Metodo para escribir mensajes por parte del cliente
        private void EscribirMsj(string mensaje)
        {
            try
            {   //Escritura del mensaje a enviar al servidor
                StreamWriter.Write(mensaje + "\0");
                StreamWriter.Flush();
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
