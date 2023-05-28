using AlumnosMaterias.BaseDatos;
using MySqlConnector;
using static Java.Text.Normalizer;

namespace AlumnosMaterias;

public partial class TomarLista : ContentPage
{
	public TomarLista()
	{
		InitializeComponent();
	}
    private void llenarComboMateria()
    {
        opGrupo grupo = new opGrupo();
        string query = $"SELECT Nombre FROM alumnosMaterias.Materia";
        MySqlCommand COMAND = new MySqlCommand(query, grupo.CrearConexion());
        MySqlDataReader reader = COMAND.ExecuteReader();
        List<string> materias = new List<string>();
        while (reader.Read())
        {
            materias.Add(reader.GetString(0));
        }
        comboBoxMateria.ItemsSource = materias.ToArray();
    }
    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        llenarComboMateria();
    }

    private void comboBoxMateria_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {

    }
    int grupo;
    private void comboBoxGrupo_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        String idGrupo = comboBoxGrupo.SelectedIndex.ToString();
        if (idGrupo == "0")
        {
            grupo = 101;
        }
        else if (idGrupo == "1")
        {
            grupo = 201;
        }
        else if (idGrupo == "2")
        {
            grupo = 301;
        }
        else if (idGrupo == "3")
        {
            grupo = 401;
        }
        else if (idGrupo == "4")
        {
            grupo = 501;
        }
        else if (idGrupo == "5")
        {
            grupo = 601;
        }
        else if (idGrupo == "6")
        {
            grupo = 701;
        }
        else if (idGrupo == "7")
        {
            grupo = 801;
        }
    }

    private void btnConsultar_Clicked(object sender, EventArgs e)
    {
        opTomarLista lista = new opTomarLista();
        string grupo = comboBoxGrupo.SelectedItem.ToString();
        string materia =comboBoxMateria.SelectedItem.ToString();
        string idMateria = $"SELECT ID FROM Materia WHERE Nombre = '{materia}'";
        MySqlCommand cmd2 = new MySqlCommand(idMateria, lista.CrearConexion());
        MySqlDataReader consultar = cmd2.ExecuteReader();
        int id = 0;
        while (consultar.Read())
        {
            id = consultar.GetInt32(0);
        }
        string query = $"SELECT a.NumeroControl, a.Nombre FROM Grupo g INNER JOIN Materia m ON g.Materia = m.ID INNER JOIN Alumno a on g.Alumno = a.NumeroControl WHERE g.Grupo = {grupo} AND g.Materia = {id}";
        MySqlCommand command = new MySqlCommand(query, lista.CrearConexion());
        MySqlDataReader reader = command.ExecuteReader();
        List<DataList> datas = new List<DataList>();
        while (reader.Read())
        {
            DataList data = new DataList();
            data.nombre = reader.GetString(1);
            data.numControl = reader.GetInt32(0);
            datas.Add(data);
        }
        dataGrid2.ItemsSource = datas;
    }

    private async void dataGrid2_CellValueChanged(object sender, Syncfusion.Maui.DataGrid.DataGridCellValueChangedEventArgs e)
    {
        var selectedrow = e.RowData;
        var dt = new DataList();
        dt.numControl = (int)selectedrow.GetType().GetProperty("numControl").GetValue(selectedrow, null);
        dt.nombre = (string)selectedrow.GetType().GetProperty("nombre").GetValue(selectedrow, null);

        string valor1 = Convert.ToString(dt.numControl);
        int id;
        if (int.TryParse(valor1, out id))
        {
            DateTime fecha = dtFecha.Date;
            int grupo = Int32.Parse(comboBoxGrupo.SelectedItem.ToString());
            string materia = comboBoxMateria.SelectedItem.ToString();
            string nombre = Convert.ToString(dt.nombre);
            int nControl = dt.numControl;
            

            if (e.CellValue != null && (bool)e.CellValue == true)
            {
                
            }
            else
            {
                opTomarLista lista = new opTomarLista();
                if (lista.Registrarlista(fecha, grupo, materia, nombre, nControl, 1))
                {
                    await DisplayAlert("Nota", "Asistencia guardada", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", lista.sLastError, "Ok");
                }
            }
        }
        else
        {
            await DisplayAlert("Error", "El valor de NumControl no es válido", "Ok");
        }
    }

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Menu());
    }

    private void btnConsultarFecha_Clicked(object sender, EventArgs e)
    {
        opTomarLista lista = new opTomarLista();
        string grupo = comboBoxGrupo.SelectedItem.ToString();
        string materia = comboBoxMateria.SelectedItem.ToString();
        string idMateria = $"SELECT ID FROM materia WHERE Nombre = '{materia}'";
        DateTime fecha = dtFecha.Date;
        MySqlCommand cmd2 = new MySqlCommand(idMateria, lista.CrearConexion());
        MySqlDataReader consultar = cmd2.ExecuteReader();
        int id = 0;
        while (consultar.Read())
        {
            id = consultar.GetInt32(0);
        }
        string query = $"SELECT a.NumeroControl, a.Nombre, l.Fecha, l.Asistencia FROM Alumno a " +
            $" INNER JOIN Grupo g ON g.Alumno = a.NumeroControl " +
            $" INNER JOIN Lista l ON l.NumeroControl = a.NumeroControl " +
            $"WHERE g.Grupo = {grupo} AND g.Materia = {id} AND l.Fecha = '{fecha.ToString("yyyy/MM/dd")}'" ;
        MySqlCommand command = new MySqlCommand(query, lista.CrearConexion());
        MySqlDataReader reader = command.ExecuteReader();
        List<DataAsistencia> datas = new List<DataAsistencia>();
        while (reader.Read())
        {
            DataAsistencia data = new DataAsistencia();
            data.asistencia = reader.GetInt32(3);
            data.nombreAlumno = reader.GetString(1);
            data.numControl = reader.GetInt32(0);
            data.fecha = reader.GetDateTime(2);

            datas.Add(data);
        }
        dataGrid3.ItemsSource = datas;
    }
}