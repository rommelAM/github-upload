using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    // Clase para generar las respuestas al punto remoto
    public class Pedidos
    {
        public static Pedido LoginOk(string respuesta)
        {
            return new Pedido(respuesta);
        }
    }
}
