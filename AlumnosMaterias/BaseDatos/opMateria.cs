using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace AlumnosMaterias.BaseDatos
{
    public class DatosMateria
    {
        public Int32 Id = 0;
        public String Nombre = " ";
        public Int32 Unidades = 0;
        public String Maestro = " ";
    }
    public class opMateria
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
        public bool Insert(DatosMateria datos)
        {
            try
            {
                string query = $"INSERT INTO Materia (ID, Nombre, Unidades, Maestro)" +
                    $"VALUES({datos.Id}, '{datos.Nombre}', {datos.Unidades}, '{datos.Maestro}')";
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
        public bool Update(DatosMateria datos)
        {
            try
            {
                string query = "UPDATE Materia " +
                                 $"SET Nombre = '{datos.Nombre}', " +
                                     $"Unidades = {datos.Unidades}, " +
                                     $"Maestro = '{datos.Maestro}', " +
                                 $"WHERE ID = {datos.Id} ";
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
        public bool Delete(Int32 iIDMateria)
        {
            try
            {
                string query = "DELETE FROM Materia " +
                                 $"WHERE ID = {iIDMateria} ";
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
        public bool Read(String nombre, ref DatosMateria datos)
        {
            try
            {
                string query = $"SELECT ID, Nombre, Unidades, Maestro FROM Materia WHERE Nombre = '{nombre}'";
                MySqlCommand cmd = new MySqlCommand(query, CrearConexion());

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    datos.Id = Int32.Parse(reader["ID"].ToString());
                    datos.Nombre = reader["Nombre"].ToString();
                    datos.Unidades = Int32.Parse(reader["Unidades"].ToString());
                    datos.Maestro = reader["Maestro"].ToString();
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
                String sCmd = "SELECT MAX(ID) FROM Materia";
                MySqlCommand cmd = new MySqlCommand(sCmd, CrearConexion());
                iFolioSiguiente = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            }
            catch (Exception ex)
            {
                sLastError = ex.Message;
            }
            return iFolioSiguiente;
        }
    }
}
