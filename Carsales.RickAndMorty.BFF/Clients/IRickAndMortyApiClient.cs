using Carsales.RickAndMorty.BFF.Models;

namespace Carsales.RickAndMorty.BFF.Clients
{
    public interface IRickAndMortyApiClient
    {
        Task<EpisodeApiResponse?> GetEpisodesAsync(int page, CancellationToken cancellationToken = default);
        Task<EpisodeApiModel?> GetEpisodeByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}