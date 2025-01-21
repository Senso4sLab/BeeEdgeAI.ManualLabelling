using BeeEdgeAI.ManualLabelling.Interfaces;
using BeeEdgeAI.ManualLabelling.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Threading.Tasks;
using Windows.Storage;


namespace BeeEdgeAI.ManualLabelling.ViewModels;

public partial class MainViewModel : ObservableObject
{   

    [ObservableProperty]
    private string title = "Manual labelling data for supervise learning";

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AreValidPaths))]
    private string rawDataFilePath = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(AreValidPaths))]
    private string featuresFilePath = string.Empty;

    public bool AreValidPaths =>
        !string.IsNullOrWhiteSpace(this.FeaturesFilePath) && !string.IsNullOrWhiteSpace(this.RawDataFilePath);

    [ObservableProperty]
    private LabeledFeatures labeledFeatures;

    [ObservableProperty]
    private DateTimePointsVM dateTimePoints;

    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(NextSliceCommand))]
    [NotifyCanExecuteChangedFor(nameof(PreviusSliceCommand))]
    [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
    private SlicedDateTimePointsVM? slicedDateTimePoints;
    public Action FileSelectorControl { get; set; }    
    
    private BeeHiveDataBuilder _beeHiveDataBuilder;
    private DateTimePointSlicer _slicer;

    private FeaturesStorage _featuresStorage;
    private int _sliceWidth = 10;
    public MainViewModel(BeeHiveDataBuilder beeHiveDataBuilder, FeaturesStorage featureStorage)
    {         
        _beeHiveDataBuilder = beeHiveDataBuilder;
        _featuresStorage = featureStorage;       
    }

    [RelayCommand]
    private void ShowFileSelectorControl()
    {
        FileSelectorControl?.Invoke();
    }

    [RelayCommand(CanExecute =nameof(CanExecuteNextSliceCommand))]
    private void NextSlice(string label)
    {      
        _featuresStorage.Add(LabeledFeatures.SetLabel(label));       

        if (_slicer.GetNextSlice() is SlicedDateTimePointsVM slicedDateTimePoints)
        {
            ShowLabeledFeaturesBy(slicedDateTimePoints.Slice);
            ShowSlicedDateTimePoints(slicedDateTimePoints, LabeledFeatures.Label);
        }       
    }

    private bool CanExecuteNextSliceCommand() =>
        _slicer?.CanGetNextSlice == true;      

    [RelayCommand(CanExecute = nameof(CanExecutePreviusSliceCommand))]
    private void PreviusSlice()
    { 
        if (_slicer.GetPreviousSlice() is SlicedDateTimePointsVM slicedDateTimePoints)
        {
            ShowLabeledFeaturesBy(slicedDateTimePoints.Slice);
            ShowSlicedDateTimePoints(slicedDateTimePoints, LabeledFeatures.Label);
        }       
    }

    private void ShowLabeledFeaturesBy(Slice slice)
    {
        LabeledFeatures = _featuresStorage.GetBy(slice);
    }

    private void ShowSlicedDateTimePoints(SlicedDateTimePointsVM slicedDateTimePoints, string title)
    {
        slicedDateTimePoints.SetTitle(title);
        SlicedDateTimePoints = slicedDateTimePoints;        
    }
        

    private bool CanExecutePreviusSliceCommand() =>
        _slicer?.CanGetPreviousSlice == true;

    [RelayCommand]
    private async Task ProceedSelectedFiles()
    {
        DateTimePoints = await _beeHiveDataBuilder.WithDateTimeXAxis().WithLineSeries(RawDataFilePath).BuildAsync();

        await _featuresStorage.LoadFeaturesFromFile(FeaturesFilePath);

        _slicer = new DateTimePointSlicer(DateTimePoints, _sliceWidth);

        if (_slicer.CanGetNextSlice)           
        {
            SlicedDateTimePoints = _slicer.GetNextSlice();
            ShowLabeledFeaturesBy(SlicedDateTimePoints!.Slice);
        }      
    }  

    [RelayCommand(CanExecute =nameof(CanExecuteSaveCommand))]   
    private async Task Save()
    {       
        var destFile = await FileInfo.Create(RawDataFilePath);
        await SaveLabeledFeaturesAsync(destFile, $"labeled_{destFile.Name}");       
    }

    private async Task SaveLabeledFeaturesAsync(FileInfo fileInfo, string fileName)
    {        
        fileInfo = fileInfo.SetFileName(fileName);
        await _featuresStorage.SaveLabeledFeatures(fileInfo);
    }

    private bool CanExecuteSaveCommand() =>
       !_featuresStorage.IsEmpty && !string.IsNullOrEmpty(RawDataFilePath);

   

}






