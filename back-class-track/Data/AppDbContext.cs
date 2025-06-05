using back_class_track.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace back_class_track.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options){}

        #region relazioni con database
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Classe> Classi { get; set; }
        public DbSet<Iscrizione> Iscrizioni { get; set; }
        public DbSet<Corso> Corsi { get; set; }
        public DbSet<ClasseCorso> ClassiCorsi { get; set; }
        public DbSet<DocenteClasse> DocentiClassi { get; set; }
        public DbSet<Lezione> Lezioni { get; set; }
        public DbSet<Presenza> Presenze { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Impostazioni per chiavi primarie e relazioni

            // Unicità Email Utente
            modelBuilder.Entity<Utente>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Relazione Classe -> Tutor (Utente)
            modelBuilder.Entity<Classe>()
                .HasOne(c => c.Tutor)
                .WithMany()
                .HasForeignKey(c => c.TutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazioni per Iscrizione (Studente -> Classe)
            modelBuilder.Entity<Iscrizione>()
                .HasOne(i => i.Studente)
                .WithMany(u => u.Iscrizioni)
                .HasForeignKey(i => i.StudenteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Iscrizione>()
                .HasOne(i => i.Classe)
                .WithMany(c => c.Iscrizioni)
                .HasForeignKey(i => i.ClasseId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione ClassiCorsi (many-to-many tra Classi e Corsi)
            modelBuilder.Entity<ClasseCorso>()
                .HasOne(cc => cc.Classe)
                .WithMany(c => c.ClassiCorsi)
                .HasForeignKey(cc => cc.ClasseId);

            modelBuilder.Entity<ClasseCorso>()
                .HasOne(cc => cc.Corso)
                .WithMany(c => c.ClasseCorsi)
                .HasForeignKey(cc => cc.CorsoId);

            // Relazione DocentiClassi (many-to-many tra Docenti e Classi)
            modelBuilder.Entity<DocenteClasse>()
                .HasOne(dc => dc.Docente)
                .WithMany()
                .HasForeignKey(dc => dc.DocenteId);

            modelBuilder.Entity<DocenteClasse>()
                .HasOne(dc => dc.Classe)
                .WithMany(c => c.DocentiClassi)
                .HasForeignKey(dc => dc.ClasseId);

            // Relazioni Lezione
            modelBuilder.Entity<Lezione>()
                .HasOne(l => l.Classe)
                .WithMany(c => c.Lezioni)
                .HasForeignKey(l => l.ClasseId);

            modelBuilder.Entity<Lezione>()
                .HasOne(l => l.Docente)
                .WithMany(u => u.LezioniComeDocente)
                .HasForeignKey(l => l.DocenteId);

            // Relazioni Presenza
            modelBuilder.Entity<Presenza>()
                .HasOne(p => p.Lezione)
                .WithMany(l => l.Presenze)
                .HasForeignKey(p => p.LezioneId);

            modelBuilder.Entity<Presenza>()
                .HasOne(p => p.Studente)
                .WithMany(u => u.PresenzeComeStudente)
                .HasForeignKey(p => p.StudenteId);

            // Unicità presenza per studente e lezione
            modelBuilder.Entity<Presenza>()
                .HasIndex(p => new { p.LezioneId, p.StudenteId })
                .IsUnique();
        }
    }
}
