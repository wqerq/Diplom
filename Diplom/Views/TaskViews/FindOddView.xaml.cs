using System.ComponentModel;
using Diplom.Models;
namespace Diplom.Views.TaskViews;

public partial class FindOddView : ContentView, ITaskPresenter, INotifyPropertyChanged
{
    public FindOddView() => InitializeComponent();


    TaskBase _task = null!;
    public TaskBase Task
    {
        get => _task;
        set
        {
            _task = value;
            Pictures.ItemsSource = ((FindOddTask)_task).Images;
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

    public void Reset()
    {
        _isCorrect = false;
        IsCompleted = false;
        Overlay.IsVisible = false;
        Pictures.SelectedItem = null;
    }

    void OnSel(object? _, SelectionChangedEventArgs e)
    {
        var fo = (FindOddTask)_task;
        var sel = (string?)e.CurrentSelection.FirstOrDefault();

        IsCompleted = sel != null;
        _isCorrect = sel == fo.Images[fo.OddIndex];
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

    public bool IsCorrect => _isCorrect;
}
