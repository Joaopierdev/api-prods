using api.DTOs;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace api.Models
{
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        public string? Nome { get; set; }

        public string? Cargo { get; set; }

        public string Apelido { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public string? Imagem { get; set; }

        public Usuario() { }

        public Usuario(bool status, string? nome, string? cargo, string apelido, string email, string senha, string? imagem)
        {
            Status = status;
            Nome = nome;
            Cargo = cargo;
            Apelido = apelido;
            Email = email;
            Senha = senha;
            Imagem = imagem;
        }
        public UsuarioDtoOutput GetUsuarioDtoOutput()
        {
            return new UsuarioDtoOutput(Id, Status, Apelido, Email);
        }
    }
}
