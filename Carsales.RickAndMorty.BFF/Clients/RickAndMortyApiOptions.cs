namespace Carsales.RickAndMorty.BFF.Clients
{
    /// <summary>
    /// Configuración para la API de Rick and Morty.
    /// Se enlaza con la sección "RickAndMortyApi" del appsettings.json.
    /// </summary>
    public class RickAndMortyApiOptions
    {
        public const string SectionName = "RickAndMortyApi";

        /// <summary>
        /// URL base de la API externa.
        /// </summary>
        public string BaseUrl { get; set; } = string.Empty;

        /// <summary>
        /// Tiempo máximo de espera para solicitudes HTTP.
        /// </summary>
        public int TimeoutSeconds { get; set; } = 30;
    }
}