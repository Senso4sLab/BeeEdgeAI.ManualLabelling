using BeeEdgeAI.ManualLabelling.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.Services;

public class FileRepository : IRepository
{   
    public async Task<IEnumerable<T>> GetAllAsync<T>(string fileName)
    {
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        
        var result = config.Delimiter;


        using (var reader = new StreamReader(fileName))
        using (var csv = new CsvReader(reader, config))
            return await csv.GetRecordsAsync<T>().ToListAsync();
    }
}


