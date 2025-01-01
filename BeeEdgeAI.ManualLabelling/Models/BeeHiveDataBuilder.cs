using LiveChartsCore.Defaults;

namespace BeeEdgeAI.ManualLabelling.Models;

public class BeeHiveDataBuilder : DateTimePointsBuilder<BeeHiveSample>
{
    public BeeHiveDataBuilder() : base(arg => new DateTimePoint(arg.Timestamp, arg.Mass))
    {

    }
}
