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
        using (var reader = new StreamReader(fileName))
        using (var csv = new CsvReader(reader, CsvConfig))
            return await csv.GetRecordsAsync<T>().ToListAsync();
    }

    public async Task SaveAsync<T>(IEnumerable<T> items, string fileName)
    {
        using (var writer = new StreamWriter(fileName))
        using (var csv = new CsvWriter(writer, CsvConfig))           
            await csv.WriteRecordsAsync(items);
    }

    private CsvConfiguration CsvConfig =>
        new CsvConfiguration(CultureInfo.InvariantCulture)
        {           
            Delimiter = ";",
        };


   
}


