using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnosMaterias
{
    public class DatosUsuario
    {
        public String Usuario = " ";
        public String Contrasena = " ";
    }
    public class opInicioSesion
    {
        public Boolean bAllOk = false;
        public String sLastError = "";
        public MySqlConnection CrearConexion()
        {
            string cadenaConexion = $"DataSource=192.168.137.183; Port=3306; User Id=FlorGax; Password=081278; Database=alumnosMaterias; SSL Mode=None;";
            MySqlConnection conn = new MySqlConnection(cadenaConexion);
            conn.Open();
            return conn;
        }
        public bool ValInicioSesion(DatosUsuario datos)
        {
            try
            {
                string query = $"SELECT COUNT(*) FROM Sesion WHERE usuario = '{datos.Usuario}' and pass = '{datos.Contrasena}';";
                MySqlCommand cmd = new MySqlCommand(query, CrearConexion());
                int i = Convert.ToInt32(cmd.ExecuteScalar());
                if (i == 1)
                {
                    bAllOk = true;
                }
            }
            catch (Exception ex)
            {
                sLastError = ex.Message;
                bAllOk = false;
            }
            return bAllOk;
        }
    }
}
