using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    // Clase Pedido para crear la solicitud por parte del cliente y separarla en el tipo de solicitud y el contenido del mensaje 
    public class Pedido
    {
        //Variables que dividen el pedido del cliente en el tipo de solicitud y los datos que envia
        public string Solicitud { get; set; }
        public string Datos { get; set; }

        // Constructor por defecto
        public Pedido()
        {

        }
        // Constructor de la clase
        public Pedido(string solicitud, string datos)
        {
            Solicitud = solicitud;
            Datos = datos;
        }

        // Constructor de la clase donde los datos se separan 
        public Pedido(string datos) 
        {
            // Separación de los datos en substrings
            int sepIndex = datos.IndexOf(":", StringComparison.Ordinal); 
            Solicitud = datos.Substring(0, sepIndex);
            Datos = datos.Substring(Solicitud.Length + 1);
        }

        //Serialización del mensaje a enviar 
        public string Serializar()
        {
            return string.Format("{0}:{1}", Solicitud, Datos);
        }

        // retorno de la serialización mediante un operador de conversion 
        public static implicit operator string(Pedido pedido)
        {
            return pedido.Serializar();
        }
    }
}
