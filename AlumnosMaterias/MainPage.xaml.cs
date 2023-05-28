using Plugin.LocalNotification;

namespace AlumnosMaterias;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    private void btnAcept_Clicked(object sender, EventArgs e)
    {
        if (tbUser.Text != null && tbPass != null)
        {
            opInicioSesion opUser = new opInicioSesion();
            DatosUsuario datos = new DatosUsuario()
            {
                Usuario = tbUser.Text,
                Contrasena = tbPass.Text
            };
            if (opUser.ValInicioSesion(datos))
            {
                Navigation.PushModalAsync(new Menu());
                var request = new NotificationRequest
                {
                    NotificationId = 2,
                    Title = "BIENVENIDO",
                    Subtitle = "Hello",
                    Description = "Que tengas lindo dia!!",
                    BadgeNumber = 42,
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddSeconds(5),
                        NotifyRepeatInterval = TimeSpan.FromSeconds(5)
                    }

                };
                LocalNotificationCenter.Current.Show(request);
            }
            else
            {
                DisplayAlert("INCORRECTO", "Usuario o contraseña incorrectos", "Aceptar");
            }

        }
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        tbPass.Text = "";
        tbUser.Text= "";
    }
}

