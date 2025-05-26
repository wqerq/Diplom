using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.ViewModels
{
    public partial class ResultViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty] private string result = "";

        public IRelayCommand RetryCommand { get; }
        public IRelayCommand HomeCommand { get; }

        public ResultViewModel()
        {
            RetryCommand = new RelayCommand(async () => await Shell.Current.GoToAsync(".."));
            HomeCommand = new RelayCommand(async () => await Shell.Current.GoToAsync("//home"));
        }
        public void ApplyQueryAttributes(IDictionary<string, object> q)
        {
            int tot = (int)q["total"], ok = (int)q["correct"];
            Result = $"Верно {ok} из {tot}";
        }
    }

}
