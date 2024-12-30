

namespace BeeEdgeAI.ManualLabelling.Models;

public class InputFiles
{    
    public FileInfo RawData { get; }    
    public FileInfo Features { get; }  

    public InputFiles(FileInfo rawData, FileInfo features)
    {
        RawData = rawData;
        Features = features;
    }
}
