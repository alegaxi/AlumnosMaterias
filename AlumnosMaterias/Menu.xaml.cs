using Plugin.LocalNotification;

namespace AlumnosMaterias;

public partial class Menu : ContentPage
{
	public Menu()
	{
		InitializeComponent();
	}

    private void btnMateria_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Materias());
    }

    private void btnAlumnos_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Alumnos());
    }

    private void btnGrupos_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new Grupos());
    }

    private void btnLista_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new TomarLista());
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "TOMAR LISTA",
            Subtitle = "Hello",
            Description= "No te olvides de tomar lista",
            BadgeNumber= 42,
            Schedule = new NotificationRequestSchedule
            {
                NotifyTime= DateTime.Now.AddSeconds(5),
                NotifyRepeatInterval= TimeSpan.FromSeconds(5)
            }

        };
        LocalNotificationCenter.Current.Show(request);
    }

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new MainPage());
    }
}