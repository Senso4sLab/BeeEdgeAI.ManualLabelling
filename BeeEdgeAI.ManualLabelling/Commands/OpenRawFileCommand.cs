using BeeEdgeAI.ManualLabelling.Interfaces;
using BeeEdgeAI.ManualLabelling.Models;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class OpenRawFileCommand : DelegateCommand
{
    private string filePath = string.Empty;
    public string FilePath
    {
        get => filePath;
        set
        {
            if(filePath != value)
            {
                filePath = value;
                RaiseOnPropertyChanged();
            }
        }
    }

    public IEnumerable<BeeHiveRawData> BeeHiveRawData { get;set; } = new List<BeeHiveRawData>();

    private readonly IRepository _repository;
    public OpenRawFileCommand(IRepository repository)
    {
      _repository = repository;
    }

    public override bool CanExecute(object? parameter) => 
        true;

    public async override void Execute(object? parameter)
    {
        if(await OpenPickerAndPickFileAsync(parameter) is StorageFile storageFile)
        {
            FilePath = RemoveFileNameFromPath(storageFile);
            BeeHiveRawData = await _repository.GetAllAsync<BeeHiveRawData>(storageFile.Path);
        }
        else
            FilePath = string.Empty;


        
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
