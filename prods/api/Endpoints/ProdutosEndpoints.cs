using api.Database;
using api.DTOs;
using api.Models;
using api.Utils;
using Microsoft.EntityFrameworkCore;

namespace api.Endpoints
{
    public static class ProdutosEndpoints
    {
        public static void RegistrarEndpointsProduto(this IEndpointRouteBuilder rotas)
        {
            //Agrupamento de rotas
            RouteGroupBuilder rotaProdutos = rotas.MapGroup("/produtos");

            //GET /produtos
            rotaProdutos.MapGet("/", (ProdsDbContext dbContext, string? NomeProduto, int? idCategoria, bool? status, float? faixapreco, int pagina = 1, int tamanhoPagina = 10) =>
            {
                IEnumerable<Produto> produtosEncontrados = dbContext.Produtos.Include(produto => produto.Categoria).AsQueryable();

                // Verifica se foi passado o ID da categoria presente no Produto
                if(idCategoria is not null)
                {
                    produtosEncontrados = produtosEncontrados.Where(produto => produto.Categoria.Id == idCategoria);
                }

                if(status is not null)
                {
                    produtosEncontrados = produtosEncontrados.Where(produto => produto.Status == status);
                }
                
                if(faixapreco is not null)
                {
                    produtosEncontrados = produtosEncontrados.Where(produto => produto.Preco <= faixapreco);
                }

                if(!string.IsNullOrEmpty(NomeProduto))
                {
                    produtosEncontrados = produtosEncontrados
                        .Where(produto => produto.Nome.Contains(NomeProduto, StringComparison.OrdinalIgnoreCase));
                }

                int totalProdutos = produtosEncontrados.Count();
                List<Produto> produtos = produtosEncontrados.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

                // Retorna os produtos filtrados
                ListaPaginada<Produto> listaProdutos = new ListaPaginada<Produto>(produtos, pagina, tamanhoPagina, totalProdutos);
                return Results.Ok(listaProdutos);

            }).Produces<ListaPaginada<ProdutoDtoOutput>>();

            // GET      /produtos/{Id}
            rotaProdutos.MapGet("/{Id}", (ProdsDbContext dbContext, int Id) =>
            {
                // Procura pelo produto com o Id recebido
                Produto? produto = dbContext.Produtos.Find(Id);

                if (produto is null)
                {
                    // Indica que o produto não foi encontrado
                    return Results.NotFound();
                }

                // Devolve o produto encontrado
                return Results.Ok(produto);

            }).Produces<ProdutoDtoOutput>();


            // POST     /produtos
            rotaProdutos.MapPost("/", (ProdsDbContext dbContext, ProdutoDtoInput produto) =>
            {
                Categoria? categoria = dbContext.Categorias.Find(produto.idCategoria);

                if (categoria is null)
                {
                    return Results.NotFound();
                }

                Produto _novoProduto = produto.ToProduto(categoria);
                var novoProduto = dbContext.Produtos.Add(_novoProduto);

                dbContext.SaveChanges();
                return Results.Created($"/produtos/{novoProduto.Entity.Id}", novoProduto.Entity.GetProdutoDtoOutput());

            }).Produces<ProdutoDtoOutput>();

            // PUT      /produtos/{Id}
            rotaProdutos.MapPut("/{Id}", (ProdsDbContext dbContext, int Id, ProdutoDtoInput produto) =>
            {
                // Encontra o produto especificado buscando pelo Id enviado
                Produto? produtoEncontrado = dbContext.Produtos.Find(Id);
                Categoria? categoria = dbContext.Categorias.Find(produto.idCategoria);

                if (produtoEncontrado is null)
                {
                    // Indica que o produto não foi encontrado
                    return Results.NotFound();
                }

                Produto _novoProduto = produto.ToProduto(categoria);

                // Mantém o Id do produto como o Id existente
                _novoProduto.Id = Id;

                // Atualiza a lista de produtos
                dbContext.Entry(produtoEncontrado).CurrentValues.SetValues(_novoProduto);

                // Salva as alterações no banco de dados
                dbContext.SaveChanges();
                return Results.NoContent();

            }).Produces<ProdutoDtoOutput>();

            // DELETE   /produtos/{Id}
            rotaProdutos.MapDelete("/{Id}", (ProdsDbContext dbContext, int Id) =>
            {
                // Encontra o produto especificado buscando pelo Id enviado
                Produto? produtosEncontrados = dbContext.Produtos.Find(Id);
                if (produtosEncontrados is null)
                {
                    // Indica que o produto não foi encontrado
                    return Results.NotFound();
                }

                // Remove o produto encontrado da lista de filmes
                dbContext.Produtos.Remove(produtosEncontrados);

                dbContext.SaveChanges();
                return Results.NoContent();

            }).Produces<ProdutoDtoOutput>();

        }
    }
}
