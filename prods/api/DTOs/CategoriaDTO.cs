using api.Models;

namespace api.DTOs
{
    public class CategoriaDtoInput
    {
        public bool Status { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }

        public Categoria ToCategoria()
        {
            return new Categoria(Status, Nome, Descricao);
        }
    }

    public class CategoriaDtoOutput
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string Nome { get; set; }

        public CategoriaDtoOutput(int id, bool status, string nome)
        {
            this.Id = id;
            this.Status = status;
            this.Nome = nome;
        }
    }
}
