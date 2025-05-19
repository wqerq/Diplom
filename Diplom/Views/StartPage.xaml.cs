using CommunityToolkit.Mvvm.Input;
using Diplom.ViewModels;

namespace Diplom.Views;

public partial class StartPage : ContentPage
{
	public StartPage(StartViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}