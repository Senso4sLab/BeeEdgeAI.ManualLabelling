using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BeeEdgeAI.ManualLabelling.Models;

public class HistoryBeeHiveFeatureLabel
{
    private Dictionary<int, BeeHiveFeatureLabel> _featuresLabel = new Dictionary<int, BeeHiveFeatureLabel>();

    public int Length => _featuresLabel.Count;
    public void Add(int sliceIndex, BeeHiveFeatureLabel featureLabel)
    {
        if (_featuresLabel.ContainsKey(sliceIndex))        
            _featuresLabel[sliceIndex] = featureLabel;        
        else        
            _featuresLabel.Add(sliceIndex, featureLabel);
    }

    public BeeHiveFeatureLabel? Get(int sliceIndex) =>
       _featuresLabel.TryGetValue(sliceIndex, out BeeHiveFeatureLabel? featureLabel) ? featureLabel: null;

    public void Clear()
    {
        _featuresLabel.Clear();
    }

    public IEnumerable<BeeHiveFeatureLabel> GetAllWithValidLabelState => 
        _featuresLabel.Values.Where(bhf =>bhf.StateLabel != null).ToList();

}
