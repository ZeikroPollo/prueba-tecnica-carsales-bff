using Carsales.RickAndMorty.BFF.Clients;
using Carsales.RickAndMorty.BFF.Models;

namespace Carsales.RickAndMorty.BFF.Services
{
    /// <summary>
    /// Implementación de la lógica de negocio asociada a los episodios.
    /// </summary>
    public class EpisodeService : IEpisodeService
    {
        private readonly IRickAndMortyApiClient _apiClient;
        private readonly ILogger<EpisodeService> _logger;

        public EpisodeService(IRickAndMortyApiClient apiClient, ILogger<EpisodeService> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene un listado paginado de episodios.
        /// </summary>
        public async Task<EpisodesDto> GetEpisodesAsync(int page, CancellationToken cancellationToken = default)
        {
            // si mandan página <= 0, uso 1, se registro.
            if (page <= 0)
            {
                _logger.LogWarning("Se recibió una página inválida {Page}. Se normaliza a 1.", page);
                page = 1;
            }

            var apiResponse = await _apiClient.GetEpisodesAsync(page, cancellationToken)
                             ?? throw new KeyNotFoundException($"No se encontraron episodios para la página {page}");

            var dto = new EpisodesDto
            {
                Page = page,
                TotalPages = apiResponse.Info?.Pages ?? 0,
                Episodes = apiResponse.Results
                    .Select(e => new EpisodeDto
                    {
                        Id = e.Id,
                        Name = e.Name,
                        EpisodeCode = e.Episode,
                        AirDate = e.AirDate
                    })
                    .ToList()
            };

            return dto;
        }

        /// <summary>
        /// Obtiene el detalle de un episodio por su identificador.
        /// </summary>
        public async Task<EpisodeDto?> GetEpisodeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Se recibió un id de episodio inválido {Id}.", id);
                throw new ArgumentException("El id debe ser mayor a cero.", nameof(id));
            }

            var apiEpisode = await _apiClient.GetEpisodeByIdAsync(id, cancellationToken);

            if (apiEpisode is null)
            {
                return null;
            }

            var dto = new EpisodeDto
            {
                Id = apiEpisode.Id,
                Name = apiEpisode.Name,
                EpisodeCode = apiEpisode.Episode,
                AirDate = apiEpisode.AirDate
            };

            return dto;
        }
    }
}