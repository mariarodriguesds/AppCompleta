using SQLite;
using System.Data;


namespace AppCompleta.Models
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(50)]
        public string Login { get; set; }
        [MaxLength(100)]
        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}