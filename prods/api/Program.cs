using api.Database;
using api.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Criação da WebApplication
            var builder = WebApplication.CreateBuilder(args);

            // Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PermitirTodasOrigens",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });

            // Configuração do Banco de Dados
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ProdsDbContext>(options =>
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
            );

            // Construção da WebApplication
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                // Inicialização do Swagger
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Registro dos endpoints de produtos
            app.RegistrarEndpointsProduto();

            // Registro dos endpoints de categorias
            app.RegistrarEndpointsCategoria();

            // Registro dos endpoints de usuarios
            app.RegistrarEndpointsUsuario();

            app.UseCors("PermitirTodasOrigens");

            // Execução da aplicação
            app.Run();
        }
    }
}
