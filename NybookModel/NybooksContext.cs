using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace NybookModel;

public partial class NybooksContext : IdentityDbContext<NybooksUser>
{
    public NybooksContext()
    {
    }

    public NybooksContext(DbContextOptions<NybooksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfigurationBuilder builder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Book>(entity =>
        {
            entity.Property(e => e.Title).IsFixedLength();

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}