using api.Models;

namespace api.DTOs
{
    public class ProdutoDtoInput
    {
        public bool Status { get; set; }
        public string?  { get; set; }
        public string? Cargo { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Imagem { get; set; }

        public Usuario ToUsuario()
        {
            return new Usuario(Status, Nome, Cargo, Apelido, Email, Senha, Imagem);
        }
    }

    public class UsuarioDtoOutput
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }

        public UsuarioDtoOutput(int id, bool status, string apelido, string email)
        {
            this.Id = id;
            this.Apelido = apelido;
            this.Email = email;
        }
    }
