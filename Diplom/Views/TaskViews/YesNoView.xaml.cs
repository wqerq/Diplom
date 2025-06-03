using Diplom.Models;
using Diplom.Models.TaskModels;
using Microsoft.Maui.Graphics;

namespace Diplom.Views.TaskViews;


public partial class YesNoView : ContentView, ITaskPresenter
{
    public YesNoView() => InitializeComponent();

    TaskBase _task = null!;
    public TaskBase Task
    {
        get => _task;
        set
        {
            _task = value;
            BindingContext = (YesNoTask)_task;
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


    public event EventHandler? ContinueRequested;


    bool? pickedAnswer;

    void OnPickYes(object s, EventArgs e) => SetPicked(true);
    void OnPickNo(object s, EventArgs e) => SetPicked(false);

    void SetPicked(bool val)
    {
        pickedAnswer = val;
        IsCompleted = true;
    }

    void OnAnswer(object? s, EventArgs e)
    {
        if (!IsCompleted) return;

        var t = (YesNoTask)Task;
        IsCorrect = (pickedAnswer == t.Answer);

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
        pickedAnswer = null;
        IsCompleted = false;
        IsCorrect = false;
        Overlay.IsVisible = false;
    }

    public void CheckAnswer() => OnAnswer(this, EventArgs.Empty);
}
