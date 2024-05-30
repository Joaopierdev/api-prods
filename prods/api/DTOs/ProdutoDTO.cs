using api.Models;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace api.DTOs
{
    public class ProdutoDtoInput
    {
        public bool Status { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Imagem { get; set; }
        public float Preco { get; set; }
        public int Estoque { get; set; }

        public Produto ToProduto(Categoria categoria)
        {
            return new Produto(Status, categoria, Nome, Descricao, Imagem, Preco, Estoque);
        }
    }

    public class ProdutoDtoOutput
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public Categoria Categoria { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }

        public string? Image { get; set; }

        public float Preco { get; set; }

        public int Estoque { get; set; }

        public ProdutoDtoOutput(int id, bool status, Categoria categoria, string nome, string? descricao, string? image, float preco, int estoque)
        {
            this.Id = id;
            this.Status = status;
            this.Categoria = categoria;
            this.Nome = nome;
            this.Descricao = descricao;
            this.Image = image;
            this.Preco = preco;
            this.Estoque = estoque;
        }
        
    }
}