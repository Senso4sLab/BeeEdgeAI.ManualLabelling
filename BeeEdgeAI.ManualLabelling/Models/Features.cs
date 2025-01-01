using BeeEdgeAI.ManualLabelling.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;

namespace BeeEdgeAI.ManualLabelling.Models;



public class Features
{

    public double Sd { get; set; }  
    public double SdGradient { get; set; }
    public double SumPosNegGradient { get; set; }
    public double WeightedSumDiffPosNegGradient { get; set; }

    public Features()
    {

    }
    protected Features(Features f) : this(f.Sd, f.SdGradient, f.SumPosNegGradient, f.WeightedSumDiffPosNegGradient)
    {
    }
    public Features(double sd, double sdGradient, double sumPosNegGradient, double weightedSumDiffPosNegGradient)
    {
        this.Sd = sd;
        this.SdGradient = sdGradient;
        this.SumPosNegGradient = sumPosNegGradient;
        this.WeightedSumDiffPosNegGradient = weightedSumDiffPosNegGradient;
    }

    public FeaturesAndLabel WithDefaultLabelValue(Slice slice) =>
        new FeaturesAndLabel(slice, this, string.Empty);

    
}





