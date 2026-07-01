using AppCompleta.Servicos;

namespace AppCompleta
{
    public partial class RegisterPage : ContentPage
    {
        private readonly ApiService _apiService;

        public RegisterPage()
        {
            InitializeComponent();
            _apiService = new ApiService();
        }

        private async void OnCadastrarClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EntryNome.Text) ||
                string.IsNullOrWhiteSpace(EntryEmail.Text) ||
                string.IsNullOrWhiteSpace(EntryLogin.Text) ||
                string.IsNullOrWhiteSpace(EntrySenha.Text) ||
                string.IsNullOrWhiteSpace(EntryConfirmarSenha.Text))
            {
                await DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
                return;
            }

            if (EntrySenha.Text != EntryConfirmarSenha.Text)
            {
                await DisplayAlert("Erro", "As senhas não coincidem", "OK");
                return;
            }

            if (EntrySenha.Text.Length < 8)
            {
                await DisplayAlert("Erro", "A senha deve ter pelo menos 8 caracteres.", "OK");
                return;
            }

            var newUser = new Models.User
            {
                Nome = EntryNome.Text,
                Email = EntryEmail.Text.Trim().ToLower(),
                Login = EntryLogin.Text.Trim().ToLower(),
                Senha = EntrySenha.Text
            };

            var success = await _apiService.RegisterUserAsync(newUser);

            if (success)
            {
                // Código de limpar os campos...
                EntryNome.Text = string.Empty;
                EntryEmail.Text = string.Empty;
                EntryLogin.Text = string.Empty;
                EntrySenha.Text = string.Empty;
                EntryConfirmarSenha.Text = string.Empty;

                await DisplayAlert("Sucesso", "Usuário cadastrado com sucesso!", "OK");

                Application.Current.MainPage = new AppShell();
            }
            else
            {
                await DisplayAlert("Erro", "Não foi possível cadastrar.", "OK");
            }
        }

        private async void OnVoltarClicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new AppShell();
        }
    }
}
