using Diplom.Models;
using Diplom.Models.TaskModels;

namespace Diplom.Views.TaskViews;

public partial class SentenceCompleteView : ContentView, ITaskPresenter
{
    public SentenceCompleteView() => InitializeComponent();

    public TaskBase _task = null!;

    public TaskBase Task
    {
        get => _task;
        set
        {
            _task = value;
            BindingContext = (SentenceCompleteTask)_task;
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
    public bool IsCorrect { get; private set; }

    string? picked;

    public event EventHandler? ContinueRequested;

    void OnPick(object sender, EventArgs e)
    {
        picked = ((Button)sender).Text;
        IsCompleted = true;
    }

    void OnAnswer(object? s, EventArgs e)
    {
        if (!IsCompleted) return;

        var t = (SentenceCompleteTask)Task;
        IsCorrect = picked == t.Answer;

        ResultText.Text = IsCorrect ? "✅ Верно" : "❌ Неверно";
        ResultText.TextColor = IsCorrect ? Colors.Green : Colors.Red;
        Overlay.IsVisible = true;
    }

    void OnContinue(object? s, EventArgs e)
    {
        Overlay.IsVisible = false;
        ContinueRequested?.Invoke(this, EventArgs.Empty);
    }

    public void Reset()
    {
        picked = null;
        IsCorrect = false;
        Overlay.IsVisible = false;
    }

    public void CheckAnswer() => OnAnswer(this, EventArgs.Empty);
}
