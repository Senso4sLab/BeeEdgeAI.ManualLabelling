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
using BeeEdgeAI.ManualLabelling.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore.SkiaSharpView.VisualElements;


namespace BeeEdgeAI.ManualLabelling.ViewModels;


[ObservableObject]
public partial class SlicedDateTimePointsVM 
{
    public Slice Slice { get; }

    [ObservableProperty]
    private LabelVisual title;

    [ObservableProperty]
    private ObservableCollection<ISeries> series = new ObservableCollection<ISeries>();

    [ObservableProperty]
    private ObservableCollection<ICartesianAxis> xAxes = new ObservableCollection<ICartesianAxis>();
    
    public SlicedDateTimePointsVM(Slice slice, LineSeries<DateTimePoint> lineSeries, ICartesianAxis xAxis)
    {
        Slice = slice;       
        Series.Add(SlicedLineSeries(lineSeries));
        XAxes.Add(xAxis);
        SetTitle(string.Empty);
    }


    public void SetTitle(string title)
    {
        Title = new LabelVisual
        {
            Text = title,
            TextSize = 18,
            Paint = new SolidColorPaint(SKColors.CornflowerBlue, 10),
            Padding = new LiveChartsCore.Drawing.Padding(10)
        };
    }
    private LineSeries<DateTimePoint> SlicedLineSeries(LineSeries<DateTimePoint> series) =>
     new LineSeries<DateTimePoint>()
     {
         
         Values = series.Values?.Skip(Slice.StartIndex).Take(Slice.Width).ToList(),
         Fill = series.Fill?.CloneTask(),
         Stroke = series.Stroke?.CloneTask(),         
         GeometryStroke = series.GeometryStroke?.CloneTask(),
         GeometrySize = series.GeometrySize,
         IsHoverable = series.IsHoverable,
         DataLabelsFormatter = series.DataLabelsFormatter,
     }; 

}



[ObservableObject]
public partial class DateTimePointsVM
{
    private ICartesianAxis _xAxes = default!;
    private LineSeries<DateTimePoint> _lineSeries = default!;
    private LineSeries<DateTimePoint> _fillSeries = default!;


    [ObservableProperty]
    private LabelVisual title;

    [ObservableProperty]
    private ObservableCollection<ISeries> series = new ObservableCollection<ISeries>();

    [ObservableProperty]
    private ObservableCollection<ICartesianAxis> xAxes = new ObservableCollection<ICartesianAxis>();   

    
    public DateTimePointsVM(LineSeries<DateTimePoint> lineSeries, LineSeries<DateTimePoint> fillSeries, ICartesianAxis xAxis)
    {
        _lineSeries = lineSeries;              
        _fillSeries = fillSeries;
        _xAxes = xAxis;

        AddSeriesAndAxes();       
        SetTitle(string.Empty);
    }


    public void SetTitle(string title)
    {
        Title = new LabelVisual
        {
            Text = title,
            TextSize = 18,
            Paint = new SolidColorPaint(SKColors.CornflowerBlue, 10),
            Padding = new LiveChartsCore.Drawing.Padding(10)
        };
    }

    private void AddSeriesAndAxes()
    {
        Series.Add(_lineSeries);
        Series.Add(_fillSeries);
        XAxes.Add(_xAxes);
    }

    public void ShowSlicedArea(Slice slice)
    {
        if (!CanSlice(slice))
            return;

        _fillSeries.Values = _lineSeries?.Values?.Skip(slice.StartIndex).Take(slice.Width).ToList();

        SetTitle($"Section - {slice.StartIndex}");
    }   

    public bool CanSlice(Slice slice) =>
        _lineSeries.Values?.Count is int length && slice.InRange(length);


    public SlicedDateTimePointsVM? GetSlicedDateTimePointsVM(Slice slice) =>
        CanSlice(slice)
        ? new SlicedDateTimePointsVM(slice, _lineSeries, _xAxes)
        : null;   

}
















