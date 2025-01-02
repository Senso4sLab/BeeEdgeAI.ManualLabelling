using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;


namespace BeeEdgeAI.ManualLabelling.Models;

public class FileInfo
{
    public string Name { get;}    
    public string Path { get;}
    public string Type { get;}

    public FileInfo(StorageFile storageFile)
    {
        this.Name = storageFile.Name;
        this.Path = storageFile.Path;
        this.Type = storageFile.FileType;
    }  

    public static async Task<FileInfo> Create(string filePath) =>
        new FileInfo(await StorageFile.GetFileFromPathAsync(filePath));

    private FileInfo(string name, string path, string type)
    {
        this.Name = name;
        this.Path = path;
        this.Type = type;
    }
    public FileInfo SetFileName(string fileName) => 
        new FileInfo(fileName, this.Path.Replace(this.Name, fileName + this.Type), this.Type);

    public bool Exists => 
        System.IO.File.Exists(this.Path);
    
}
