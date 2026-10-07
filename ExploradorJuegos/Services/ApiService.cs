using System.Net.Http.Json;
using System.Text.Json;
using ExploradorJuegos.Models;

namespace ExploradorJuegos.Services;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Juego>> ObtenerJuegosAsync()
    {
        using var respuesta = await _httpClient.GetAsync(
            "https://www.freetogame.com/api/games");

        respuesta.EnsureSuccessStatusCode();

        var juegos = await respuesta.Content
            .ReadFromJsonAsync<List<Juego>>();

        return juegos
            ?? throw new JsonException(
                "La API no devolvió una lista de juegos.");
    }
}