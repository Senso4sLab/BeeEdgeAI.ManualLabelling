using BeeEdgeAI.ManualLabelling.Interfaces;
using LiveChartsCore.Defaults;
using System;

namespace BeeEdgeAI.ManualLabelling.Models;

public class BeeHiveDataBuilder : DateTimePointsBuilder<BeeHiveSample>
{
    public BeeHiveDataBuilder(IRepository repository) : base(repository)
    {

    }

    protected override Func<BeeHiveSample, DateTimePoint> Mapping => arg => new DateTimePoint(arg.Timestamp, arg.Mass);

  
}
