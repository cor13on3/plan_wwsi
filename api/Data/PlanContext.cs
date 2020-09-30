using Microsoft.EntityFrameworkCore;
using Test2.Models;

namespace Test2.Data
{
    public partial class PlanContext : DbContext
    {
        public virtual DbSet<Grupa> Grupa { get; set; }
        public virtual DbSet<GrupaZjazd> GrupaZjazd { get; set; }
        public virtual DbSet<Lekcja> Lekcja { get; set; }
        public virtual DbSet<LekcjaGrupa> LekcjaGrupa { get; set; }
        public virtual DbSet<Przedmiot> Przedmiot { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Specjalnosc> Specjalnosc { get; set; }
        public virtual DbSet<WyklSpec> WyklSpec { get; set; }
        public virtual DbSet<Wykladowca> Wykladowca { get; set; }
        public virtual DbSet<Zjazd> Zjazd { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql("Host=localhost;Database=plancodefirst;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grupa>(entity =>
            {
                entity.HasKey(e => e.NrGrupy);
            });

            modelBuilder.Entity<Zjazd>(entity =>
            {
                entity.HasKey(e => e.IdZjazdu);
            });

            modelBuilder.Entity<Przedmiot>(entity =>
            {
                entity.HasKey(e => e.IdPrzedmiotu);
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.HasKey(e => e.IdSali);
            });

            modelBuilder.Entity<Specjalnosc>(entity =>
            {
                entity.HasKey(e => e.IdSpecjalnosci);
            });

            modelBuilder.Entity<Wykladowca>(entity =>
            {
                entity.HasKey(e => e.IdWykladowcy);
            });



            modelBuilder.Entity<GrupaZjazd>(entity =>
            {
                entity.HasKey(e => new { e.NrZjazdu, e.IdZjazdu, e.NrGrupy });
            });

            modelBuilder.Entity<GrupaZjazd>()
            .HasOne(gz => gz.Grupa)
            .WithMany(g => g.GrupaZjazdList)
            .HasForeignKey(gz => gz.NrGrupy);

            modelBuilder.Entity<GrupaZjazd>()
            .HasOne(gz => gz.Zjazd)
            .WithMany(z => z.GrupaZjazdList)
            .HasForeignKey(gz => gz.IdZjazdu);




            modelBuilder.Entity<Lekcja>(entity =>
            {
                entity.HasKey(e => e.IdLekcji);
            });

            modelBuilder.Entity<Lekcja>()
            .HasOne(l => l.Wykladowca)
            .WithMany(w => w.LekcjaList)
            .HasForeignKey(l => l.IdWykladowcy);

            modelBuilder.Entity<Lekcja>()
            .HasOne(l => l.Przedmiot)
            .WithMany(p => p.LekcjaList)
            .HasForeignKey(l => l.IdPrzedmiotu);

            modelBuilder.Entity<Lekcja>()
            .HasOne(l => l.Sala)
            .WithMany(s => s.LekcjaList)
            .HasForeignKey(l => l.IdSali);




            modelBuilder.Entity<LekcjaGrupa>(entity =>
            {
                entity.HasKey(e => new { e.IdLekcji, e.NrGrupy });
            });

            modelBuilder.Entity<LekcjaGrupa>()
            .HasOne(lg => lg.Lekcja)
            .WithMany(l => l.LekcjaGrupaList)
            .HasForeignKey(lg => lg.IdLekcji);

            modelBuilder.Entity<LekcjaGrupa>()
            .HasOne(lg => lg.Grupa)
            .WithMany(g => g.LekcjaGrupaList)
            .HasForeignKey(lg => lg.NrGrupy);



           



            modelBuilder.Entity<WyklSpec>(entity =>
            {
                entity.HasKey(e => new { e.IdWykladowcy, e.IdSpecjalnosci });
            });

            modelBuilder.Entity<WyklSpec>()
            .HasOne(ws => ws.Wykladowca)
            .WithMany(w => w.WyklSpecList)
            .HasForeignKey(ws => ws.IdWykladowcy);

            modelBuilder.Entity<WyklSpec>()
            .HasOne(ws => ws.Specjalnosc)
            .WithMany(s => s.WyklSpecList)
            .HasForeignKey(ws => ws.IdSpecjalnosci);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
