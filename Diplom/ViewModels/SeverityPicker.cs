using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Diplom.Models.Enums;
using System.Diagnostics;

namespace Diplom.ViewModels;

public partial class SeverityPickerViewModel : ObservableObject
{
    public IReadOnlyList<AphasiaType> AphasiaTypes { get; } =
        Enum.GetValues<AphasiaType>();
    public IReadOnlyList<Severity> Severities { get; } =
        Enum.GetValues<Severity>();

    [ObservableProperty] private AphasiaType? selectedType;
    [ObservableProperty] private Severity? selectedSeverity;

    [ObservableProperty] private bool isStep1 = true;
    [ObservableProperty] private bool isStep2;


    partial void OnIsStep1Changed(bool value) => IsStep2 = !value;

    public bool CanFinish =>
        true;

    [RelayCommand]
    private void Next()
    {
        if (SelectedType != null)
            IsStep1 = false;
    }

    [RelayCommand]
    private void Back() => IsStep1 = true;

    [RelayCommand(CanExecute = nameof(CanFinish))]
    private async Task Finish()
    {
        var parms = new Dictionary<string, object>
        {
            ["type"] = SelectedType,
            ["severity"] = SelectedSeverity
        };
        await Shell.Current.GoToAsync("task", parms);
    }
}
