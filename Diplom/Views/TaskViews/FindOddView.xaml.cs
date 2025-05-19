using Diplom.Models;

namespace Diplom.Views.TaskViews;

public partial class FindOddView : ContentView, ITaskPresenter
{
    public FindOddView() => InitializeComponent();

    public TaskBase Task { get; set; } = null!;
    public bool IsCompleted { get; private set; }
    public bool IsCorrect { get; private set; }
    public void Reset() { IsCompleted = IsCorrect = false; }

    void OnSel(object? s, SelectionChangedEventArgs e)
    {
        var t = (FindOddTask)Task;
        var sel = (string?)e.CurrentSelection.FirstOrDefault();
        IsCorrect = sel == t.Images[t.OddIndex];
        IsCompleted = sel != null;
    }
    public void CheckAnswer() { /* уже посчитано в OnSel */ }
}
