using System;
using System.Collections.Generic;

using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using BeeEdgeAI.ManualLabelling.Models;
using BeeEdgeAI.ManualLabelling.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;



namespace BeeEdgeAI.ManualLabelling;

[ObservableObject]
public sealed partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; set; }   
    public string OpenFileName { get; set; } = string.Empty;


    [ObservableProperty]
    private string rawDataFilePath = string.Empty;

    [ObservableProperty]
    private string featuresFilePath = string.Empty;


    RelayCommand InputFilesCommand { get; }
  
    public MainWindow(MainViewModel mainViewModel)
    {
        this.InitializeComponent();

        this.ViewModel = mainViewModel;        

        this.ViewModel.InputFilesFunc = async () =>
        {
            if(await ShowContentDialogAsync() && !string.IsNullOrWhiteSpace(RawDataFilePath) && !string.IsNullOrWhiteSpace(FeaturesFilePath))
            {
                return await CreateInputFilesAsync();
            }

            return null;
        };
        
       
    }
    
    private async Task<bool> ShowContentDialogAsync() =>
        await cdInputFiles.ShowAsync() == ContentDialogResult.Primary;   

    private async Task<InputFiles> CreateInputFilesAsync()
    {
        var featureFileInfo = await this.CreateFileInfoAsync(this.FeaturesFilePath);
        var rawDataFileInfo = await this.CreateFileInfoAsync(this.RawDataFilePath);
        return new InputFiles(rawDataFileInfo, featureFileInfo);
    }    

    private async Task<FileInfo> CreateFileInfoAsync(string filePath)
    {
        var storageFile = await StorageFile.GetFileFromPathAsync(this.FeaturesFilePath);
        return new FileInfo(storageFile);
    }





    [RelayCommand]
    private async Task PickRawDataFile()
    {
        RawDataFilePath = await OpenPickerAndPickFileAsync() is StorageFile storageFile ? storageFile.Path : string.Empty;       
    }

    [RelayCommand]
    private async Task PickFeatureFile()
    {
        FeaturesFilePath = await OpenPickerAndPickFileAsync() is StorageFile storageFile ? storageFile.Path : string.Empty;
    }


    private async Task<StorageFile?> OpenPickerAndPickFileAsync()
    {
        var openPicker = new FileOpenPicker();
        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
        // Initialize the file picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        // Set options for your file picker
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        openPicker.FileTypeFilter.Add(".txt");
        // Open the picker for the user to pick a file
        return await openPicker.PickSingleFileAsync();
    }

    private void tbRawData_TextChanged(object sender, TextChangedEventArgs e)
    {

    }
}
