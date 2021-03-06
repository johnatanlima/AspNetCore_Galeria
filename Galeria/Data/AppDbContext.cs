﻿using Galeria.Models;
using Microsoft.EntityFrameworkCore;

namespace Galeria.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Album> Albuns { get; set; }

        public DbSet<Imagem> Imagens { get; set; }
    }
}
