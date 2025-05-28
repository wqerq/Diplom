using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Diplom.Models.Enums;

namespace Diplom.ViewModels
{
    public partial class StartViewModel : ObservableObject
    {
        public IEnumerable<AphasiaType> AphasiaTypes { get; } =
            Enum.GetValues<AphasiaType>();

        public IEnumerable<Severity> Severities { get; } =
            Enum.GetValues<Severity>();

        [ObservableProperty] private AphasiaType? selectedType;
        [ObservableProperty] private Severity? selectedSeverity;


        public bool CanStart =>
            SelectedType is not null && SelectedSeverity is not null;

        [RelayCommand(CanExecute = nameof(CanStart))]
        private async Task Start()
        {
            var parms = new Dictionary<string, object>
            {
                ["type"] = SelectedType,
                ["severity"] = SelectedSeverity
            };
            await Shell.Current.GoToAsync("task", parms);
        }

        [RelayCommand]
        private async Task StartTraining()
        {
            await Shell.Current.GoToAsync("select");
        }

        [RelayCommand]
        private async Task OpenStats()
        {
            await Shell.Current.GoToAsync("stats");
        }

        [RelayCommand]
        private async Task OpenVideo()
        {
            await Shell.Current.GoToAsync("video");
        }

    }

}
