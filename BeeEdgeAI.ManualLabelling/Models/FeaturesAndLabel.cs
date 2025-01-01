﻿using BeeEdgeAI.ManualLabelling.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CsvHelper.Configuration.Attributes;

namespace BeeEdgeAI.ManualLabelling.Models;

[ObservableObject]
public partial class FeaturesAndLabel : Features
{
    [Ignore]
    public Slice Slice { get; }

    [ObservableProperty]
    private string label = string.Empty;
  
    public FeaturesAndLabel(Slice slice, Features features, string label) : base(features)
    {       
        this.Slice = slice;
        this.Label = label;
    }

    public FeaturesAndLabel WithLabelValue(string label) =>
       new FeaturesAndLabel(this.Slice, this, label);


}
