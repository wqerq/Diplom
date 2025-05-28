using Diplom.ViewModels;

namespace Diplom.Views;

public partial class StatsPage : ContentPage
{
    public StatsPage(StatsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}