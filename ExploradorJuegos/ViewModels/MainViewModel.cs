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
    private Juego? _juegoSeleccionado;
    private bool _navegando;
    public ObservableCollection<Juego> Juegos { get; } = new();

    public ICommand CargarJuegosCommand { get; }
    public ICommand VerDetalleCommand { get; }
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
   public Juego? JuegoSeleccionado
    {
        get => _juegoSeleccionado;
        set => SetProperty(ref _juegoSeleccionado, value);
    }

    public MainViewModel(IApiService apiService)
    {
        _apiService = apiService;

        CargarJuegosCommand = new Command(
            async () => await CargarJuegosAsync(),
            () => !IsBusy);
        
        
        VerDetalleCommand = new Command(
          async () => await AbrirDetalleAsync());

    }
    private async Task AbrirDetalleAsync()
    {
        if (JuegoSeleccionado is null || _navegando)
            return;

        var juego = JuegoSeleccionado;

        try
        {
            _navegando = true;

            await Shell.Current.GoToAsync(
                "detallejuego",
                new Dictionary<string, object>
                {
                    ["juego"] = juego
                });
        }
        catch (Exception)
        {
            Mensaje = "No pudimos abrir el detalle del juego.";
        }
        finally
        {
            JuegoSeleccionado = null;
            _navegando = false;
        }
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
                400 => "No se pudo procesar la solicitud. Intentá nuevamente. (HTTP 400)",
                404 => "No se encontró la información solicitada. (HTTP 404)",
                >= 500 => $"El servidor tiene un problema. Intentá más tarde. (HTTP {codigo})",
                _ => $"No se pudieron cargar los juegos. Intentá nuevamente. (HTTP {codigo})"
            };
        }
        catch (HttpRequestException)
        {
            Mensaje = "No se pudieron cargar los juegos. Revisá tu conexión a internet e intentá nuevamente.";
        }
        catch (TaskCanceledException)
        {
            Mensaje = "La carga tardó demasiado. Intentá nuevamente.";
        }
        catch (JsonException)
        {
            Mensaje = "La información recibida no tiene el formato esperado. Intentá más tarde.";
        }
        catch (Exception)
        {
            Mensaje = "No se pudieron cargar los juegos. Intentá nuevamente.";
        }
        finally
        {
            IsBusy = false;
        }
    }
}