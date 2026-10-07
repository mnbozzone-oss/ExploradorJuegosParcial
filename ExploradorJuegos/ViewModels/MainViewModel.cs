using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using ExploradorJuegos.Models;
using ExploradorJuegos.Services;

namespace ExploradorJuegos.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly IApiService _apiService;
    private bool _isBusy;
    private string _mensaje = "Presioná Cargar juegos para empezar.";

    public ObservableCollection<Juego> Juegos { get; } = new();

    public ICommand CargarJuegosCommand { get; }

    public string Mensaje
    {
        get => _mensaje;
        set => SetProperty(ref _mensaje, value);
    }

    public bool IsBusy
    {
        get => _isBusy;
        set
        {
            if (SetProperty(ref _isBusy, value))
            {
                ((Command)CargarJuegosCommand).ChangeCanExecute();
            }
        }
    }

    public MainViewModel(IApiService apiService)
    {
        _apiService = apiService;

        CargarJuegosCommand = new Command(
            async () => await CargarJuegosAsync(),
            () => !IsBusy);
    }

    private async Task CargarJuegosAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;
            Mensaje = "Cargando juegos...";

            var juegos = await _apiService.ObtenerJuegosAsync();

            Juegos.Clear();

            foreach (var juego in juegos)
            {
                Juegos.Add(juego);
            }

            Mensaje = Juegos.Count > 0
                ? $"Se cargaron {Juegos.Count} juegos."
                : "No se encontraron juegos.";
        }
        catch (HttpRequestException ex) when (ex.StatusCode.HasValue)
        {
            int codigo = (int)ex.StatusCode.Value;

            Mensaje = codigo switch
            {
                400 => "Error HTTP 400: la solicitud no es válida.",
                404 => "Error HTTP 404: no se encontró el recurso.",
                >= 500 => $"Error HTTP {codigo}: el servidor está fallando.",
                _ => $"La API respondió con un error HTTP {codigo}."
            };
        }
        catch (HttpRequestException)
        {
            Mensaje = "No pudimos conectar con la API. Revisá tu conexión.";
        }
        catch (TaskCanceledException)
        {
            Mensaje = "La consulta tardó demasiado. Intentá nuevamente.";
        }
        catch (JsonException)
        {
            Mensaje = "No pudimos interpretar los datos de la API.";
        }
        catch (Exception)
        {
            Mensaje = "Ocurrió un error inesperado al cargar los juegos.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}