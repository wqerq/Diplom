using System.ComponentModel;
using Diplom.Models;
using Diplom.Models.TaskModels;

namespace Diplom.Views.TaskViews;

public partial class CategoryView : ContentView, ITaskPresenter, INotifyPropertyChanged
{
    public CategoryView() => InitializeComponent();

    TaskBase _task = null!;
    public TaskBase Task
    {
        get => _task;
        set
        {
            _task = value;
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
        Gallery.SelectedItems.Clear();
    }

    void OnAnswer(object? sender, EventArgs e)
    {
        var ct = (CategoryTask)_task;
        var sel = Gallery.SelectedItems.Cast<string>().ToArray();

        IsCompleted = sel.Length > 0;
        _isCorrect = IsCompleted && sel.OrderBy(s => s)
                                        .SequenceEqual(ct.Answers.OrderBy(s => s));

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
