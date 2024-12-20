using BeeEdgeAI.ManualLabelling.Commands;
using BeeEdgeAI.ManualLabelling.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace BeeEdgeAI.ManualLabelling.ViewModels;

public class MainViewModel : BaseViewModel
{
	private string openFileName = string.Empty;
	public string OpenFileName
	{
		get => openFileName;
		set 
		{ 
			if(openFileName != value)
			{
				openFileName = value;
				RaiseOnPropertyChanged();
			}			
		}
	}
	public OpenRawFileCommand OpenRawFileCommand { get; set; }
    public OpenFeatureFileCommand OpenFeatureFileCommand { get; set; }	
    public MainViewModel(OpenRawFileCommand openFileCommand)
    {
		
        Title = "Manual labelling data for supervise learning";
		OpenRawFileCommand = openFileCommand;
        OpenFeatureFileCommand = new OpenFeatureFileCommand();
    }


}
