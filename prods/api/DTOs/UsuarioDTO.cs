using api.Models;
using System.Globalization;

namespace api.DTOs
{
    public class UsuarioDtoInput
    {
        public bool Status { get; set; }
        public string? Nome { get; set; }
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
        public string? Nome { get; set; }
        public string? Cargo { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string? Imagem { get; set; }

        public UsuarioDtoOutput(int id, bool status, string? nome, string? cargo, string apelido, string email, string senha, string? imagem)
        {
            this.Id = id;
            this.Status = status;
            this.Nome = nome;
            this.Cargo = cargo;
            this.Apelido = apelido;
            this.Email = email;
            this.Senha = senha;
            this.Imagem = imagem;
        }
    }
}
