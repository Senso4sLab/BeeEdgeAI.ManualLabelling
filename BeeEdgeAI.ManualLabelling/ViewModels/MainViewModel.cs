using BeeEdgeAI.ManualLabelling.Commands;
using BeeEdgeAI.ManualLabelling.Models;


namespace BeeEdgeAI.ManualLabelling.ViewModels;

public class MainViewModel : BaseViewModel
{
	private string fileName = string.Empty;
	public string FileName
	{
		get => fileName;
		set 
		{ 
			if(fileName != value)
			{
				fileName = value;
				RaiseOnPropertyChanged();
                RaiseOnPropertyChanged(nameof(FilePath));
            }			
		}
	}

    private string fileDirectory = string.Empty;
    public string FileDirectory
    {
        get => fileDirectory;
        set
        {
            if (fileDirectory != value)
            {
                fileDirectory = value;                
                RaiseOnPropertyChanged();
                RaiseOnPropertyChanged(nameof(FilePath));

                
            }
        }
    }

    public string FilePath => this.FileDirectory + this.FileName + ".txt";

    private string? labelState = null;
    public string? LabelState
    {
        get => labelState;
        set
        {
            if (labelState != value)
            {
                labelState = value;
                RaiseOnPropertyChanged();
            }
        }
    }


    private BeeHiveFeatures beeHiveFeatures;

    public BeeHiveFeatures BeeHiveFeatures
    {
        get => beeHiveFeatures;
        set
        {
            if (beeHiveFeatures != value)
            {
                beeHiveFeatures = value;
                RaiseOnPropertyChanged();
            }
        }
    }



    private BeeHiveDateTimeViewModel _beeHiveDataTime;
    public BeeHiveDateTimeViewModel BeeHiveDataTime
	{
		get => _beeHiveDataTime;
		set
		{
			if (_beeHiveDataTime != value)
			{
				_beeHiveDataTime = value;
				RaiseOnPropertyChanged();
			}
		}
	}

    private DateTimePointViewModel _slicedBeeHiveDateTime = DateTimePointViewModel.Empty;
    public DateTimePointViewModel SlicedBeeHiveDateTime
    {
        get => _slicedBeeHiveDateTime;
        set
        {
            if (_slicedBeeHiveDateTime != value)
            {
                _slicedBeeHiveDateTime = value;
                RaiseOnPropertyChanged();
            }
        }
    }



    public ForwardCommand ForwardCommand { get; set; }
    public BackwardCommand BackwardCommand { get; set; }
    public SaveCommand SaveCommand { get; set; }
    public OpenRawFileCommand OpenRawFileCommand { get; set; }
    public OpenFeatureFileCommand OpenFeatureFileCommand { get; set; }	

	
    public MainViewModel(OpenRawFileCommand openFileCommand, OpenFeatureFileCommand openFeatureFileCommand,
        SaveCommand saveCommand,
        ForwardCommand forwardCommand,
		BackwardCommand backwardCommand,		
		BeeHiveDateTimeViewModel beeHiveDataTime)
    {		
        Title = "Manual labelling data for supervise learning";
        _beeHiveDataTime = beeHiveDataTime;
        OpenRawFileCommand = openFileCommand;
		BackwardCommand = backwardCommand;
		ForwardCommand = forwardCommand;
        SaveCommand = saveCommand;
        OpenFeatureFileCommand = openFeatureFileCommand;
    }
}






