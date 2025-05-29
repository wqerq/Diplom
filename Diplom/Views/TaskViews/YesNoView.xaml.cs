using Diplom.Models;
using Diplom.Models.TaskModels;
using System.ComponentModel;

namespace Diplom.Views.TaskViews;


public partial class YesNoView : ContentView, ITaskPresenter
{
    public YesNoView() => InitializeComponent();

    public TaskBase Task { get; set; } = null!;
    public bool IsCompleted { get; private set; }
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

    public void CheckAnswer()
    {
        throw new NotImplementedException();
    }
}
