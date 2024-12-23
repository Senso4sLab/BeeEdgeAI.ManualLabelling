using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class BeeHiveFeatures
{
    public double Sd { get; set; }  
    public double SdGradient { get; set; }
    public double SumPosNegGradient { get; set; }
    public double WeightedSumDiffPosNegGradient { get; set; }
}

public class BeeHiveVisualData
{
    public List<BeeHiveFeatures> Features { get; set; } = new List<BeeHiveFeatures>();
    public List<BeeHiveSample> RawData { get; set; } = new List<BeeHiveSample>();


}



