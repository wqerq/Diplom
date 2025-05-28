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
        private readonly IResultStorage _store;

        Session? _sess;
        public TaskBase? CurrentTask { get; private set; }
        public string Progress => $"{_sess?.Index + 1}/{_sess?.Total}";

        public SessionViewModel(TaskGeneratorService gen, IResultStorage store)
        {
            _gen = gen;
            _store = store;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> q)
        {
            int total = q.TryGetValue("count", out var c) ? (int)c : 10;
            _sess = new Session((AphasiaType)q["type"], (Severity)q["severity"], total);
            LoadNext();
        }


        void LoadNext()
        {
            CurrentTask = _gen.Next(_sess!.Type, _sess.Level);
            OnPropertyChanged(nameof(CurrentTask));
            OnPropertyChanged(nameof(Progress));
        }

        [RelayCommand]
        void Answer()
        {
            var pres = TaskPresented?.Invoke();
            if (pres == null) return;

            pres.CheckAnswer();
            if (pres.IsCorrect) _sess!.IncCorrect();
            _sess!.NextTask();
            if (_sess!.IsFinished)
                Finish();
            else
                LoadNext();
        }

        void Finish()
        {
            _store.Save(new TestResult
            {
                Date = DateTime.Now,
                Type = _sess!.Type,
                Level = _sess.Level,
                Total = _sess.Total,
                Correct = _sess.Correct
            });

            Shell.Current.GoToAsync("summary",
                new Dictionary<string, object>
                {
                    ["result"] = _sess.Correct,
                    ["total"] = _sess.Total
                });
        }

        public Func<ITaskPresenter?>? TaskPresented { get; set; }

        [RelayCommand]
        void Cancel() => Shell.Current.GoToAsync("..");
    }


}
