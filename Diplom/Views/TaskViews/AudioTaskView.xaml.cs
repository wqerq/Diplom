using Diplom.Models;
using Diplom.Models.TaskModels;
using Microsoft.Maui.Media;
using System.ComponentModel;
using System.Diagnostics;

namespace Diplom.Views.TaskViews;

public partial class AudioTaskView : ContentView, ITaskPresenter, INotifyPropertyChanged
{
    public AudioTaskView() => InitializeComponent();

    TaskBase _task = null!;
    public TaskBase Task
    {
        get => _task;
        set
        {
            _task = value;
        }
    }

    public bool IsCorrect => throw new NotImplementedException();

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

    async void OnSpeakClicked(object? sender, EventArgs e)
    {
        if (Task is null) return;

        var at = (AudioTask)_task;
        await SpeakSlow(at.Text);

        IsCompleted = true;
    }

    async Task SpeakSlow(string full)
    {
        var parts = full.Split(' ');
        foreach (var p in parts)
        {
            await TextToSpeech.Default.SpeakAsync(p);
            await System.Threading.Tasks.Task.Delay(2000);
        }
    }


    void OnContinue(object? sender, EventArgs e)
    {
        ContinueRequested?.Invoke(this, EventArgs.Empty);
    }
    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void CheckAnswer() => OnAnswer(this, EventArgs.Empty);

    public event EventHandler? ContinueRequested;
    public event PropertyChangedEventHandler? PropertyChanged;
    void OnPropertyChanged(string n) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(n));
}
