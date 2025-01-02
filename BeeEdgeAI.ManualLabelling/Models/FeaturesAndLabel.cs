using BeeEdgeAI.ManualLabelling.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace BeeEdgeAI.ManualLabelling.Models;

[ObservableObject]
public partial class LabeledFeatures : Features
{
    [Ignore]
    public Slice Slice { get; }

    [ObservableProperty]
    private string label = string.Empty;
  
    public LabeledFeatures(Slice slice, Features features, string label) : base(features)
    {       
        this.Slice = slice;
        this.Label = label;
    }

    public LabeledFeatures WithLabelValue(string label) =>
       new LabeledFeatures(this.Slice, this, label);


}
