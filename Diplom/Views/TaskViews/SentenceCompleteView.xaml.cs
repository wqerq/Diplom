using Diplom.Models;
using Diplom.Models.TaskModels;

namespace Diplom.Views.TaskViews;

public partial class SentenceCompleteView : ContentView, ITaskPresenter
{
    public SentenceCompleteView() => InitializeComponent();

    public TaskBase Task { get; set; } = null!;
    public bool IsCompleted => picked is not null;
    public bool IsCorrect { get; private set; }

    string? picked;

    public event EventHandler? ContinueRequested;

    void OnPick(object sender, EventArgs e)
        => picked = ((Button)sender).Text;

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

    public void CheckAnswer()
    {
        throw new NotImplementedException();
    }
}
