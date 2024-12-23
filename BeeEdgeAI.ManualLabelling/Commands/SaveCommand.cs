using BeeEdgeAI.ManualLabelling.Interfaces;
using BeeEdgeAI.ManualLabelling.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class SaveCommand : DelegateCommand
{
    private readonly IRepository _repository;
    private readonly HistoryBeeHiveFeatureLabel _history;

    public SaveCommand(IRepository repository, HistoryBeeHiveFeatureLabel history)
    {
        _repository = repository;
        _history = history;
    }
    public override bool CanExecute(object? parameter) =>
        _history.Length > 0;  

    public async override void Execute(object? parameter)
    {
        var filePath = parameter as string;

        if (filePath is null)
            return;

        var records = _history.GetAllWithValidLabelState;

        if (records.Any())
        {
            await _repository.SaveAsync(records, filePath);
            _history.Clear();
        }        
    }
}
