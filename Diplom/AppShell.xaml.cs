using System.Windows.Input;

namespace Diplom
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public ICommand OpenSettingsCommand =>
            new Command(async () => await DisplayAlert("Настройки", "Тут будет позже", "OK"));
    }
}
