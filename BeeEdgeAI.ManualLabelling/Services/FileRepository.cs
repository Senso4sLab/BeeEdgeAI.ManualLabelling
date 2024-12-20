using BeeEdgeAI.ManualLabelling.Interfaces;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Services;

public class FileRepository<T> : IRepository<T>
{   
    public async Task<IEnumerable<T>> GetAllAsync(string fileName)
    {        
        using (var reader = new StreamReader(fileName))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            return await csv.GetRecordsAsync<T>().ToListAsync();
    }

   
}
