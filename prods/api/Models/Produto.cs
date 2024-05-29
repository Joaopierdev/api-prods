using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace api.Models
{
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        public bool Status { get; set; }

        public Categoria Categoria { get; set; }

        public string Nome { get; set; }

        public string? Descricao { get; set; }

        public string? Image { get; set; }

        public float Preco { get; set; }

        public int Estoque { get; set; }

        public Produto() { }

        public Produto(bool status, Categoria categoria, string nome, string? descricao, string? image, float price, int estoque)
        {
            Status = status;
            Categoria = categoria;
            Nome = nome;
            Descricao = descricao;
            Image = image;
            Preco = price;
            Estoque = estoque;
        }
    }
}
