using BeeEdgeAI.ManualLabelling.ViewModels;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class BeeHiveDisplayManager
{
    private int SliceWidth { get; }
    private int MaxSlices
    {
        get
        {
            int slices = _beeHiveDate.Length - SliceWidth;
            return slices < 0 ? 0 : slices;
        }
    }

    private int _sliceIndex;
    public int SliceIndex
    {
        get => _sliceIndex;
        set
        {
            if (value < MaxSlices && value >= 0)
                _sliceIndex = value;
        }
    }

    private readonly BeeHiveDateTimeViewModel _beeHiveDate;
    private readonly HistoryBeeHiveFeatureLabel _beeHistory;
    public BeeHiveDisplayManager(BeeHiveDateTimeViewModel beeHiveDate, HistoryBeeHiveFeatureLabel beeHistory, int sliceWidth)
    {
        _beeHiveDate = beeHiveDate;
        _beeHistory = beeHistory;
        SliceWidth = sliceWidth;
    }
    
    public void ResetSliceIndex()
    {
        SliceIndex = 0;
    }   

    public bool CanGetNextSlice =>
        this.SliceIndex < _beeHiveDate.Length;

    public DateTimeFeaturesViewModel GetSlice=>    
        _beeHiveDate.GetViewModelWithFeaturesForSlice(SliceIndex, SliceWidth);

    public DateTimeFeaturesViewModel GetNextSlice()
    {
        var vmWithFeatures = _beeHiveDate.GetViewModelWithFeaturesForSlice(SliceIndex++, SliceWidth);
        _beeHiveDate.AddFillUnderSeries(vmWithFeatures.DateTimePointViewModel);
        return vmWithFeatures;
    }

    public bool CanGetPreviousSlice =>
        this.SliceIndex > 0;
    public DateTimeFeaturesViewModel GetPreviousSlice()
    {
        var vmWithFeatures = _beeHiveDate.GetViewModelWithFeaturesForSlice(--SliceIndex, SliceWidth);
        _beeHiveDate.AddFillUnderSeries(vmWithFeatures.DateTimePointViewModel);
        return vmWithFeatures;
    }
}






