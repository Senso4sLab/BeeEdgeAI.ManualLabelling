using LiveChartsCore.Defaults;
using System.Collections.Generic;
using System.Linq;
using BeeEdgeAI.ManualLabelling.Models;
using BeeEdgeAI.ManualLabelling.Interfaces;
using Windows.Storage;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.ViewModels;

public class BeeHiveDateTimeViewModel : DateTimePointViewModel
{ 
    private List<BeeHiveFeatures> _beeHiveFeatures { get; set; } = new List<BeeHiveFeatures>();
    public BeeHiveFeatures GetFeatures(int sliceIndex) =>
        sliceIndex < _beeHiveFeatures.Count ?
            _beeHiveFeatures[sliceIndex] : new BeeHiveFeatures();


    public DateTimeFeaturesViewModel GetViewModelWithFeaturesForSlice(int sliceIndex, int sliceWidth) =>
        new DateTimeFeaturesViewModel(GetSlice(sliceIndex, sliceWidth), GetFeatures(sliceIndex));


    private readonly IRepository _repository;
    public BeeHiveDateTimeViewModel(IRepository repository) : base()
    {
        _repository = repository;
    }
    public async Task AddDataFromFile(string fileName)
    {
        var beeHiveRawData = await _repository.GetAllAsync<BeeHiveSample>(fileName);
        AddPointToLineSeries(MappingFrom(beeHiveRawData));
    }
    public async Task AddFeaturesFromFile(string fileName)
    {
        _beeHiveFeatures = new List<BeeHiveFeatures>(await _repository.GetAllAsync<BeeHiveFeatures>(fileName));      
    }
    private List<DateTimePoint> MappingFrom(IEnumerable<BeeHiveSample> samples) =>
        new List<DateTimePoint>(samples.Select(sample => new DateTimePoint() { DateTime = sample.Timestamp, Value = sample.Mass }));

}
