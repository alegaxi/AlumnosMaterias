using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnosMaterias.BaseDatos
{
    public class DatosGrupos
    {
       public Int32 Grupo = 0;
       public Int32 Materia = 0;
       public Int32 Alumno = 0;
    }
    public class opGrupo
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

        public bool ConsultarAlumno(String Nombre, DatosGrupos grupos)
        {
            try
            {
                string query = $"SELECT NumeroControl FROM alumnosMaterias.Alumno WHERE Nombre = '{Nombre}'";
                MySqlCommand command = new MySqlCommand(query, CrearConexion());

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    grupos.Alumno = Int32.Parse(reader["NumeroControl"].ToString());
                }
                bAllOk = true;
            }
            catch (Exception ex)
            {
                sLastError = ex.Message;
                bAllOk = false;
            }
            return bAllOk;
        }
        public bool ConsultarMateria(String Materia, DatosGrupos grupos)
        {
            try
            {
                string query = $"SELECT ID FROM alumnosMaterias.Materia WHERE Nombre = '{Materia}'";
                MySqlCommand command = new MySqlCommand(query, CrearConexion());
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    grupos.Materia = Int32.Parse(reader["ID"].ToString());
                }
                bAllOk = true;
            }
            catch (Exception ex)
            {
                sLastError = ex.Message;
                bAllOk = false;
            }
            return bAllOk;
        }
        public bool Insert(DatosGrupos datos)
        {
            try
            {
                string query = $"INSERT INTO Grupo (Grupo, Materia, Alumno)" +
                    $"VALUES({datos.Grupo}, {datos.Materia}, {datos.Alumno})";
                MySqlCommand command = new MySqlCommand(query, CrearConexion());
                command.ExecuteNonQuery();
                bAllOk = true;
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
