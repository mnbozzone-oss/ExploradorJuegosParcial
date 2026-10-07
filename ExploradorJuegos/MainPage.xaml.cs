using ExploradorJuegos.ViewModels;

namespace ExploradorJuegos;

    public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
