using back_class_track.Data;
using back_class_track.Models.Entities;
using Bogus;

namespace back_class_track.Utilities
{
    public class DatabaseSeeder
    {
        public static async Task GenerateFakeUser(AppDbContext context)
        {
            try
            {
                var userFaker = new Faker<Utente>()
                .RuleFor(u => u.nome, f => f.Name.FirstName()) //GENERA NOME
                .RuleFor(u => u.cognome, f => f.Name.LastName()) //GENERA COGNOME
                .RuleFor(u => u.email, (f, u) => f.Internet.Email(u.nome, u.cognome)) //EMAIL NOME.COGNOME@EMAIL.COM
                .RuleFor(u => u.password, f => BCrypt.Net.BCrypt.HashPassword("password123")) //PASSWORD HASHATA
                .RuleFor(u => u.ruolo, f => f.PickRandom(new[] { "Studente", "Docente", "Tutor" })); //RUOLO CASUALE

                //QUANTI UTENTI CREARE
                var utenti = userFaker.Generate(10);

                await context.Utenti.AddRangeAsync(utenti);

                //SALVO NEL DB
                await context.SaveChangesAsync();
            }
            catch (Exception ex) {
                throw new Exception("Errore durante il seeding degli utenti: " + ex.Message, ex);
            }
            
        }
    }
}
