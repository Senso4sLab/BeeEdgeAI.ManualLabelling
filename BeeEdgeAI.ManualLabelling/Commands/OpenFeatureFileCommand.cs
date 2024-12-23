using BeeEdgeAI.ManualLabelling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Pickers;
using Windows.Storage;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class OpenFeatureFileCommand : DelegateCommand
{
    private readonly BeeHiveDisplayManager _beeHiveManager;
    public OpenFeatureFileCommand(BeeHiveDisplayManager beeHiveManager)
    {
        _beeHiveManager = beeHiveManager;
    }
    public override bool CanExecute(object? parameter) => 
        true;

    public async override void Execute(object? parameter)
    {
        if (await OpenPickerAndPickFileAsync(parameter) is StorageFile storageFile && parameter is MainWindow mainWindow)
        {           
            await mainWindow.ViewModel.BeeHiveDataTime.AddFeaturesFromFile(storageFile.Path);
            mainWindow.ViewModel.BeeHiveFeatures = _beeHiveManager.GetSlice.BeeHiveFeatures;
        }
       
    }

    private async Task<StorageFile?> OpenPickerAndPickFileAsync(object? parameter)
    {
        var openPicker = new FileOpenPicker();
        // Retrieve the window handle (HWND) of the current WinUI 3 window.
        var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(parameter);
        // Initialize the file picker with the window handle (HWND).
        WinRT.Interop.InitializeWithWindow.Initialize(openPicker, hWnd);
        // Set options for your file picker
        openPicker.ViewMode = PickerViewMode.Thumbnail;
        openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        openPicker.FileTypeFilter.Add(".txt");
        // Open the picker for the user to pick a file
        return await openPicker.PickSingleFileAsync();
    }
}
