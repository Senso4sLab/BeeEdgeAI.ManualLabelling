using BeeEdgeAI.ManualLabelling.Models;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class BackwardCommand : DelegateCommand
{
    private readonly BeeHiveDisplayManager _beeHiveManager;
    private readonly HistoryBeeHiveFeatureLabel _history;
    public BackwardCommand(BeeHiveDisplayManager beeHiveManager, HistoryBeeHiveFeatureLabel history)
    {
        _beeHiveManager = beeHiveManager;
        _history = history;
    }
    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        if (parameter is MainWindow mainWindow && _beeHiveManager.CanGetPreviousSlice)
        {
            var vm = _beeHiveManager.GetPreviousSlice();
            mainWindow.ViewModel.SlicedBeeHiveDateTime = vm.DateTimePointViewModel;
            mainWindow.ViewModel.BeeHiveFeatures = vm.BeeHiveFeatures;
            var beeHaveFeatureLabel = _history.Get(_beeHiveManager.SliceIndex);
            mainWindow.ViewModel.LabelState = _history.Get(_beeHiveManager.SliceIndex) is BeeHiveFeatureLabel hiveFeatureLabel ? hiveFeatureLabel.StateLabel : null;            
        }
    }
}
