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
        public DbSet<SistemaVotacion.Core.Entities.PartidoPolitico> Partidos { get; set; }
        public DbSet<SistemaVotacion.Core.Entities.Voto> Votos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Regla: La cédula debe ser única en la base de datos
            modelBuilder.Entity<Votante>()
                .HasIndex(v => v.Cedula)
                .IsUnique();

            // Índices únicos para partidos: Nombre y Siglas
            modelBuilder.Entity<SistemaVotacion.Core.Entities.PartidoPolitico>(entity =>
            {
                // Propiedades con restricciones: Nombre requerido (max 100), Siglas requerido (max 20), Descripcion opcional (max 500)
                entity.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
                entity.Property(p => p.Siglas).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Descripcion).HasMaxLength(500);

                entity.HasIndex(p => p.Nombre).IsUnique();
                entity.HasIndex(p => p.Siglas).IsUnique();
            });

            // Configuración de relación Voto -> Votante y PartidoPolitico
            modelBuilder.Entity<SistemaVotacion.Core.Entities.Voto>(entity =>
            {
                entity.HasKey(v => v.Id);

                // Relación con Votante: muchos Votos a un Votante
                entity.HasOne(v => v.Votante)
                      .WithMany()
                      .HasForeignKey(v => v.VotanteId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relación con PartidoPolitico: muchos Votos a un PartidoPolitico
                entity.HasOne(v => v.PartidoPolitico)
                      .WithMany()
                      .HasForeignKey(v => v.PartidoPoliticoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(v => v.FechaVoto).IsRequired();
                // Índice único para asegurar que un votante solo tenga un registro en Votos
                entity.HasIndex(v => v.VotanteId).IsUnique();
            });
        }
    }
}