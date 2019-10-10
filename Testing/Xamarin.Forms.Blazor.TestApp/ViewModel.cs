using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Blazor.TestApp
{
	[Preserve(AllMembers = true)]
	public class ViewModel : INotifyPropertyChanged
	{
		#region string StatusText property
		private string _StatusText = "No click yet";
		public string StatusText
		{
			get
			{
				return _StatusText;
			}
			set
			{
				if (_StatusText != value)
				{
					_StatusText = value;
					OnPropertyChanged();
				}
			}
		}
		#endregion

		#region ICommand Clicked Command

		private Command _ClickedCommand;

		public ICommand ClickedCommand
		{
			get
			{
				return _ClickedCommand ?? (_ClickedCommand = new Command(
					() =>
					{
						this.StatusText = "Clicked!";
					}));
			}
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string property = null)
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		}
	}
}
