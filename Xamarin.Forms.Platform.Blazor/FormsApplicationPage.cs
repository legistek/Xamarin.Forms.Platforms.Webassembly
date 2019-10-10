using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Xamarin.Forms.Platform.Blazor
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
            OnWindowResize(1920, 1080);  // initial guess
            var sz = await XFUilities.RegisterApplicationPage(this);
            OnWindowResize(sz.Width, sz.Height);
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

        [JSInvokable]
        public void OnWindowResize(double width, double height)
        {
            Application.MainPage.Layout(new Rectangle(0, 0, width, height));
        }
	}
}