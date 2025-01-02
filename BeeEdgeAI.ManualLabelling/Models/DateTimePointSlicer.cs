using BeeEdgeAI.ManualLabelling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;



public class DateTimePointSlicer
{
    private Slice _slice = default!;
    private DateTimePointsVM _points = default!;   
    public DateTimePointSlicer(DateTimePointsVM points, int sliceWidth)
    {      
        _points = points;
        _slice = new Slice(sliceWidth);
    }

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
