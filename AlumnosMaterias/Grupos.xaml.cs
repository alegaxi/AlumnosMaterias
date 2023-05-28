
using AlumnosMaterias.BaseDatos;
using MySqlConnector;

namespace AlumnosMaterias;

public partial class Grupos : ContentPage
{
	public Grupos()
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
    private void llenarComboAlumno()
    {
        opGrupo grupo = new opGrupo();
        string query = $"SELECT Nombre FROM alumnosMaterias.Alumno";
        MySqlCommand COMAND = new MySqlCommand(query, grupo.CrearConexion());
        MySqlDataReader reader = COMAND.ExecuteReader();
        List<string> materias = new List<string>();
        while (reader.Read())
        {
            materias.Add(reader.GetString(0));
        }
        comboBoxAlumno.ItemsSource = materias.ToArray();
    }
    private void ContentPage_Appearing(object sender, EventArgs e)
    {
        llenarComboMateria();
        llenarComboAlumno();
    }

    private void comboBoxAlumno_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        opGrupo op = new opGrupo();
        DatosGrupos dato = new DatosGrupos();
        op.ConsultarAlumno(Convert.ToString(comboBoxAlumno.SelectedItem), dato);
        NumControl.Text = dato.Alumno.ToString();
    }

    private void comboBoxMateria_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        opGrupo op = new opGrupo();
        DatosGrupos dato = new DatosGrupos();
        op.ConsultarMateria(Convert.ToString(comboBoxMateria.SelectedItem), dato);
        idMateria.Text = dato.Materia.ToString();
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        if(grupo==0)
        {
            DisplayAlert("ERROR", "Seleccione un grupo", "Aceptar");
        }
        else
        {
            if(idMateria.Text == null)
            {
                DisplayAlert("ERROR", "Seleccione una materia", "Aceptar");
            }
            else
            {
                if(NumControl.Text == null)
                {
                    DisplayAlert("ERROR", "Seleccione un alumno", "Aceptar");
                }
                else
                {
                    DatosGrupos datos = new DatosGrupos()
                    {
                        Grupo = grupo,
                        Materia = Int32.Parse(idMateria.Text),
                        Alumno = Int32.Parse(NumControl.Text),
                    };
                    opGrupo op = new opGrupo();
                    op.Insert(datos);
                    if (op.bAllOk == true)
                    {
                        DisplayAlert("CORRECTO", "Informacion almacenada correctamente", "Aceptar");
                        LimpiarCampos();
                    }
                    else
                    {
                        DisplayAlert("ERROR", op.sLastError, "Aceptar");
                    }
                }
            }
        }
    }
    void LimpiarCampos()
    {
        idMateria.Text = "";
        NumControl.Text = "";
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

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {

    }

    private void btnRegresar_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Menu());
    }
}