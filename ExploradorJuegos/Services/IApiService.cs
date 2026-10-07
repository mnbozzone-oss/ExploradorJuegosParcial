using ExploradorJuegos.Models;

namespace ExploradorJuegos.Services;

public interface IApiService
{
    Task<List<Juego>> ObtenerJuegosAsync();
}