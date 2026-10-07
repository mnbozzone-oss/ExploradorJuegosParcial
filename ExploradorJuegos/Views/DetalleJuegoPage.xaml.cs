using ExploradorJuegos.ViewModels;

namespace ExploradorJuegos.Views;

public partial class DetalleJuegoPage : ContentPage
{
	public DetalleJuegoPage(DetalleJuegoViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}