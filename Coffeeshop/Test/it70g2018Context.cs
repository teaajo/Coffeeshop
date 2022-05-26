﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Coffeeshop.Test
{
    public partial class it70g2018Context : DbContext
    {
       

        public it70g2018Context(DbContextOptions<it70g2018Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Porudzbine> Porudzbines { get; set; }
        public virtual DbSet<Proizvod> Proizvods { get; set; }
        public virtual DbSet<ProizvodPorudzbine> ProizvodPorudzbines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=147.91.175.176;Database=it70g2018;User Id= it70g2018; Password=ftnftn2018;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Porudzbine>(entity =>
            {
                entity.ToTable("Porudzbine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Adresa)
                    .HasMaxLength(50)
                    .HasColumnName("adresa");

                entity.Property(e => e.Datum)
                    .HasColumnType("date")
                    .HasColumnName("datum");

                entity.Property(e => e.IdKorisnik).HasColumnName("id_korisnik");

                entity.Property(e => e.Iznos)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("iznos");

                entity.Property(e => e.Kupon)
                    .HasMaxLength(50)
                    .HasColumnName("kupon");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .HasColumnName("status");
            });

            modelBuilder.Entity<Proizvod>(entity =>
            {
                entity.ToTable("Proizvod");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Cena)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("cena");

                entity.Property(e => e.IdTip).HasColumnName("id_tip");

                entity.Property(e => e.Kolicina).HasColumnName("kolicina");

                entity.Property(e => e.Naziv)
                    .HasMaxLength(50)
                    .HasColumnName("naziv");

                entity.Property(e => e.Opis)
                    .HasMaxLength(50)
                    .HasColumnName("opis");

                entity.Property(e => e.ProsecnaOcena).HasColumnName("prosecna_ocena");
            });

            modelBuilder.Entity<ProizvodPorudzbine>(entity =>
            {
                entity.ToTable("Proizvod_Porudzbine");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdPorudzbine).HasColumnName("id_porudzbine");

                entity.Property(e => e.IdProizvod).HasColumnName("id_proizvod");

                entity.HasOne(d => d.IdPorudzbineNavigation)
                    .WithMany(p => p.ProizvodPorudzbines)
                    .HasForeignKey(d => d.IdPorudzbine)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proizvod_Porudzbine_Porudzbine");

                entity.HasOne(d => d.IdProizvodNavigation)
                    .WithMany(p => p.ProizvodPorudzbines)
                    .HasForeignKey(d => d.IdProizvod)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Proizvod_Porudzbine_Proizvod");
            });

            modelBuilder.HasSequence<int>("Dsekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("ID", "Test")
                .IncrementsBy(2)
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("Kafasekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("Ksekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence("lokacija_seq")
                .StartsAt(3300)
                .IncrementsBy(100)
                .HasMax(9900);

            modelBuilder.HasSequence<int>("Osekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("Psekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("RBR", "ParkingServ")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence("sekvenca")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("Tsekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            modelBuilder.HasSequence<int>("Vsekvenca", "it70g2018")
                .HasMin(1)
                .IsCyclic();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
