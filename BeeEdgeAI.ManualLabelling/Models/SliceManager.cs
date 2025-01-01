using BeeEdgeAI.ManualLabelling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class SliceManager
{
    private Slice _slice = default!;
    private DateTimePointsVM _points = default!;
    private readonly List<Features> _features = default!;
    public SliceManager(Slice slice, DateTimePointsVM points, IEnumerable<Features> features)
    {
        _slice = slice.ResetStartIndex();
        _points = points;
        _features = features.ToList();
    }

    public FeaturesAndLabel? FeaturesWithDefaultLabel(Slice slice) =>
        slice.InRange(_features.Count)
        ? _features[slice.StartIndex].WithDefaultLabelValue(slice)
        : null;

    public bool CanGetNextSlice =>
        _points.CanSlice(_slice + 1);

    public SlicedDateTimePointsVM? GetNextSlice()
    {
        ShowSlicedArea(++_slice);
        return SlicedDateTimePoints(_slice);
    }
    public bool CanGetPreviousSlice =>
        _points.CanSlice(_slice - 1);
    public SlicedDateTimePointsVM? GetPreviousSlice()
    {
        ShowSlicedArea(--_slice);
        return SlicedDateTimePoints(_slice);
    }
    SlicedDateTimePointsVM? SlicedDateTimePoints(Slice slice) =>
        _points.GetSlicedDateTimePointsVM(slice);

    private void ShowSlicedArea(Slice slice) =>
        _points.ShowSlicedArea(slice);
}
