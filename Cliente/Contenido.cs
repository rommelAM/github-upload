using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    // Clase para serialización y deserialización de mensajes
    public class Contenido
    {
        // Metodo para enlistar todos los datos a serializar 
        public static string Serializar(List<string> lista)
        {
            if (lista.Count == 0)
            {
                return null;
            }
            // Variable de conteo y separacion
            bool esElPrimero = true;
            var salida = new StringBuilder();

            foreach (var linea in lista)
            {
                // Separacion  del primer elemento que identifica la solicitud
                if (esElPrimero)
                {
                    salida.Append(linea);
                    esElPrimero = false;
                }
                else
                {
                    salida.Append(string.Format(",{0}", linea));
                }
            }
            //retorno de la salida con la solicitud identificada
            return salida.ToString();
        }

        // metodo para deserializar el mensaje
        public static List<string> Deserializar(string entrada)
        {
            // Creacion de una lista para elementos deserializados
            string str = entrada;
            var lista = new List<string>();

            if (string.IsNullOrEmpty(str))
            {
                return lista;
            }

            try
            {
                // Se añade cada elemento separado por una coma 
                foreach (string linea in entrada.Split(','))
                {
                    lista.Add(linea);
                }
            }
            catch (Exception)
            {
                return null;
            }
            // Retorno de la lista con los elementos del mensaje (protocolo de envio y respuesta)
            return lista;
        }
    }
}
