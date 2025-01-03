using BeeEdgeAI.ManualLabelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class FeaturesStorage
{
    public bool IsEmpty =>
        !GetLabeledFeatures().Any();

    private Dictionary<int, Features> _features = new Dictionary<int, Features>();

    private readonly IRepository _fileRepository;
    public FeaturesStorage(IRepository fileRepository)
    {
        _fileRepository = fileRepository;
    }   
    public async Task LoadFeaturesFromFile(string filePath) => 
        AddToDictionary(await GetFeaturesAsync(filePath));
    private async Task<IEnumerable<Features>> GetFeaturesAsync(string filePath) => 
        await _fileRepository.GetAllAsync<Features>(filePath);
    private void AddToDictionary(IEnumerable<Features> features) => 
        _features = features.Select((index, f) => (key: index, features: f))
            .ToDictionary(tuple => tuple.features, tuple => tuple.key);
    public void Add(LabeledFeatures feature)
    {
        if (_features.ContainsKey(feature.Slice.StartIndex))        
            _features[feature.Slice.StartIndex] = feature;        
        else        
            _features.Add(feature.Slice.StartIndex, feature);
    }
    public LabeledFeatures GetBy(Slice slice) =>
       _features.TryGetValue(slice.StartIndex, out Features? features) 
        ? features is LabeledFeatures labeledFeatures 
        ? labeledFeatures 
        : features.WithDefaultLabel(slice) : 
        throw new Exception($"Features with slice index {slice.StartIndex} does not exist!");

    public async Task SaveLabeledFeatures(FileInfo dest) => 
        await _fileRepository.SaveAsync(GetLabeledFeatures(), dest);

    private IEnumerable<LabeledFeatures> GetLabeledFeatures() =>
        _features.Values.OfType<LabeledFeatures>();            

}
