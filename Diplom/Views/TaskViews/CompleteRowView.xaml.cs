using System.ComponentModel;
using Diplom.Models;
using Diplom.Models.TaskModels;

namespace Diplom.Views.TaskViews;

public partial class CompleteRowView : ContentView, ITaskPresenter, INotifyPropertyChanged
{
    public CompleteRowView() => InitializeComponent();

    TaskBase _task = null!;
    public TaskBase Task
    {
        get => _task;
        set
        {
            _task = value;
            var crt = (CompleteRowTask)_task;
            Row.ItemsSource = crt.Row;
            Variants.ItemsSource = crt.Variants;
            BindingContext = _task;
            Reset();
        }
    }

    bool _isCompleted;
    public bool IsCompleted
    {
        get => _isCompleted;
        private set
        {
            if (_isCompleted == value) return;
            _isCompleted = value;
            OnPropertyChanged(nameof(IsCompleted));
        }
    }

    bool _isCorrect;

    public bool IsCorrect => _isCorrect;
    public void CheckAnswer() => OnAnswer(this, EventArgs.Empty);
    public void Reset()
    {
        _isCorrect = false;
        IsCompleted = false;
        Overlay.IsVisible = false;
        Variants.SelectedItem = null;
    }

    void OnSel(object? _, SelectionChangedEventArgs e)
    {
        var crt = (CompleteRowTask)_task;
        var sel = (string?)e.CurrentSelection.FirstOrDefault();

        IsCompleted = sel != null;
        _isCorrect = sel == crt.Correct;
    }

    void OnAnswer(object? sender, EventArgs e)
    {
        if (!IsCompleted) return;
        OverlayText.Text = _isCorrect ? "✅ Верно" : "❌ Неверно";
        OverlayText.TextColor = _isCorrect ? Colors.Green : Colors.Red;
        Overlay.IsVisible = true;
    }

    void OnContinue(object? sender, EventArgs e)
    {
        Overlay.IsVisible = false;
        ContinueRequested?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? ContinueRequested;
    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged(string n) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
