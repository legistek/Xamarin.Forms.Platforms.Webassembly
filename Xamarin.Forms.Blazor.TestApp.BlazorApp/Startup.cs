using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Xamarin.Forms.Blazor.TestApp.BlazorApp
{
	public class Startup
	{
		public void Configure(IBlazorApplicationBuilder app)
		{
			app.AddComponent<Xamarin.Forms.Blazor.TestApp.BlazorApp.App>("app");
		}
	}
}
