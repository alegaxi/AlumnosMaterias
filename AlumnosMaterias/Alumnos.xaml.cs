namespace AlumnosMaterias;
using AlumnosMaterias.BaseDatos;
using Syncfusion.Maui.DataSource.Extensions;

public partial class Alumnos : ContentPage
{
    public Boolean bExiste = false;
    public Alumnos()
	{
		InitializeComponent();
        opAlumno alumno = new opAlumno();
        tbNumControl.Text = alumno.Folio().ToString();
    }

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Menu());
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        opAlumno op = new opAlumno();

        if (op.Delete(Int32.Parse(tbNumControl.Text)))
        {
            await DisplayAlert("CORRECTO", "Informacion eliminada correctamente", "Aceptar");
            LimpiarCampos();
        }
        else
        {
            await DisplayAlert("ERROR", op.sLastError, "Aceptar");
        }
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        if(tbNombreAlummno.Text == null)
        {
            await DisplayAlert("ERROR", "Ingrese el nombre", "Aceptar");
        }
        else
        {
            if(tbApellidos.Text == null)
            {
                await DisplayAlert("ERROR", "Ingrese los apellidos", "Aceptar");
            }
            else
            {
                if(tbTelefono.Text == null)
                {
                    await DisplayAlert("ERROR", "Ingrese el telefono", "Aceptar");
                }
                else
                {
                    if(tbCarrera.Text == null)
                    {
                        await DisplayAlert("ERROR", "Ingrese la carrera", "Aceptar");
                    }
                    else
                    {
                        DatosAlumno datos = new DatosAlumno()
                        {
                            NumeroControl = Int32.Parse(tbNumControl.Text),
                            Nombre = tbNombreAlummno.Text,
                            Apellidos = tbApellidos.Text,
                            Telefono = tbTelefono.Text,
                            Carrera = tbCarrera.Text,
                            iGrupo = grupo
                        };
                        opAlumno op = new opAlumno();
                        Boolean bAllOUsuario = (!bExiste) ? op.Insert(datos) : op.Update(datos);

                        if (op.bAllOk == true)
                        {
                            await DisplayAlert("CORRECTO", "Informacion almacenada correctamente", "Aceptar");
                            LimpiarCampos();
                        }
                        else
                        {
                            await DisplayAlert("ERROR", op.sLastError, "Aceptar");
                        }
                    }
                }
            }
        }
    }
    int grupo;
    private void comboBoxGrupo_SelectionChanged(object sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        String idGrupo = comboBoxGrupo.SelectedIndex.ToString();
        if(idGrupo == "0")
        {
            grupo = 101;
        }
        else if(idGrupo == "1")
        {
            grupo = 201;
        }
        else if(idGrupo == "2")
        {
            grupo = 301;
        }
        else if(idGrupo =="3")
        {
            grupo = 401;
        }
        else if(idGrupo == "4")
        {
            grupo = 501;
        }
        else if(idGrupo == "5")
        {
            grupo = 601;
        }
        else if(idGrupo == "6")
        {
            grupo = 701;
        }
        else if(idGrupo == "7")
        {
            grupo = 801;
        }
    }
    void LimpiarCampos()
    {
        tbNombreAlummno.Text = "Nombre";
        tbApellidos.Text = "Apellidos";
        tbTelefono.Text = "Telefono";
        tbCarrera.Text = "Carrera";
        opAlumno alumno = new opAlumno();
        tbNumControl.Text = alumno.Folio().ToString();
    }
    private void tbNumControl_Completed(object sender, EventArgs e)
    {
        DatosAlumno datos = new DatosAlumno();
        opAlumno op = new opAlumno();
        if (op.Read(tbNombreAlummno.Text, ref datos))
        {
            tbNumControl.Text = datos.NumeroControl.ToString();
            tbApellidos.Text = datos.Apellidos;
            tbTelefono.Text = datos.Telefono;
            tbCarrera.Text = datos.Carrera;
            comboBoxGrupo.ItemsSource = datos.iGrupo.ToString();
        }
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        LimpiarCampos();
    }
}