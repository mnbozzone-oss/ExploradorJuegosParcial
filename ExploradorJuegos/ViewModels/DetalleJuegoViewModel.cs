using ExploradorJuegos.Models;

namespace ExploradorJuegos.ViewModels;
public class DetalleJuegoViewModel : BaseViewModel, IQueryAttributable
{
    private Juego? _juego;

    public Juego? Juego
    {
        get => _juego;
        set => SetProperty(ref _juego, value);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("juego", out var valor)
            && valor is Juego juego)
        {
            Juego = juego;
        }
    }
}   