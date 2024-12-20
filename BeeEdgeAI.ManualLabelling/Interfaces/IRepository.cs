using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Interfaces;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync(string fileName);

}
