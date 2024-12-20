using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Interfaces;

public interface IRepository
{
    Task<IEnumerable<T>> GetAllAsync<T>(string fileName);

}
