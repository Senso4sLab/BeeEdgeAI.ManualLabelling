using BeeEdgeAI.ManualLabelling.Commands;
using BeeEdgeAI.ManualLabelling.Interfaces;
using BeeEdgeAI.ManualLabelling.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Dispatching;
using System;

using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
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
    private FeaturesAndLabel? featuresAndLabel;

    [ObservableProperty]
    private DateTimePointsVM dateTimePoints;

    [ObservableProperty]
    private SlicedDateTimePointsVM? slicedDateTimePoints;
    public Action FileSelectorControl { get; set; }   

    private InputFiles _files;
    private IRepository _repository;
    private BeeHiveDataBuilder _beeHiveDataBuilder;
    private SliceManager _sliceManager;

    private HistoryFeaturesLabels _history;
    private Slice _slice = new Slice(-1, 10);
    public MainViewModel(IRepository repository, BeeHiveDataBuilder beeHiveDataBuilder, HistoryFeaturesLabels history)
    {       
        _repository = repository;   
        _beeHiveDataBuilder = beeHiveDataBuilder;
        _history = history;

        NotifyCanExecuteSliceManagerCommands();
    }

    [RelayCommand]
    private void ShowFileSelectorControl()
    {
        FileSelectorControl?.Invoke();
    }

    [RelayCommand(CanExecute =nameof(CanExecuteNextSliceCommand))]
    private void NextSlice(string label)
    {      
        _history.Add(FeaturesAndLabel!.WithLabelValue(label));        

        if(_sliceManager.GetNextSlice() is SlicedDateTimePointsVM slicePoints)
        {             
            FeaturesAndLabel = GetFeaturesAndLabelBy(slicePoints.Slice);
            SlicedDateTimePoints = slicePoints;
            SlicedDateTimePoints!.SetTitle(FeaturesAndLabel!.Label);
        }
        NotifyCanExecuteSliceManagerCommands();
    }

    private bool CanExecuteNextSliceCommand() =>
        _sliceManager is SliceManager sliceManager && sliceManager.CanGetNextSlice;

      

    [RelayCommand(CanExecute = nameof(CanExecutePreviusSliceCommand))]
    private void PreviusSlice()
    { 
        if (_sliceManager.GetPreviousSlice() is SlicedDateTimePointsVM slicePoints)
        {
            FeaturesAndLabel = GetFeaturesAndLabelBy(slicePoints.Slice);        
            SlicedDateTimePoints = slicePoints;
            SlicedDateTimePoints!.SetTitle(FeaturesAndLabel!.Label);
        }

        NotifyCanExecuteSliceManagerCommands();
    }

    private FeaturesAndLabel? GetFeaturesAndLabelBy(Slice slice) =>
        _history.Get(slice) ?? _sliceManager.FeaturesWithDefaultLabel(slice);



    private bool CanExecutePreviusSliceCommand() =>
        _sliceManager is SliceManager sliceManager && sliceManager.CanGetPreviousSlice;

    [RelayCommand]
    private async Task ProceedSelectedFiles()
    {
        _files  = await CreateInputFilesAsync();

        var rawData  = await _repository.GetAllAsync<BeeHiveSample>(RawDataFilePath);
        var features = await _repository.GetAllAsync<Features>(FeaturesFilePath);        

        DateTimePoints = _beeHiveDataBuilder.WithDateTimeXAxis().WithLineSeries(rawData).Build();       

        _sliceManager = new SliceManager(_slice, DateTimePoints, features);

        if (_sliceManager.CanGetNextSlice)           
        {
            SlicedDateTimePoints = _sliceManager.GetNextSlice();
            FeaturesAndLabel = _sliceManager.FeaturesWithDefaultLabel(SlicedDateTimePoints!.Slice);
        }

        NotifyCanExecuteSliceManagerCommands();
    }

    public void NotifyCanExecuteSliceManagerCommands()
    {
        PreviusSliceCommand.NotifyCanExecuteChanged();
        NextSliceCommand.NotifyCanExecuteChanged();
        SaveCommand.NotifyCanExecuteChanged();
    }

    [RelayCommand(CanExecute =nameof(CanExecuteSaveCommand))]
    private async void Save()
    {
        FileInfo destFile = _files.RawData.AppendBeforeFileName("labeled_");      
        await _history.Save(destFile);

        NotifyCanExecuteSliceManagerCommands();
    }

    private bool CanExecuteSaveCommand() =>
       !_history.IsEmpty && !string.IsNullOrEmpty(_files.RawData.Path);

    private async Task<InputFiles> CreateInputFilesAsync()
    {
        var featureFileInfo = await this.CreateFileInfoAsync(this.FeaturesFilePath);
        var rawDataFileInfo = await this.CreateFileInfoAsync(this.RawDataFilePath);
        return new InputFiles(rawDataFileInfo, featureFileInfo);
    }

    private async Task<FileInfo> CreateFileInfoAsync(string filePath)
    {
        var storageFile = await StorageFile.GetFileFromPathAsync(filePath);
        return new FileInfo(storageFile);
    }

}






