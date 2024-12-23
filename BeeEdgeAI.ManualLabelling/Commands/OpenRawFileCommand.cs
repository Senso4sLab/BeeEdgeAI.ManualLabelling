using BeeEdgeAI.ManualLabelling.Interfaces;
using BeeEdgeAI.ManualLabelling.Models;
using BeeEdgeAI.ManualLabelling.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class OpenRawFileCommand : DelegateCommand
{  
    
    private readonly BeeHiveDisplayManager _beeHiveManager;
    public OpenRawFileCommand(BeeHiveDisplayManager beeHiveManager)
    {
        _beeHiveManager = beeHiveManager;
    }

    public override bool CanExecute(object? parameter) => 
        true;

    public async override void Execute(object? parameter)
    {
        var mainWindow = parameter as MainWindow;

        if (mainWindow is null)
            return;
       
        if(await OpenPickerAndPickFileAsync(parameter) is StorageFile storageFile )
        {
            mainWindow.ViewModel.FileDirectory = RemoveFileNameFromPath(storageFile);            
            await mainWindow.ViewModel.BeeHiveDataTime.AddDataFromFile(storageFile.Path);
            _beeHiveManager.ResetSliceIndex();
            mainWindow.ViewModel.SlicedBeeHiveDateTime = _beeHiveManager.GetSlice.DateTimePointViewModel; 
        }
        else
            mainWindow.ViewModel.FileDirectory  = string.Empty;        
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



    private string RemoveFileNameFromPath(StorageFile storageFile)
    {
        int index = storageFile.Path.LastIndexOf(storageFile.Name);     
        
        return index == -1 ? storageFile.Path :  storageFile.Path.Remove(index);
    }
}

