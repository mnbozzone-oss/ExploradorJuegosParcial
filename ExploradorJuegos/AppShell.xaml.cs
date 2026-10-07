using ExploradorJuegos.Views;
namespace ExploradorJuegos
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("detallejuego", typeof(DetalleJuegoPage));
        }
    }
}
