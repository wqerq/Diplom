using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Storage;
using System.Collections.ObjectModel;

namespace Diplom.ViewModels;
public partial class DailyVideoViewModel : ObservableObject
{
    public partial class Clip : ObservableObject
    {
        public string Title { get; }
        public string File { get; }

        [ObservableProperty] bool watched;

        public Clip(string title, string file, bool watched)
        {
            Title = title; File = file; this.watched = watched;
        }
    }


    const string Key = "video_";

    public ObservableCollection<Clip> Clips { get; }

    public DailyVideoViewModel()
    {
        Clips = new ObservableCollection<Clip>
        {
            new Clip("Первое видео",
                     "FirstTrainig.mp4",
                     Preferences.Get(Key+"FirstTrainig.mp4", false)),

            new Clip("Второе видео",
                     "SecondTrainnig.mp4",
                     Preferences.Get(Key+"SecondTrainnig.mp4", false))
        };
    }

    public void MarkWatched(string file)
    {
        var clip = Clips.First(c => c.File == file);
        if (!clip.Watched)
        {
            clip.Watched = true;
            Preferences.Set(Key + file, true);
        }
    }
}
