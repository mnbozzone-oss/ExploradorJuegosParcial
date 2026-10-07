using System.Text.Json.Serialization;

namespace ExploradorJuegos.Models;

public class Juego
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Nombre { get; set; } = string.Empty;

    [JsonPropertyName("short_description")]
    public string Descripcion { get; set; } = string.Empty;

    [JsonPropertyName("genre")]
    public string Genero { get; set; } = string.Empty;

    [JsonPropertyName("platform")]
    public string Plataforma { get; set; } = string.Empty;

    [JsonPropertyName("game_url")]
    public string Url { get; set; } = string.Empty;
}