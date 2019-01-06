using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.Blazor
{
	public static class ColorExtensions
	{
		public static string ToHTMLColor (this Color c)
		{
			if (c.IsDefault)
				return "unset";
			byte r = (byte)(c.R * 255.0);
			byte g = (byte)(c.G * 255.0);
			byte b = (byte)(c.B * 255.0);
			return $"#{r.ToString("X2")}{g.ToString("X2")}{b.ToString("X2")}";
		}
	}
}
