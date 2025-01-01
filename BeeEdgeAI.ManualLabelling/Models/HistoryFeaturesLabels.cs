using BeeEdgeAI.ManualLabelling.Interfaces;
using BeeEdgeAI.ManualLabelling.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class HistoryFeaturesLabels
{
    public bool IsEmpty =>
        GetAllWithValidLabel().Any();

    private Dictionary<int, FeaturesAndLabel> _featuresAndLabels = new Dictionary<int, FeaturesAndLabel>();

    private readonly IRepository _fileRepository;
    public HistoryFeaturesLabels(IRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }
    public void Add(FeaturesAndLabel featuresLabel)
    {
        if (_featuresAndLabels.ContainsKey(featuresLabel.Slice.StartIndex))        
            _featuresAndLabels[featuresLabel.Slice.StartIndex] = featuresLabel;        
        else        
            _featuresAndLabels.Add(featuresLabel.Slice.StartIndex, featuresLabel);
    }

    public FeaturesAndLabel? Get(Slice slice) =>
       _featuresAndLabels.TryGetValue(slice.StartIndex, out FeaturesAndLabel? featureLabel) ? featureLabel: null;
    

    public async Task Save(FileInfo dest)
    {
        await _fileRepository.SaveAsync(GetAllWithValidLabel(), dest);
        _featuresAndLabels.Clear();
    }

    private IEnumerable<FeaturesAndLabel> GetAllWithValidLabel() =>
        _featuresAndLabels.Values
            .Where(bhf => bhf.Label != null);
            

}
