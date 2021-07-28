using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servidor
{
    public class Pedidos
    {
        // Clase para generar las respuestas al punto remoto
        public static Pedido LoginOk(string respuesta)
        {
            return new Pedido(respuesta);
        }
    }
}
