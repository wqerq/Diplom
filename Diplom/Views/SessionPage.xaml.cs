using Diplom.ViewModels;

namespace Diplom.Views;

public partial class SessionPage : ContentPage
{
    public SessionPage(SessionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        Presenter.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(Presenter.Content) &&
                Presenter.Content is ITaskPresenter p)
                p.Reset();
        };
    }
    protected override bool OnBackButtonPressed()
        => ((SessionViewModel)BindingContext).TryCancel();
}
