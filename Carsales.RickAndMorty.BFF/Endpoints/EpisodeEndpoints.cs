using Carsales.RickAndMorty.BFF.Services;

namespace Carsales.RickAndMorty.BFF.Endpoints
{
    /// <summary>
    /// Registro de endpoints relacionados con episodios.
    /// Implementado usando Minimal APIs.
    /// </summary>
    public static class EpisodeEndpoints
    {
        /// <summary>
        /// Registra los endpoints de episodios en la aplicación.
        /// </summary>
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/episodes")
                           .WithTags("Episodes");

            // GET /api/episodes?page=1
            group.MapGet("/", async (
                    int page,
                    IEpisodeService service,
                    CancellationToken ct) =>
            {
                var result = await service.GetEpisodesAsync(page, ct);
                return Results.Ok(result);
            })
                .WithName("GetEpisodes")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound);

            // GET /api/episodes/{id}
            group.MapGet("/{id:int}", async (
                    int id,
                    IEpisodeService service,
                    CancellationToken ct) =>
            {
                var episode = await service.GetEpisodeByIdAsync(id, ct);

                if (episode is null)
                {
                    return Results.NotFound(new { message = $"Episodio {id} no encontrado" });
                }

                return Results.Ok(episode);
            })
                .WithName("GetEpisodeById")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest);
        }
    }
}