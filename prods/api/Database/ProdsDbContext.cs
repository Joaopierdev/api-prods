using System;
using System.Collections.Generic;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace api.Database;

public partial class ProdsDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public ProdsDbContext()
    {
    }

    public ProdsDbContext(DbContextOptions<ProdsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
