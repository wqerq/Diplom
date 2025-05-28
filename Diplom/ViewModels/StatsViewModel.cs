using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Diplom.Services;
using Microcharts;
using SkiaSharp;
using System.Collections.ObjectModel;
using Diplom.Models;


namespace Diplom.ViewModels;

public partial class StatsViewModel : ObservableObject
{
    public enum StatsMode { Day, Range }
    private readonly IResultStorage _store;
    private readonly List<TestResult> _all;

    [ObservableProperty] DateTime selectedDate = DateTime.Today;
    [ObservableProperty] DateTime fromDate;
    [ObservableProperty] DateTime toDate;
    [ObservableProperty] Chart chart;

    [ObservableProperty] StatsMode mode = StatsMode.Day;

    public StatsViewModel(IResultStorage store)
    {
        _store = store;
        _all = _store.LoadAll().OrderBy(r => r.Date).ToList();

        ToDate = DateTime.Today;
        FromDate = ToDate.AddDays(-6);

        BuildRangeChart();
    }


    [RelayCommand] void BuildPie() => BuildPieChart();
    [RelayCommand] void BuildRange() => BuildRangeChart();


    void BuildPieChart()
    {
        var dayRes = _all
            .Where(r => r.Date.Date == SelectedDate.Date)
            .OrderBy(r => r.Date)
            .LastOrDefault();

        if (dayRes == null)
        {
            Chart = null!;
            return;
        }

        var right = dayRes.Correct;
        var wrong = dayRes.Total - dayRes.Correct;

        Chart = new PieChart
        {
            Entries =
            [
                new ChartEntry(right)
                {
                    Label = "Верно",
                    ValueLabel = right.ToString(),
                    Color = SKColor.Parse("#4CAF50")
                },
                new ChartEntry(wrong)
                {
                    Label = "Ошибки",
                    ValueLabel = wrong.ToString(),
                    Color = SKColor.Parse("#F44336")
                }
            ],
            HoleRadius = 0.4f
        };
    }

    void BuildRangeChart()
    {
        var days = _all
           .Where(r => r.Date.Date >= FromDate.Date && r.Date.Date <= ToDate.Date)
           .GroupBy(r => r.Date.Date)
           .Select(g => g.OrderBy(r => r.Date).Last())
           .OrderBy(r => r.Date)
           .ToList();

        var entries = days.Select(r =>
        {
            float percent = 100f * r.Correct / r.Total;
            return new ChartEntry(percent)
            {
                Label = r.Date.ToString("dd.MM"),
                ValueLabel = $"{percent:0}%",
                Color = new SKColor(63, 81, 181)
            };
        }).ToArray();

        Chart = new BarChart
        {
            Entries = entries,
            MaxValue = 100,
            MinValue = 0,
            ValueLabelOrientation = Orientation.Horizontal,
            LabelOrientation = Orientation.Horizontal
        };
    }

    [RelayCommand] void ShowDay() => Mode = StatsMode.Day;
    [RelayCommand] void ShowRange() => Mode = StatsMode.Range;

}

