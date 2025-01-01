using BeeEdgeAI.ManualLabelling.ViewModels;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeeEdgeAI.ManualLabelling.Models;


public class DateTimePointsBuilder<T>
{
    private int _geometrySize = 2;

    private SolidColorPaint _stroke = new SolidColorPaint(SKColors.CornflowerBlue)
    {
        StrokeCap = SKStrokeCap.Round,
        StrokeThickness = 2,
    };

    private SolidColorPaint _geometryStroke = new SolidColorPaint(SKColors.CornflowerBlue, 3);
    private SolidColorPaint _fill = new SolidColorPaint(SKColors.CornflowerBlue.WithAlpha(40), 3);

    private IEnumerable<DateTimePoint> _dataTimePoints = Enumerable.Empty<DateTimePoint>();
    private IEnumerable<DateTimePoint> _fillTimePoints = Enumerable.Empty<DateTimePoint>();

    private Func<T, DateTimePoint> _mapping;
    private TimeSpan _unitTimeSpan = TimeSpan.FromMinutes(1);
    private string _dateTimeFormat = "H:mm";
    public DateTimePointsBuilder(Func<T, DateTimePoint> mapping) =>
        _mapping = mapping;

    public DateTimePointsBuilder<T> WithDateTimeXAxis(TimeSpan? timeSpan = null, string? dateFormat = null)
    {
        _unitTimeSpan = timeSpan ?? _unitTimeSpan;
        _dateTimeFormat = dateFormat ?? _dateTimeFormat;
        return this;
    }
    public DateTimePointsBuilder<T> WithLineSeries(IEnumerable<T> data, SolidColorPaint? stroke = null, SolidColorPaint? geometryStroke = null, int? geometrySize = null)
    {
        _dataTimePoints = data.Select(d => _mapping(d));
        _stroke = stroke ?? _stroke;
        _geometryStroke = geometryStroke ?? _geometryStroke;
        _geometrySize = geometrySize ?? _geometrySize;
        return this;
    }

    public DateTimePointsBuilder<T> WithFillSeries(SolidColorPaint? fill = null)
    {
        _fill = fill ?? _fill;
        return this;
    }
    private LineSeries<DateTimePoint> CreateLineSeries(List<DateTimePoint> points, bool isHoverable, SolidColorPaint? fill = null) =>
          new LineSeries<DateTimePoint>(points)
          {
              Fill = fill,
              Stroke = _stroke,
              GeometrySize = _geometrySize,
              GeometryStroke = _geometryStroke,
              IsHoverable = isHoverable,
              DataLabelsFormatter = point => $"{point.Model.Value} {Environment.NewLine}{point.Model.DateTime.ToString(_dateTimeFormat)}",
          };

    private DateTimeAxis CreateDateTimeAxis() =>
        new DateTimeAxis(_unitTimeSpan, date => date.ToString(_dateTimeFormat));


    public DateTimePointsVM Build()
    {
        var _lineSeries = CreateLineSeries(_dataTimePoints.ToList(), true);
        var _fillSeries = CreateLineSeries([], false, _fill);
        var _xAxes = CreateDateTimeAxis();
        return new DateTimePointsVM(_lineSeries, _fillSeries, _xAxes);
    }
}
