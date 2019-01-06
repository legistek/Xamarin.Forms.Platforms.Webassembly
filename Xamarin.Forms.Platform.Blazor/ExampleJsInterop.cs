using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.Blazor
{
    public class ExampleJsInterop
    {
		[JSInvokable]
		public static async Task ResizeMainWindowAsync(double width, double height)
		{
			Application.Current.MainPage.Layout(new Rectangle(0, 0, width, height));
		}

        public static Task<string> Prompt(string message)
        {
            // Implemented in exampleJsInterop.js
            return JSRuntime.Current.InvokeAsync<string>(
                "exampleJsFunctions.showPrompt",
                message);
        }

		public static async Task<double> MeasureTextAsync(string text, string fontFamily, double fontSize)
		{
			var width = await JSRuntime.Current.InvokeAsync<string>(
				"exampleJsFunctions.measureText",
				text,
				$"{fontSize}px {fontFamily}");

			double w = 1;
			double.TryParse(width, out w);
			return w;
		}

		public static async Task<Size> GetWindowSizeAsync()
		{
			var szString = await JSRuntime.Current.InvokeAsync<string>(
				"exampleJsFunctions.getWindowSize");
			if (string.IsNullOrEmpty(szString))
				return default(Size);

			string[] sizes = szString.Split(',');

			double width = 1, height = 1;
			double.TryParse(sizes[0], out width);
			double.TryParse(sizes[1], out height);
			return new Size(width, height);
		}
    }
}
