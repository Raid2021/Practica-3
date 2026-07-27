using Microsoft.EntityFrameworkCore;
using SistemaVotacion.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVotacion.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Votante> Votantes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Regla: La cédula debe ser única en la base de datos
            modelBuilder.Entity<Votante>()
                .HasIndex(v => v.Cedula)
                .IsUnique();
        }
    }
}