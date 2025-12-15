using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using Carsales.RickAndMorty.BFF.Models;

namespace Carsales.RickAndMorty.BFF.Clients
{
    public class RickAndMortyApiClient : IRickAndMortyApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RickAndMortyApiClient> _logger;

        private const string DefaultBaseUrl = "https://rickandmortyapi.com/api/";
        private const int DefaultTimeoutSeconds = 30;

        public RickAndMortyApiClient(
            HttpClient httpClient,
            IOptions<RickAndMortyApiOptions> options,
            ILogger<RickAndMortyApiClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            var cfg = options.Value ?? new RickAndMortyApiOptions();

            // Si la URL viene vacía o mal configurada, uso una por defecto
            if (string.IsNullOrWhiteSpace(cfg.BaseUrl))
            {
                _logger.LogWarning(
                    "La configuración 'RickAndMortyApi:BaseUrl' está vacía. Se utilizará la URL por defecto {BaseUrl}.",
                    DefaultBaseUrl);

                cfg.BaseUrl = DefaultBaseUrl;
            }

            if (cfg.TimeoutSeconds <= 0)
            {
                _logger.LogWarning(
                    "La configuración 'RickAndMortyApi:TimeoutSeconds' es inválida ({Timeout}). Se utilizará el valor por defecto {DefaultTimeout}.",
                    cfg.TimeoutSeconds,
                    DefaultTimeoutSeconds);

                cfg.TimeoutSeconds = DefaultTimeoutSeconds;
            }

            _httpClient.BaseAddress = new Uri(cfg.BaseUrl, UriKind.Absolute);
            _httpClient.Timeout = TimeSpan.FromSeconds(cfg.TimeoutSeconds);
        }

        public async Task<EpisodeApiResponse?> GetEpisodesAsync(int page, CancellationToken cancellationToken = default)
        {
            var url = $"episode?page={page}";

            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error al obtener episodios. StatusCode: {StatusCode}", response.StatusCode);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException(
                    $"Error al llamar a RickAndMorty API: {(int)response.StatusCode} - {content}");
            }

            var data = await response.Content.ReadFromJsonAsync<EpisodeApiResponse>(cancellationToken: cancellationToken);

            return data;
        }

        public async Task<EpisodeApiModel?> GetEpisodeByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var url = $"episode/{id}";

            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Error al obtener episodio {EpisodeId}. StatusCode: {StatusCode}", id, response.StatusCode);

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException(
                    $"Error al llamar a RickAndMorty API: {(int)response.StatusCode} - {content}");
            }

            var data = await response.Content.ReadFromJsonAsync<EpisodeApiModel>(cancellationToken: cancellationToken);

            return data;
        }
    }
}