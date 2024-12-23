using BeeEdgeAI.ManualLabelling.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class DateTimeFeaturesViewModel
{
    public DateTimePointViewModel DateTimePointViewModel { get; }
    public BeeHiveFeatures BeeHiveFeatures { get; }
    public DateTimeFeaturesViewModel(DateTimePointViewModel dateTimePointViewModel, BeeHiveFeatures beeHiveFeatures)
    {
        this.DateTimePointViewModel = dateTimePointViewModel;
        this.BeeHiveFeatures = beeHiveFeatures;
    }

}
