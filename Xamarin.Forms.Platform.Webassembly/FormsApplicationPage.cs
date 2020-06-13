using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Xamarin.Forms.Platform.Webassembly.Interop;

namespace Xamarin.Forms.Platform.Webassembly
{
	public class FormsApplicationPage : FormsPage
	{
		Application _application;

		public FormsApplicationPage()
		{
		}

        [Parameter]
		public Application Application
		{
			get
			{
				return _application;
			}
			set
			{
                if (_application == null)
                    LoadApplication(value);
			}
		}

		protected Platform Platform { get; private set; }

		private void LoadApplication(Application application)
		{			
			Application.Current = application;
			application.PropertyChanged += ApplicationOnPropertyChanged;
			this._application = application;
			SetMainPage();            
		}

        protected override async Task OnInitializedAsync()
        {
			XFUtilities.ForceResizeMainPage();
            await base.OnInitializedAsync();           
        }

        void ApplicationOnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (args.PropertyName == "MainPage")
				SetMainPage();
		}

		void SetMainPage()
		{
			if (Platform == null)
				Platform = new Platform(this);

			Platform.SetPage(Application.MainPage);
			this.SetElement(Application.MainPage);
		}
	}
}