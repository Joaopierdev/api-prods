namespace api.Endpoints
{
    public static class UsuariosEndpoints
    {
        public static void RegistrarEndpointsUsuario(this IEndpointRouteBuilder rotas)
        {
            //Agrupamento de rotas
            RouteGroupBuilder rotaUsuarios = rotas.MapGroup("/usuarios");
        }
    }
}
