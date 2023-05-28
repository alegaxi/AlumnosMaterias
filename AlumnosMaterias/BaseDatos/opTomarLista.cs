using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnosMaterias.BaseDatos
{
    public class opTomarLista
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
        public bool Registrarlista(DateTime fecha, Int32 Grupo, String Materia, String NombreAlumno, Int32 NumeroControl, Int32 Asistencia)
        {
            try
            {
                string query = $"INSERT INTO Lista (Fecha, Grupo, Materia, NombreAlumno, NumeroControl, Asistencia) VALUES ('{fecha.ToString("yyyy/MM/dd")}', {Grupo}, '{Materia}', '{NombreAlumno}', {NumeroControl}, {Asistencia})";
                Console.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, CrearConexion());
                cmd.ExecuteNonQuery();
                bAllOk = true;
            }
            catch (Exception e)
            {
                sLastError = e.Message;
                bAllOk = false;
            }
            return bAllOk;

        }
    }
}
