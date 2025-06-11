using back_class_track.Data;
using back_class_track.Models.Entities;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace back_class_track.Utilities
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabaseAsync(AppDbContext context)
        {
            #region GENERA UTENTI
            //var userFaker = new Faker<Utente>("it")
            //.RuleFor(u => u.nome, f => f.Name.FirstName()) //GENERA NOME
            //.RuleFor(u => u.cognome, f => f.Name.LastName()) //GENERA COGNOME
            //.RuleFor(u => u.email, (f, u) => f.Internet.Email(u.nome, u.cognome)) //EMAIL NOME.COGNOME@EMAIL.COM
            //.RuleFor(u => u.password, f => BCrypt.Net.BCrypt.HashPassword("password123")) //PASSWORD HASHATA
            //.RuleFor(u => u.ruolo, f => f.PickRandom(new[] { "Studente", //"Tutor", "Docente" })); //RUOLO CASUALE

            ////QUANTI UTENTI CREARE
            //var utenti = userFaker.Generate(50);

            //await context.Utenti.AddRangeAsync(utenti);

            ////SALVO NEL DB
            //await context.SaveChangesAsync();
            #endregion

            #region GENERA CORSI
            //            var nomiCorsi = new[]
            //{
            //    // Matematica
            //    "Algebra Lineare - Base", "Algebra Lineare - Avanzato", "Geometria Analitica", "Analisi Matematica I", "Analisi Matematica II",
            //    // Informatica
            //    "Programmazione C# - Base", "Programmazione C# - Avanzato", "Sviluppo Web con HTML e CSS", "Sviluppo Web con JavaScript", "Basi di Dati - SQL", "Amministrazione Database", "Reti di Computer",
            //    // Italiano
            //    "Letteratura Italiana del '900", "Grammatica Italiana", "Scrittura Creativa", "Storia della Letteratura Italiana",
            //    // Inglese
            //    "Inglese Base", "Inglese Intermedio", "Inglese Avanzato", "Conversazione Inglese", "Preparazione TOEFL", "Letteratura Inglese",
            //    // Scienze
            //    "Biologia Molecolare", "Chimica Organica", "Chimica Inorganica", "Fisica Moderna", "Fisica Meccanica",
            //    // Arte
            //    "Storia dell’Arte Contemporanea", "Disegno Tecnico", "Pittura ad Olio", "Grafica Digitale", "Fotografia Artistica",
            //    // Storia e Geografia
            //    "Storia Contemporanea", "Storia del '900", "Geografia Umana", "Geopolitica",
            //    // Diritto ed Economia
            //    "Diritto Costituzionale", "Diritto Civile", "Economia Aziendale", "Educazione Finanziaria",
            //    // Altro
            //    "Educazione Ambientale", "Psicologia Generale", "Sociologia", "Pedagogia", "Etica e Cittadinanza", "Tecniche di Comunicazione", "Orientamento al Lavoro"
            //};

            //            var corsi = nomiCorsi.Select(nome => new Corso
            //            {
            //                nome = nome,
            //                categoriaGenerale = nome switch
            //                {
            //                    var n when n.Contains("Algebra") || n.Contains("Geometria") || n.Contains("Analisi") => "Matematica",
            //                    var n when n.Contains("Programmazione") || n.Contains("Sviluppo Web") || n.Contains("Basi di Dati") || n.Contains("Database") || n.Contains("Reti") => "Informatica",
            //                    var n when n.Contains("Letteratura Italiana") || n.Contains("Grammatica") || n.Contains("Scrittura") => "Italiano",
            //                    var n when n.Contains("Inglese") || n.Contains("TOEFL") => "Inglese",
            //                    var n when n.Contains("Biologia") || n.Contains("Chimica") || n.Contains("Fisica") => "Scienze",
            //                    var n when n.Contains("Arte") || n.Contains("Disegno") || n.Contains("Pittura") || n.Contains("Grafica") || n.Contains("Fotografia") => "Arte",
            //                    var n when n.Contains("Storia") => "Storia",
            //                    var n when n.Contains("Geografia") || n.Contains("Geopolitica") => "Geografia",
            //                    var n when n.Contains("Diritto") || n.Contains("Economia") || n.Contains("Finanziaria") => "Diritto ed Economia",
            //                    _ => "Altro"
            //                },
            //                durataOre = new Random().Next(20, 100)
            //            }).ToList();

            //            await context.Corsi.AddRangeAsync(corsi);
            //            await context.SaveChangesAsync();



            #endregion

            #region CLASSI

            //// Ottieni i tutor dal database
            //var tutorIds = await context.Utenti
            //    .Where(u => u.ruolo == "Tutor")
            //    .Select(u => u.id)
            //    .ToListAsync();

            //if (tutorIds.Count == 0)
            //{
            //    Console.WriteLine("⚠️ Nessun tutor trovato. Inserisci prima utenti con ruolo 'Tutor'.");
            //    return;
            //}

            //var nomiClassi = new[]
            //{
            //    "1A", "1B", "2A", "2B", "3A", "3B", "4A", "4B", "5A", "5B"
            //};

            //// Anni formativi possibili (puoi adattare)
            //var anniFormativi = new[] { 2023, 2024, 2025 };

            //// Faker per le classi
            //var classiFaker = new Faker<Classe>("it")
            //    .RuleFor(c => c.nome, f => f.PickRandom(nomiClassi))
            //    .RuleFor(c => c.annoFormativo, f => f.PickRandom(anniFormativi))
            //    .RuleFor(c => c.tutorId, f => f.PickRandom(tutorIds));

            //// Genera 8-10 classi
            //var classi = classiFaker.Generate(8);

            //await context.Classi.AddRangeAsync(classi);
            //await context.SaveChangesAsync();

            //Console.WriteLine("✅ Classi inserite con successo.");


            #endregion

        }
    }
}
