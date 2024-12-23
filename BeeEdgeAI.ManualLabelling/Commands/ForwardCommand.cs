using BeeEdgeAI.ManualLabelling.Models;
using Windows.Security.Cryptography.Certificates;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class ForwardCommand : DelegateCommand
{
    private readonly BeeHiveDisplayManager _beeHiveManager;
    private readonly HistoryBeeHiveFeatureLabel _history;
    public ForwardCommand(BeeHiveDisplayManager beeHiveManager, HistoryBeeHiveFeatureLabel history)
    {
        _beeHiveManager = beeHiveManager;
        _history = history;
    }
    public override bool CanExecute(object? parameter) => true;

    public override void Execute(object? parameter)
    {
        if (parameter is MainWindow mainWindow )
        {          
            _history.Add(_beeHiveManager.SliceIndex, new BeeHiveFeatureLabel(mainWindow.ViewModel.BeeHiveFeatures, mainWindow.ViewModel.LabelState));
            mainWindow.ViewModel.SaveCommand.RaiseCanExecuteChanged();

            if (_beeHiveManager.CanGetNextSlice)
            {
                var vm = _beeHiveManager.GetNextSlice();
                mainWindow.ViewModel.SlicedBeeHiveDateTime = vm.DateTimePointViewModel;
                mainWindow.ViewModel.BeeHiveFeatures = vm.BeeHiveFeatures;
                var beeHaveFeatureLabel = _history.Get(_beeHiveManager.SliceIndex);
                mainWindow.ViewModel.LabelState = _history.Get(_beeHiveManager.SliceIndex) is BeeHiveFeatureLabel hiveFeatureLabel ? hiveFeatureLabel.StateLabel : null;
            }
        }
    }
}
