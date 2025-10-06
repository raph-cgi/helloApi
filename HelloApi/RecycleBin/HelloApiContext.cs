using HelloApi.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Globalization;

namespace HelloApi.RecycleBin
{
    public class HelloApiContext : DbContext
    {
        public HelloApiContext(DbContextOptions<HelloApiContext> options) : base(options)
        {
        }

        public DbSet<TPersonEntity> TPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Formats acceptés : 1..4 chiffres pour l'année, 1..2 pour mois/jour
            var formats = new[]
            {
                "yyyy-MM-dd","yyyy-M-d",
                "yyy-MM-dd","yyy-M-d",
                "yy-MM-dd","yy-M-d",
                "y-MM-dd","y-M-d"
            };

            var bornDateConverter = new ValueConverter<DateTime, string>(
    v => v.ToString("yyyy-MM-dd"),
    v => FixDate(v)
);

            var deadDateConverter = new ValueConverter<DateTime?, string>(
                v => v.HasValue ? v.Value.ToString("yyyy-MM-dd") : null,
                v => string.IsNullOrWhiteSpace(v) ? null : FixDate(v)
            );


            modelBuilder.Entity<TPersonEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Prenom).IsRequired().HasMaxLength(100);
                entity.Property(e => e.DateBorn).IsRequired().HasConversion(bornDateConverter).HasColumnType("TEXT");
                entity.Property(e => e.DateDead).HasConversion(deadDateConverter).HasColumnType("TEXT");
                entity.Property(e => e.Nationalite).HasMaxLength(100);
            });

            base.OnModelCreating(modelBuilder);
        }

        private DateTime FixDate(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return DateTime.MinValue; // ou throw, ou null si nullable

            // Normaliser le tiret en ASCII
            s = s.Replace('–', '-').Trim();

            var parts = s.Split('-');

            if (parts.Length < 3)
            {
                // Cas bizarre : pas au format yyyy-MM-dd → on décide quoi faire
                // Exemple : renvoyer MinValue, ou lever une exception contrôlée
                return DateTime.MinValue;
            }

            if (!int.TryParse(parts[0], out var year)) year = 1;
            if (!int.TryParse(parts[1], out var month)) month = 1;
            if (!int.TryParse(parts[2], out var day)) day = 1;

            if (year < 1) year = 1; // DateTime .NET ne supporte pas <= 0
            month = Math.Clamp(month == 0 ? 1 : month, 1, 12);
            day = day == 0 ? 1 : day;
            day = Math.Clamp(day, 1, DateTime.DaysInMonth(year, month));

            return new DateTime(year, month, day);
        }
    }
}