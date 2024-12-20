using BeeEdgeAI.ManualLabelling.Commands;
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

	public OpenRawFileCommand OpenFileCommand { get; set; }

    public MainViewModel()
    {
        this.Title = "Manual labelling data for supervise learning";
        this.OpenFileCommand = new OpenRawFileCommand();		
    }

}
