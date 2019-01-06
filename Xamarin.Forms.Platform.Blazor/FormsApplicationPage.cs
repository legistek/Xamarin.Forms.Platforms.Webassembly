using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.RenderTree;

namespace Xamarin.Forms.Platform.Blazor
{
	public class FormsApplicationPage : FormsPage
	{
		Application _application;
		Type _applicationType;

		public FormsApplicationPage()
		{
		}

		[Parameter]
		private Type ApplicationType
		{
			get => _applicationType;
			set
			{
				LoadApplication(value);
			}
		}

		public Application Application
		{
			get
			{
				return _application;
			}
			private set
			{
				_application = value;
			}
		}

		protected Platform Platform { get; private set; }

		private async void LoadApplication(Type applicationType)
		{
			Forms.Init();
			_applicationType = applicationType;
			var application = Activator.CreateInstance(applicationType) as Application;

			Application.Current = application;
			application.PropertyChanged += ApplicationOnPropertyChanged;
			this._application = application;

			SetMainPage();

			var sz = await ExampleJsInterop.GetWindowSizeAsync();
			Application.MainPage.Layout(new Rectangle(0, 0, sz.Width, sz.Height));
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