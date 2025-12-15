namespace Carsales.RickAndMorty.BFF.Models
{
    /// <summary>
    /// DTO para devolver un listado paginado de episodios
    /// desde el BFF hacia el frontend.
    /// </summary>
    public class EpisodesDto
    {
        /// <summary>
        /// Página actual que se está devolviendo.
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Cantidad total de páginas disponibles (según la API externa).
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Lista de episodios de la página actual.
        /// </summary>
        public List<EpisodeDto> Episodes { get; set; } = new();
    }

    /// <summary>
    /// DTO simplificado que representa un episodio.
    /// </summary>
    public class EpisodeDto
    {
        /// <summary>
        /// Identificador único del episodio.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre del episodio.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Código del episodio (por ejemplo S01E01).
        /// </summary>
        public string EpisodeCode { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de emisión.
        /// </summary>
        public string AirDate { get; set; } = string.Empty;
    }
}