using System.Security.Cryptography;
using System.Text;
using AppCompletaAPI.Models;
using AppCompletaAPI.Services;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace AppCompletaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseService _databaseService;

        public UsersController(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            using var connection = _databaseService.GetConnection();

            await connection.OpenAsync();

            // Verifica se o login já existe
            string checkSql = "SELECT COUNT(*) FROM usuarios WHERE login = @login";

            using var checkCmd = new MySqlCommand(checkSql, connection);

            checkCmd.Parameters.AddWithValue("@login", user.Login);

            var count = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());

            if (count > 0)
            {
                return BadRequest("Login já existe.");
            }

            // Hash da senha
            using var sha256 = SHA256.Create();

            var senhaBytes = Encoding.UTF8.GetBytes(user.Senha);

            var senhaHash =
                Convert.ToHexString(
                    sha256.ComputeHash(senhaBytes));

            string sql = @"
        INSERT INTO usuarios
        (nome,email,login,senha)
        VALUES
        (@nome,@email,@login,@senha)";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@login", user.Login);
            cmd.Parameters.AddWithValue("@senha", senhaHash);

            await cmd.ExecuteNonQueryAsync();

            return Ok("Usuário cadastrado com sucesso.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            using var sha256 = SHA256.Create();

            var senhaHash =
                Convert.ToHexString(
                    sha256.ComputeHash(
                        Encoding.UTF8.GetBytes(request.Senha)));

            using var connection = _databaseService.GetConnection();

            await connection.OpenAsync();

            string sql = @"
        SELECT nome
        FROM usuarios
        WHERE login = @login
        AND senha = @senha";

            using var cmd = new MySqlCommand(sql, connection);

            cmd.Parameters.AddWithValue("@login", request.Login);
            cmd.Parameters.AddWithValue("@senha", senhaHash);

            using var reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return Ok(new
                {
                    Success = true,
                    Nome = reader["nome"].ToString()
                });
            }

            return Unauthorized(new
            {
                Success = false,
                Message = "Login ou senha inválidos."
            });
        }

    }




}