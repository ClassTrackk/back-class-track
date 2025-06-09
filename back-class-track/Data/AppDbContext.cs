using back_class_track.Models.Entities;
using Microsoft.EntityFrameworkCore;
using back_class_track.DTO.Corsi;

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
                .HasIndex(u => u.email)
                .IsUnique();

            // Relazione Classe -> Tutor (Utente)
            modelBuilder.Entity<Classe>()
                .HasOne(c => c.tutor)
                .WithMany()
                .HasForeignKey(c => c.tutorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relazioni per Iscrizione (Studente -> Classe)
            modelBuilder.Entity<Iscrizione>()
                .HasOne(i => i.studente)
                .WithMany(u => u.Iscrizioni)
                .HasForeignKey(i => i.studenteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Iscrizione>()
                .HasOne(i => i.classe)
                .WithMany(c => c.iscrizioni)
                .HasForeignKey(i => i.classeId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relazione ClassiCorsi (many-to-many tra Classi e Corsi)
            modelBuilder.Entity<ClasseCorso>()
                .HasOne(cc => cc.classe)
                .WithMany(c => c.classiCorsi)
                .HasForeignKey(cc => cc.classeId);

            modelBuilder.Entity<ClasseCorso>()
                .HasOne(cc => cc.corso)
                .WithMany(c => c.classeCorsi)
                .HasForeignKey(cc => cc.corsoId);

            // Relazione DocentiClassi (many-to-many tra Docenti e Classi)
            modelBuilder.Entity<DocenteClasse>()
                .HasOne(dc => dc.docente)
                .WithMany()
                .HasForeignKey(dc => dc.docenteId);

            modelBuilder.Entity<DocenteClasse>()
                .HasOne(dc => dc.classe)
                .WithMany(c => c.docentiClassi)
                .HasForeignKey(dc => dc.classeId);

            // Relazioni Lezione
            modelBuilder.Entity<Lezione>()
                .HasOne(l => l.classe)
                .WithMany(c => c.lezioni)
                .HasForeignKey(l => l.classeId);

            modelBuilder.Entity<Lezione>()
                .HasOne(l => l.docente)
                .WithMany(u => u.lezioniComeDocente)
                .HasForeignKey(l => l.docenteId);

            // Relazioni Presenza
            modelBuilder.Entity<Presenza>()
                .HasOne(p => p.lezione)
                .WithMany(l => l.Presenze)
                .HasForeignKey(p => p.lezioneId);

            modelBuilder.Entity<Presenza>()
                .HasOne(p => p.studente)
                .WithMany(u => u.PresenzeComeStudente)
                .HasForeignKey(p => p.studenteId);

            // Unicità presenza per studente e lezione
            modelBuilder.Entity<Presenza>()
                .HasIndex(p => new { p.lezioneId, p.studenteId })
                .IsUnique();
        }
        public DbSet<back_class_track.DTO.Corsi.CorsoDTO> CorsoDTO { get; set; } = default!;
    }
}
