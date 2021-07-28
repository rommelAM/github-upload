using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Servidor
{
    //Clase para la conexion con la Base de Datos del servidor donde se almacenará la información de los pedidos
    public class Conexion
    {
        //Establecimiento de la conexión con la base de datos
        public static SqlConnection Conectar()
        {
            //Establecimiento de la cadena de conexión
            SqlConnection cadena = new SqlConnection("server=localhost ; database=dbNoCircula ; integrated security = true");
            cadena.Open(); 
            return cadena;
        }

        // Metodo para insertar la información en la base de datos
        public static void llenarBase(int puerto, string direccion, string Placa, string tipo, string dias, string horario, string respuesta)
        {
            // Inserción de los datos 
            string insertar = "INSERT INTO tblPedido (Puerto,DireccionIp,Placa,TipoPedido,Dias,Horario,Reply_Salvo)VALUES(@Puerto,@DireccionIp,@Placa,@TipoPedido,@Dias,@Horario,@Reply_Salvo)";
            SqlCommand comando = new SqlCommand(insertar, Conectar());
            comando.Parameters.AddWithValue("@Puerto", puerto);
            comando.Parameters.AddWithValue("@DireccionIp", direccion);
            comando.Parameters.AddWithValue("@Placa", Placa);
            comando.Parameters.AddWithValue("@TipoPedido", tipo);
            comando.Parameters.AddWithValue("@Dias", dias);
            comando.Parameters.AddWithValue("@Horario", horario);
            comando.Parameters.AddWithValue("@Reply_Salvo", respuesta);
            comando.ExecuteNonQuery();
        }

        // Metodo para llenar los datos en el DataGrid de la interfaz gráfica
        public static DataTable llenartabla()
        {
            Conectar(); //Establecimiento de la conexión
            DataTable dt = new DataTable(); // Creacion de un DataTable
            string consulta = "SELECT * FROM tblPedido"; // Sentencia de seleccion de Datos
            SqlCommand comando2 = new SqlCommand(consulta, Conectar());
            SqlDataAdapter adaptador = new SqlDataAdapter(comando2);
            adaptador.Fill(dt); //Llenado de datos en la base
            return dt; //Retorno del DataTable
        }
    }
}