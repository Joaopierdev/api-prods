using api.Database;
using api.Models;
using api.Utils;

namespace api.Endpoints
{
    public static class CategoriasEndpoint
    {
        public static void RegistrarEndpointsCategoria(this IEndpointRouteBuilder rotas)
        {
            //Agrupamento de rotas
            RouteGroupBuilder rotaCategorias = rotas.MapGroup("/categorias");

            //GET /categorias
            rotaCategorias.MapGet("/", (ProdsDbContext dbContext, string? nomeCategoria, bool? status, int pagina = 1, int tamanhoPagina = 10) =>
            {
                IEnumerable<Categoria> categoriasEncontradas = dbContext.Categorias.AsQueryable();

                // Verifica se o status foi passado
                if (status is not null)
                {
                    categoriasEncontradas = categoriasEncontradas.Where(categoria => categoria.Status == status);
                }

                if (string.IsNullOrEmpty(nomeCategoria))
                {
                    categoriasEncontradas = categoriasEncontradas
                        .Where(categoria => categoria.Nome.Contains(nomeCategoria, StringComparison.OrdinalIgnoreCase));
                }

                int totalCategorias = categoriasEncontradas.Count();
                List<Categoria> categorias = categoriasEncontradas.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

                // Retorna as categorias filtradas
                ListaPaginada<Categoria> listaCategorias = new ListaPaginada<Categoria>(categorias, pagina, tamanhoPagina, totalCategorias);
                return TypedResults.Ok(listaCategorias);

            }).Produces<ListaPaginada<Categoria>>();


            // GET      /categorias/{Id}
            rotaCategorias.MapGet("/{Id}", (ProdsDbContext dbContext, int Id) =>
            {
                // Procura pela categoria com o Id recebido
                Categoria? categoria = dbContext.Categorias.Find(Id);

                if (categoria is null)
                {
                    // Indica que a categoria não foi encontrado
                    return Results.NotFound();
                }

                // Devolve a categoria encontrado
                return TypedResults.Ok(categoria);

            }).Produces<Categoria>();

            // POST     /categorias
            rotaCategorias.MapPost("/", (ProdsDbContext dbContext, Categoria categoria) =>
            {
                var novaCategoria = dbContext.Categorias.Add(categoria);
                dbContext.SaveChanges();

                return TypedResults.Created($"/produtos/{categoria.Id}", categoria);
            });

            // PUT      /categorias/{Id}
            rotaCategorias.MapPut("/{Id}", (ProdsDbContext dbContext, int Id, Categoria categoria) =>
            {
                // Encontra a categoria especificado buscando pelo Id enviado
                Categoria? categoriaEncontrada = dbContext.Categorias.Find(Id);

                if (categoriaEncontrada is null)
                {
                    // Indica que a categoria não foi encontrada
                    return Results.NotFound();
                }

                // Mantém o Id da categoria como o Id existente
                categoria.Id = Id;

                // Atualiza a lista de categorias
                dbContext.Entry(categoriaEncontrada).CurrentValues.SetValues(categoria);

                // Salva as alterações no banco de dados
                dbContext.SaveChanges();
                return TypedResults.NoContent();

            });

            // DELETE   /categorias/{Id}
            rotaCategorias.MapDelete("/{Id}", (ProdsDbContext dbContext, int Id) =>
            {
                // Encontra a categoria especificado buscando pelo Id enviado
                Categoria? categoriasEncontradas = dbContext.Categorias.Find(Id);
                if (categoriasEncontradas is null)
                {
                    // Indica que a categoria não foi encontrada
                    return Results.NotFound();
                }

                // Remove a categoria encontrada da lista de categorias
                dbContext.Categorias.Remove(categoriasEncontradas);

                dbContext.SaveChanges();
                return TypedResults.NoContent();

            });
        }
    }
}
