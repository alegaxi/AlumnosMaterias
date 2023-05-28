using AlumnosMaterias.BaseDatos;

namespace AlumnosMaterias;

public partial class Materias : ContentPage
{
    public Boolean bExiste = false;
    public Materias()
	{
		InitializeComponent();
        opMateria materia = new opMateria();
        tbIdMateria.Text = materia.Folio().ToString();
    }
    private void LimpiarCampos()
    {
        opMateria materia = new opMateria();
        tbIdMateria.Text = materia.Folio().ToString();
        tbNombreMateria.Text = "Nombre";
        tbUnidades.Text = "Unidades";
        tbMaestro.Text = "Maestro";
    }
    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Menu());
    }

    private async void btnAgregar_Clicked(object sender, EventArgs e)
    {
        if(tbNombreMateria.Text ==  null)
        {
            await DisplayAlert("ERROR", "Ingrese el nombre de la materia", "Aceptar");
        }
        else
        {
            if(tbUnidades.Text == null)
            {
                await DisplayAlert("ERROR", "Ingrese las unidades", "Aceptar");
            }
            else
            {
                if(tbMaestro.Text == null)
                {
                    await DisplayAlert("ERROR", "Ingrese el nombre del maestro", "Aceptar");
                }
                else
                {
                    DatosMateria datos = new DatosMateria()
                    {
                        Id = Int32.Parse(tbIdMateria.Text),
                        Nombre = tbNombreMateria.Text,
                        Unidades = Int32.Parse(tbUnidades.Text),
                        Maestro = tbMaestro.Text
                    };
                    opMateria op = new opMateria();
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

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        opMateria op = new opMateria();

        if (op.Delete(Int32.Parse(tbIdMateria.Text)))
        {
            await DisplayAlert("CORRECTO", "Informacion eliminada correctamente", "Aceptar");
            LimpiarCampos();
        }
        else
        {
            await DisplayAlert("ERROR", op.sLastError, "Aceptar");
        }
    }

    private void btnCancelar_Clicked(object sender, EventArgs e)
    {
        LimpiarCampos();
    }

    private void tbNombreMateria_Completed(object sender, EventArgs e)
    {
        DatosMateria dato = new DatosMateria();
        opMateria opUser = new opMateria();
        if (opUser.Read(tbNombreMateria.Text, ref dato))
        {
            tbIdMateria.Text = Convert.ToString(dato.Id);
            tbUnidades.Text = Convert.ToString(dato.Unidades);
            tbMaestro.Text = dato.Maestro;
        }
    }
}