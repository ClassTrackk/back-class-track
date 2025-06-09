using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using back_class_track.DTO.Corsi;
using back_class_track.Data;
namespace back_class_track.Controllers.GestioneCorso;

public static class CorsoDTOEndpoints
{
    public static void MapCorsoDTOEndpoints (this IEndpointRouteBuilder routes)
    {
        //Crea un gruppo di endpoints con il prefisso /api/CorsoDTO
        var group = routes.MapGroup("/api/corsi").WithTags(nameof(CorsoDTO));

        //Rotta principale /api/CorsoDTO/ 
        group.MapGet("/", async (AppDbContext db) =>
        {
            return await db.CorsoDTO.ToListAsync();
        })
        .WithName("GetAllCorsoDTOs")
        .WithOpenApi();

        //Rotta /api/CorsoDTO/{id}  ottiene un corso specifico
        group.MapGet("/{id}", async Task<Results<Ok<CorsoDTO>, NotFound>> (int id, AppDbContext db) =>
        {
            return await db.CorsoDTO.AsNoTracking()
                .FirstOrDefaultAsync(model => model.id == id)
                is CorsoDTO model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetCorsoDTOById")
        .WithOpenApi();

        //Rotta /api/CorsoDTO/{id} aggiorna un corso con specifico ID
        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (int id, CorsoDTO corsoDTO, AppDbContext db) =>
        {
            var affected = await db.CorsoDTO
                .Where(model => model.id == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(m => m.id, corsoDTO.id)
                    .SetProperty(m => m.nome, corsoDTO.nome)
                    .SetProperty(m => m.categoriaGenerale, corsoDTO.categoriaGenerale)
                    .SetProperty(m => m.durataOre, corsoDTO.durataOre)
                    );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateCorsoDTO")
        .WithOpenApi();

        //Rotta principale crea un corso
        group.MapPost("/", async (CorsoDTO corsoDTO, AppDbContext db) =>
        {
            db.CorsoDTO.Add(corsoDTO);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/CorsoDTO/{corsoDTO.id}",corsoDTO);
        })
        .WithName("CreateCorsoDTO")
        .WithOpenApi();

        //Con rotta /api/CorsoDTO/{id} elimina un corso
        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (int id, AppDbContext db) =>
        {
            var affected = await db.CorsoDTO
                .Where(model => model.id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteCorsoDTO")
        .WithOpenApi();
    }
}
