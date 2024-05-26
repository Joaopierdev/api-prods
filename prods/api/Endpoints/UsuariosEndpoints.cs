using api.Database;
using api.Models;
using api.Utils;

namespace api.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void RegistrarEndpointsUsuario(this IEndpointRouteBuilder rotas)
        {
            //Agrupamento de rotas
            RouteGroupBuilder rotaUsuarios = rotas.MapGroup("/usuarios");

            //GET /produtos
            rotaUsuarios.MapGet("/", (ProdsDbContext dbContext, bool? status, string? ApelidoUsuario, string? email, int pagina = 1, int tamanhoPagina = 10) =>
            {
                IEnumerable<Usuario> usuariosEncontrados = dbContext.Usuarios.AsQueryable();

                // Verifica se o status foi buscado
                if (status is not null)
                {
                    usuariosEncontrados = usuariosEncontrados.
                    Where(usuario => usuario.Status == status);
                }

                //Verifica se o e-mail foi passado para busca do usuário
                if (!string.IsNullOrEmpty(email))
                {
                    usuariosEncontrados = usuariosEncontrados.
                    Where(usuario => usuario.Email == email);
                }

                //Verifica se o apelido (usuário) foi passado para busca do usuário
                if (!string.IsNullOrEmpty(ApelidoUsuario))
                {
                    usuariosEncontrados = usuariosEncontrados.
                    Where(usuario => usuario.Apelido.Contains(ApelidoUsuario, StringComparison.OrdinalIgnoreCase));
                }

                int totalUsuarios = usuariosEncontrados.Count();
                List<Usuario> usuarios = usuariosEncontrados.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

                // Retorna os usuarios filtrados
                ListaPaginada<Usuario> listaUsuarios = new ListaPaginada<Usuario>(usuarios, pagina, tamanhoPagina, totalUsuarios);
                return TypedResults.Ok(listaUsuarios);

            }).Produces<ListaPaginada<Usuario>>();


            // GET      /usuarios/{Id}
            rotaUsuarios.MapGet("/{Id}", (ProdsDbContext dbContext, int Id) =>
            {
                // Procura pelo usuário com o Id recebido
                Usuario? usuario = dbContext.Usuarios.Find(Id);

                if (usuario is null)
                {
                    // Indica que o usuário não foi encontrado
                    return TypedResults.NotFound();
                }

                // Devolve o usuario encontrado
                return TypedResults.Ok(usuario);

            }).Produces<Usuario>();


            // POST     /usuarios
            rotaUsuarios.MapPost("/", (ProdsDbContext dbContext, Usuario usuario) =>
            {
                var novoUsuario = dbContext.Usuarios.Add(usuario);
                dbContext.SaveChanges();

                return TypedResults.Created($"/usuarios/{usuario.Id}", usuario);
            });

            // PUT      /usuarios/{Id}
            rotaUsuarios.MapPut("/{Id}", (ProdsDbContext dbContext, int Id, Usuario usuario) =>
            {
                // Encontra o usuario especificado buscando pelo Id enviado
                Usuario? usuarioEncontrado = dbContext.Usuarios.Find(Id);

                if (usuarioEncontrado is null)
                {
                    // Indica que o usuario não foi encontrado
                    return TypedResults.NotFound();
                }

                // Mantém o Id do usuario como o Id existente
                usuario.Id = Id;

                // Atualiza a lista de usuarios
                dbContext.Entry(usuarioEncontrado).CurrentValues.SetValues(usuario);

                // Salva as alterações no banco de dados
                dbContext.SaveChanges();
                return TypedResults.NoContent();

            });

            // DELETE   /usuarios/{Id}
            rotaUsuarios.MapDelete("/{Id}", (ProdsDbContext dbContext, int Id) =>
            {
                // Encontra o usuario especificado buscando pelo Id enviado
                Usuario? usuariosEncontrados = dbContext.Usuarios.Find(Id);
                if (usuariosEncontrados is null)
                {
                    // Indica que o usuarios não foi encontrado
                    return TypedResults.NotFound();
                }

                // Remove o produto encontrado da lista de filmes
                dbContext.Usuarios.Remove(usuariosEncontrados);

                dbContext.SaveChanges();
                return TypedResults.NoContent();

            });



        }
    }
}
