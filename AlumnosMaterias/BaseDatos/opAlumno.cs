using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlumnosMaterias.BaseDatos
{
    public class DatosAlumno
    {
        public Int32 NumeroControl = 0;
        public String Nombre = " ";
        public String Apellidos = " ";
        public String Telefono = " ";
        public String Carrera = " ";
        public Int32 iGrupo = 0;
    }
    public class opAlumno
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
        public bool Insert(DatosAlumno datos)
        {
            try
            {
                string query = $"INSERT INTO Alumno (NumeroControl, Nombre, Apellidos, Telefono, Carrera, Grupo)" +
                    $"VALUES({datos.NumeroControl}, '{datos.Nombre}', '{datos.Apellidos}', '{datos.Telefono}', '{datos.Carrera}', {datos.iGrupo})";
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
        public bool Update(DatosAlumno datos)
        {
            try
            {
                string query = "UPDATE Alumno " +
                                 $"SET Nombre = '{datos.Nombre}', " +
                                     $"Apellidos = '{datos.Apellidos}', " +
                                     $"Telefono = '{datos.Telefono}', " +
                                     $"Carrera = '{datos.Carrera}', " +
                                     $"Grupo = {datos.iGrupo}," +
                                 $"WHERE NumeroControl = {datos.NumeroControl} ";
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
        public bool Delete(Int32 NumeroControl)
        {
            try
            {
                string query = "DELETE FROM Alumno " +
                                 $"WHERE NumeroControl = {NumeroControl} ";
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
        public bool Read(String nombre, ref DatosAlumno datos)
        {
            try
            {
                string query = $"SELECT NumeroControl, Nombre, Apellidos, Telefono, Carrera, Grupo FROM Alumno WHERE Nombre = '{nombre}'";
                MySqlCommand cmd = new MySqlCommand(query, CrearConexion());

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    datos.NumeroControl = Int32.Parse(reader["NumeroControl"].ToString());
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.Apellidos = reader["Apellidos"].ToString();
                    datos.Telefono = reader["Telefono"].ToString();
                    datos.Carrera = reader["Carrera"].ToString();
                    datos.iGrupo = Int32.Parse(reader["Grupo"].ToString());
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
        public Int32 Folio()
        {
            Int32 iFolioSiguiente = 0;

            try
            {
                String sCmd = "SELECT MAX(NumeroControl) FROM Alumno";
                MySqlCommand cmd = new MySqlCommand(sCmd, CrearConexion());
                iFolioSiguiente = 1925010000 + (Convert.ToInt32(cmd.ExecuteScalar()) + 1);
            }
            catch (Exception ex)
            {
                sLastError = ex.Message;
            }
            return iFolioSiguiente;
        }
    }
}
