using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Blazor;

namespace Xamarin.Forms
{
	public static class Forms
	{
		public static bool IsInitialized { get; private set; }

		public static void Init(IEnumerable<Assembly> rendererAssemblies = null)
		{
			if (IsInitialized)
				return;

			Device.SetTargetIdiom(TargetIdiom.Desktop);
			Device.PlatformServices = new BlazorPlatformServices();

			Registrar.RegisterAll(new[] { typeof(ExportRendererAttribute) });

			Device.SetIdiom(TargetIdiom.Desktop);

			IsInitialized = true;
		}
	}
}
