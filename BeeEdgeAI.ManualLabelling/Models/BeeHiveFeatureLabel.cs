using System;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class BeeHiveFeatureLabel
{

    public double Sd { get; set; }
    public double SdGradient { get; set; }
    public double SumPosNegGradient { get; set; }
    public double WeightedSumDiffPosNegGradient { get; set; }
    public string? StateLabel { get; set; }

    public BeeHiveFeatureLabel(BeeHiveFeatures features, string? stateLabel)
    {
        this.Sd = features.Sd;
        this.SdGradient = features.SdGradient;
        this.SumPosNegGradient = features.SumPosNegGradient;
        this.WeightedSumDiffPosNegGradient = features.WeightedSumDiffPosNegGradient;
        this.StateLabel = stateLabel;
    }
}
