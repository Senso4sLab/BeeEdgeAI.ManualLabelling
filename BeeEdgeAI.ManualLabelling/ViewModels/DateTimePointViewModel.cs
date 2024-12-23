using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.ViewModels;

public class DateTimePointViewModel
{
    public ObservableCollection<ISeries> Series { get; set; } = new ObservableCollection<ISeries>();
    public ObservableCollection<ICartesianAxis> XAxes { get; set; } = new ObservableCollection<ICartesianAxis>();

    private LineSeries<DateTimePoint> _lineSeries;
    private LineSeries<DateTimePoint> _fillSeries;
    public DateTimePointViewModel()
    {
        XAxes.Add(DateTimeAxis);
        _lineSeries = CreateLineSeries([], null, true);
        _fillSeries = CreateLineSeries([], new SolidColorPaint(SKColors.CornflowerBlue.WithAlpha(40), 3), false);
        Series.Add(_lineSeries);
        Series.Add(_fillSeries);
    }
    public int Length =>
        _lineSeries.Values?.Count ?? 0;
    public static DateTimePointViewModel Empty =>
        new DateTimePointViewModel();
    protected DateTimePointViewModel(IEnumerable<DateTimePoint> dateTimePoints) : this() =>
        AddPointToLineSeries(dateTimePoints);
    public DateTimePointViewModel GetSlice(int slideIndex, int slideWidth) =>
        IsValidSlice(slideIndex, slideWidth) ?
            new DateTimePointViewModel(_lineSeries.Values!.Skip(slideIndex).Take(slideWidth).ToList()) :
            new DateTimePointViewModel();
    private bool IsValidSlice(int start, int slideWidth) =>
        start >= 0 && (start + slideWidth) <= Length;
    public void AddFillUnderSeries(DateTimePointViewModel dateTimePointView)
    {
        if (dateTimePointView._lineSeries.Values?.Any() == true)
            _fillSeries.Values = dateTimePointView._lineSeries.Values.ToList();
    }

    protected void AddPointToLineSeries(IEnumerable<DateTimePoint> dateTimePoints) =>
        AddPointToLineSeries(new ObservableCollection<DateTimePoint>(dateTimePoints));
    private void AddPointToLineSeries(ObservableCollection<DateTimePoint> dateTimePoints) =>
        _lineSeries.Values = dateTimePoints;

    protected ICartesianAxis DateTimeAxis =>
        new DateTimeAxis(TimeSpan.FromMinutes(10), date => date.ToString("H:mm"));

    private LineSeries<DateTimePoint> CreateLineSeries(ObservableCollection<DateTimePoint> dateTimePoints, SolidColorPaint? fill, bool isHoverable) =>
          new LineSeries<DateTimePoint>
          {
              Values = dateTimePoints,
              Fill = fill,
              GeometrySize = 2,
              Stroke = LineSeriesStroke,
              GeometryStroke = new SolidColorPaint(SKColors.CornflowerBlue, 3),
              IsHoverable = isHoverable
          };
    private SolidColorPaint LineSeriesStroke =>
        new SolidColorPaint
        {
            Color = SKColors.CornflowerBlue,
            StrokeCap = SKStrokeCap.Round,
            StrokeThickness = 2,
        };
}
