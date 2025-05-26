using Diplom.Views;
using System.Windows.Input;

namespace Diplom
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            Routing.RegisterRoute("select", typeof(SeverityPicker));
            Routing.RegisterRoute("session", typeof(SessionPage));
            Routing.RegisterRoute("result", typeof(ResultPage));
        }

        public ICommand OpenSettingsCommand =>
            new Command(async () => await DisplayAlert("Настройки", "Тут будет позже", "OK"));
    }
}
