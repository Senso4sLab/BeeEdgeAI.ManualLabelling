using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Commands;

public class OpenFeatureFileCommand : DelegateCommand
{
    public override bool CanExecute(object? parameter) => 
        true;

    public override void Execute(object? parameter)
    {
       
    }
}
