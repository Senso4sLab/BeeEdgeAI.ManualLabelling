using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BeeEdgeAI.ManualLabelling.ViewModels;

public class BaseViewModel : INotifyPropertyChanged
{
	private string title = string.Empty;

	public string Title
	{
		get => title;
		set
		{
			if (title != value)
			{
				title = value;
				RaiseOnPropertyChanged();
			}
		}
	}


	public event PropertyChangedEventHandler? PropertyChanged;

	protected virtual void RaiseOnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public virtual Task LoadAsync(object? parameter)
	{
		return Task.CompletedTask;
	}
}
