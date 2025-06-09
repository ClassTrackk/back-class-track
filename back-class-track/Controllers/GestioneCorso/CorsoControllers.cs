using back_class_track.Data;
using back_class_track.DTO.Corsi;
using back_class_track.Models; // Assuming your Corso entity is here
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace back_class_track.Controllers.GestioneCorso;

public static class CorsoControllers
{
    public static void MapCorsoDTOEndpoints(this IEndpointRouteBuilder routes)
    {
        // Group with prefix /api/corsi
        var group = routes.MapGroup("/api/corsi").WithTags(nameof(CorsoDTO));

        // GET /api/corsi
        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.Corsi
                .Select(c => new CorsoDTO
                {
                    id = c.id,
                    nome = c.nome,
                    categoriaGenerale = c.categoriaGenerale,
                    durataOre = c.durataOre
                }).ToListAsync();
        })
        .WithName("GetAllCorsoDTOs")
        .WithOpenApi();

        // GET /api/corsi/{id}
        group.MapGet("/{id}", async Task<Results<Ok<CorsoDTO>, NotFound>> (int id, AppDbContext db) =>
        {
            var corso = await db.Corsi.AsNoTracking()
                .FirstOrDefaultAsync(c => c.id == id);

            return corso is not null
                ? TypedResults.Ok(new CorsoDTO
                {
                    id = corso.id,
                    nome = corso.nome,
                    categoriaGenerale = corso.categoriaGenerale,
                    durataOre = corso.durataOre
                })
                : TypedResults.NotFound();
        })
        .WithName("GetCorsoDTOById")
        .WithOpenApi();

        // PUT /api/corsi/{id}
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, CorsoDTO corsoDTO, AppDbContext db) =>
        {
            var corso = await db.Corsi.FirstOrDefaultAsync(c => c.id == id);

            if (corso == null)
                return TypedResults.NotFound();

            corso.nome = corsoDTO.nome;
            corso.categoriaGenerale = corsoDTO.categoriaGenerale;
            corso.durataOre = corsoDTO.durataOre;

            await db.SaveChangesAsync();
            return TypedResults.Ok();
        })
        .WithName("UpdateCorsoDTO")
        .WithOpenApi();

        // POST /api/corsi
        group.MapPost("/", async (CorsoDTO dto, AppDbContext db) =>
        {
            var entity = new Models.Entities.Corso
            {
                nome = dto.nome,
                categoriaGenerale = dto.categoriaGenerale,
                durataOre = dto.durataOre
            };

            db.Corsi.Add(entity);
            await db.SaveChangesAsync();

            dto.id = entity.id; // Set ID for response
            return TypedResults.Created($"/api/corsi/{dto.id}", dto);
        })
        .WithName("CreateCorsoDTO")
        .WithOpenApi();

        // DELETE /api/corsi/{id}
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var corso = await db.Corsi.FindAsync(id);
            if (corso == null)
                return TypedResults.NotFound();

            db.Corsi.Remove(corso);
            await db.SaveChangesAsync();
            return TypedResults.Ok();
        })
        .WithName("DeleteCorsoDTO")
        .WithOpenApi();
    }
}
