using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Categoria
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }

        public bool Status { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public Categoria() { }

        public Categoria(int id, bool status, string nome, string? descricao)
        {
            Id = id;
            Status = status;
            Nome = nome;
            Descricao = descricao;
        }
    }
}
