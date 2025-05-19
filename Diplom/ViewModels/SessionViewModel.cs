using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Diplom.Models;
using Diplom.Models.Enums;
using Diplom.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public partial class SessionViewModel : ObservableObject, IQueryAttributable
    {
        private readonly TaskGeneratorService _gen;
        private Session? _session;

        public TaskBase? Current { get; private set; }
        public string ProgressText => $"{_session?.Index + 1} / {_session?.Total}";

        [ObservableProperty] private bool isRunning;

        public SessionViewModel(TaskGeneratorService gen) => _gen = gen;

        public void ApplyQueryAttributes(IDictionary<string, object> q)
        {
            _session = new Session((AphasiaType)q["type"], (Severity)q["severity"],
                                   q.TryGetValue("count", out var c) ? (int)c : 10);
            LoadNext();
        }

        void LoadNext()
        {
            Current = _gen.Next(_session!.Type, _session.Level);
            OnPropertyChanged(nameof(Current));
            OnPropertyChanged(nameof(ProgressText));
        }

        [RelayCommand]
        void Answer()
        {
            var pres = (ITaskPresenter)CurrentPresenter();
            pres.CheckAnswer();
            if (pres.IsCorrect) _session!.IncCorrect();
            _session!.NextTask();
            if (_session.IsFinished) Finish();
            else LoadNext();
        }

        void Finish() =>
            Shell.Current.GoToAsync("final",
                new Dictionary<string, object>
                {
                    ["total"] = _session!.Total,
                    ["correct"] = _session.Correct
                });

        public bool TryCancel()
        {
            Shell.Current.GoToAsync(".."); return true;
        }

        private Element CurrentPresenter() => (Element)
            (Application.Current.MainPage as Shell).CurrentPage
               .FindByName<ContentPresenter>("Presenter").Content;
    }

}
