using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Models;

public class BeeHiveData : IEntityBase
{
    public DateTime TimeStamp { get ; set ; }
    public double Mass { get; set ; }   
}
