using api.Models;

namespace api.DTOs
{
    public class UsuarioDtoInput
    {
        public bool Status { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public Usuario ToUsuario()
        {
            return new Usuario(Id, Status, Apelido, Email, Senha);
        }
    }

    public class UsuarioDtoOutput
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }

        public UsuarioDtoOutput(int id, string nome, string email)
        {
            this.Id = id;
            this.Nome = nome;
            this.Email = email;
        }
    }
}
