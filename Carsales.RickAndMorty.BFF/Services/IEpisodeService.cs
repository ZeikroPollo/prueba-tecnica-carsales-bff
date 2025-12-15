using Carsales.RickAndMorty.BFF.Models;

namespace Carsales.RickAndMorty.BFF.Services
{
    public interface IEpisodeService
    {
        Task<EpisodesDto> GetEpisodesAsync(int page, CancellationToken cancellationToken = default);
        Task<EpisodeDto?> GetEpisodeByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}