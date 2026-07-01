namespace AppCompleta;

[QueryProperty(nameof(UserName), "userName")]
public partial class WelcomePage : ContentPage
{
    private string _userName;
    public string UserName
    {
        get => _userName;
        set
        {
            _userName = value;

            LblNome.Text = Uri.UnescapeDataString(value);
        }
    }

    public WelcomePage()
    {
        InitializeComponent();
    }

    private async void OnSairClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }
}