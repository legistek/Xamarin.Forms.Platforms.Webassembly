using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Webassembly.Interop;

using Microsoft.AspNetCore.Components.Rendering;

[assembly: Xamarin.Forms.Platform.Webassembly.ExportRenderer(
	typeof(Label),
	typeof(Xamarin.Forms.Platform.Webassembly.Renderers.LabelRenderer))]

namespace Xamarin.Forms.Platform.Webassembly.Renderers
{
	public class LabelRenderer : ViewRenderer<Label>
	{
		static HashSet<string> _renderProperties = new HashSet<string>
		{
			nameof(Label.BackgroundColor),
			nameof(Label.FontFamily),
			nameof(Label.FontSize),
			nameof(Label.LineHeight),
			nameof(Label.Text),
			nameof(Label.TextColor),
		};

		protected override bool AffectsRender(string propertyName)
		{
			return base.AffectsRender(propertyName) ||
				_renderProperties.Contains(propertyName);
		}

		protected override void OnElementPropertyChanged(
			object sender,
			PropertyChangedEventArgs e)
		{
			switch (e.PropertyName)
			{
				case nameof(Label.Text):
				case nameof(Label.FontSize):
				case nameof(Label.FontFamily):
				case nameof(Label.LineHeight):
					this.Element.NativeSizeChanged();
					break;
			}
			base.OnElementPropertyChanged(sender, e);
		}

		private double ActualLineHeight => this.Element.LineHeight == -1 ? 1 : this.Element.LineHeight;

		protected override void SetBasicStyles()
		{
			base.SetBasicStyles();

			this.Styles["line-height"] = this.ActualLineHeight.ToString();

			this.Styles["background-color"] = Element.BackgroundColor.ToHTMLColor();
			this.Styles["color"] = this.Element.TextColor.ToHTMLColor();

			if (string.IsNullOrEmpty(this.Element.FontFamily))
				this.Styles["font-family"] = "unset";
			else
				this.Styles["font-family"] = this.Element.FontFamily;

			double fs = this.Element.FontSize;
			if (!double.IsNaN(fs))
				this.Styles["font-size"] = fs.ToString() + "px";			
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			return XFUtilities.MeasureText(
				this.Element.Text,
				this.Element.FontFamily,
				this.Element.FontSize,
				(double.IsInfinity(availableSize.Width) || availableSize.Width == -1) 
					? 0 
					: availableSize.Width);		
		}

		protected override void RenderContent(RenderTreeBuilder builder)
		{
			base.RenderContent(builder);
			builder.AddContent(0, this.Element.Text);
		}
	}
}
