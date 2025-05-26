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

        public SessionViewModel(TaskGeneratorService gen) => _gen = gen;

        public TaskBase? CurrentTask { get; private set; }
        public string Progress => _session is null ? "" :
                                  $"{_session.Index + 1}/{_session.Total}";

        public IRelayCommand AnswerCommand => new RelayCommand(Answer);
        public IRelayCommand CancelCommand => new RelayCommand(Cancel);

        public void ApplyQueryAttributes(IDictionary<string, object> q)
        {
            _session = new Session((AphasiaType)q["type"],
                                   (Severity)q["severity"],
                                   q.TryGetValue("count", out var c) ? (int)c : 10);
            LoadNext();
        }

        void LoadNext()
        {
            CurrentTask = _gen.Next(_session!.Type, _session.Level);
            OnPropertyChanged(nameof(CurrentTask));
            OnPropertyChanged(nameof(Progress));
        }

        void Answer()
        {
            if (CurrentTask is null) return;

            var presenter = TaskPresented?.Invoke();
            if (presenter is null || !presenter.IsCompleted) return;

            if (presenter.IsCorrect) _session!.IncCorrect();
            _session!.NextTask();

            if (_session.IsFinished)
                Shell.Current.GoToAsync("final",
                    new Dictionary<string, object>
                    {
                        ["total"] = _session.Total,
                        ["correct"] = _session.Correct
                    });
            else
                LoadNext();
        }

        void Cancel() => Shell.Current.GoToAsync("..");

        public Func<ITaskPresenter?>? TaskPresented { get; set; }
    }

}
