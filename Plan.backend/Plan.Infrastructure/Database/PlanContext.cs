using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Plan.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Plan.Serwis.BazaDanych
{
    public partial class PlanContext : IdentityDbContext<Uzytkownik>
    {
        private IConfiguration _configuration;
        public virtual DbSet<Grupa> Grupa { get; set; }
        public virtual DbSet<GrupaZjazd> GrupaZjazd { get; set; }
        public virtual DbSet<Lekcja> Lekcja { get; set; }
        public virtual DbSet<LekcjaGrupa> LekcjaGrupa { get; set; }
        public virtual DbSet<Przedmiot> Przedmiot { get; set; }
        public virtual DbSet<Sala> Sala { get; set; }
        public virtual DbSet<Specjalnosc> Specjalnosc { get; set; }
        public virtual DbSet<WykladowcaSpecjalizacja> WyklSpec { get; set; }
        public virtual DbSet<Wykladowca> Wykladowca { get; set; }
        public virtual DbSet<Zjazd> Zjazd { get; set; }

        public PlanContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(ConfigurationExtensions.GetConnectionString(this._configuration, "PlanDB"), b => b.MigrationsAssembly("Plan.Infrastructure"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Grupa>(entity =>
            {
                entity.HasKey(e => e.NrGrupy);
                entity.Property(e => e.TrybStudiow)
                      .HasConversion(new EnumToStringConverter<TrybStudiow>());
                entity.Property(e => e.StopienStudiow)
                      .HasConversion(new EnumToStringConverter<StopienStudiow>());
            });

            modelBuilder.Entity<Zjazd>(entity =>
            {
                entity.Property(e => e.RodzajSemestru)
                      .HasConversion(new EnumToStringConverter<RodzajSemestru>());
                entity.HasKey(e => e.IdZjazdu);
            });

            modelBuilder.Entity<Przedmiot>(entity =>
            {
                entity.HasKey(e => e.IdPrzedmiotu);
            });

            modelBuilder.Entity<Sala>(entity =>
            {
                entity.Property(e => e.Rodzaj)
                      .HasConversion(new EnumToStringConverter<RodzajSali>());
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
                entity.HasKey(e => new { e.IdZjazdu, e.NrGrupy });
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
                entity.Property(e => e.Forma)
                      .HasConversion(new EnumToStringConverter<FormaLekcji>());
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
                entity.HasKey(e => new { e.IdLekcji, e.NrGrupy, e.NrZjazdu });
            });

            modelBuilder.Entity<LekcjaGrupa>()
            .HasOne(lg => lg.Lekcja)
            .WithMany(l => l.LekcjaGrupaList)
            .HasForeignKey(lg => lg.IdLekcji);

            modelBuilder.Entity<LekcjaGrupa>()
            .HasOne(lg => lg.Grupa)
            .WithMany(g => g.LekcjaGrupaList)
            .HasForeignKey(lg => lg.NrGrupy);


            modelBuilder.Entity<WykladowcaSpecjalizacja>(entity =>
            {
                entity.HasKey(e => new { e.IdWykladowcy, e.IdSpecjalnosci });
            });

            modelBuilder.Entity<WykladowcaSpecjalizacja>()
            .HasOne(ws => ws.Wykladowca)
            .WithMany(w => w.WyklSpecList)
            .HasForeignKey(ws => ws.IdWykladowcy);

            modelBuilder.Entity<WykladowcaSpecjalizacja>()
            .HasOne(ws => ws.Specjalnosc)
            .WithMany(s => s.WyklSpecList)
            .HasForeignKey(ws => ws.IdSpecjalnosci);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
