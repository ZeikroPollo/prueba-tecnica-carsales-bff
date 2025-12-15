using System.Text.Json.Serialization;

namespace Carsales.RickAndMorty.BFF.Models
{
    /// <summary>
    /// Respuesta de la API externa para el listado de episodios.
    /// </summary>
    public class EpisodeApiResponse
    {
        public EpisodeApiInfo Info { get; set; } = new();
        public List<EpisodeApiModel> Results { get; set; } = new();
    }

    /// <summary>
    /// Información de paginación que entrega la API externa.
    /// </summary>
    public class EpisodeApiInfo
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string? Next { get; set; }
        public string? Prev { get; set; }
    }

    /// <summary>
    /// Episodio tal como viene desde la API de Rick and Morty.
    /// </summary>
    public class EpisodeApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // El JSON tiene "air_date", lo mapeamos a AirDate
        [JsonPropertyName("air_date")]
        public string AirDate { get; set; } = string.Empty;

        public string Episode { get; set; } = string.Empty;
        public List<string> Characters { get; set; } = new();
        public string Url { get; set; } = string.Empty;
        public string Created { get; set; } = string.Empty;
    }
}