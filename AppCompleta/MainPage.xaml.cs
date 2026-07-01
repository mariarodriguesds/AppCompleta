using AppCompleta.Servicos;

namespace AppCompleta
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryLogin.Text) ||
                string.IsNullOrWhiteSpace(EntrySenha.Text))
            {
                await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
                return;
            }

            var result = await _apiService.LoginAsync(EntryLogin.Text, EntrySenha.Text);

            if (result != null && result.Success)
            {
                EntryLogin.Text = string.Empty;
                EntrySenha.Text = string.Empty;

                await Shell.Current.GoToAsync($"//welcome?userName={result.Nome}");
            }
            else
            {
                EntrySenha.Text = string.Empty;

                await DisplayAlert("Erro", "Login ou senha incorretos.", "OK");
            }
        }

        private async void OnCadastroClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("register");
        }

    }

}
