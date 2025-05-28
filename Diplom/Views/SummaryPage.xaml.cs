namespace Diplom.Views;

public partial class SummaryPage : ContentPage, IQueryAttributable
{
    public SummaryPage() => InitializeComponent();

    public void ApplyQueryAttributes(IDictionary<string, object> q)
    {
        int ok = (int)q["result"];
        int all = (int)q["total"];
        Score.Text = $"Правильно {ok} из {all}";
    }

    void OnHome(object s, EventArgs e) =>
        Shell.Current.GoToAsync("main");
}

