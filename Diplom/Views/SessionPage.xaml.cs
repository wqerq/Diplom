using Diplom.Helpers;
using Diplom.Models;
using Diplom.ViewModels;
using Diplom.Views.TaskViews;
using System.ComponentModel;

namespace Diplom.Views;

public partial class SessionPage : ContentPage
{
    private readonly TaskTemplateSelector _selector;

    public SessionPage(SessionViewModel vm)
    {
        InitializeComponent();

        BindingContext = vm;

        _selector = (TaskTemplateSelector)Application.Current.Resources["TaskSelector"];

        vm.TaskPresented = () => TaskHost.Content as ITaskPresenter;

        vm.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(vm.CurrentTask))
                ShowTask(vm.CurrentTask);
        };

        ShowTask(vm.CurrentTask);
    }

    void ShowTask(TaskBase? task)
    {
        if (task is null) return;

        var template = _selector.SelectTemplate(task, this);
        var view = (View)template.CreateContent();

        if (view is ITaskPresenter pres)
        {
            pres.Task = task;

            if (view is INotifyPropertyChanged)
                (view as ContentView).BindingContext = pres;

            pres.ContinueRequested += (_, __) =>
            {
                if (BindingContext is SessionViewModel vm)
                    vm.AnswerCommand.Execute(null);
            };
        }

        TaskHost.Content = view;
    }

    protected override bool OnBackButtonPressed()
    {
        (BindingContext as SessionViewModel)?.CancelCommand.Execute(null);
        return true;
    }
}