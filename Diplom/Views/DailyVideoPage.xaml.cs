using CommunityToolkit.Maui.Views;
using Diplom.ViewModels;

namespace Diplom.Views;

public partial class DailyVideoPage : ContentPage
{
    public DailyVideoPage() => InitializeComponent();

    void OnEnded(object sender, EventArgs e)
    {
        if (BindingContext is DailyVideoViewModel vm &&
            sender is MediaElement me &&
            me.BindingContext is DailyVideoViewModel.Clip clip)
        {
            vm.MarkWatched(clip.File);
        }
    }
}
