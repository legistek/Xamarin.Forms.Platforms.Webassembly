using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Blazor.Interop;

using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

[assembly: Xamarin.Forms.Platform.Blazor.ExportRenderer(
	typeof(Label),
	typeof(Xamarin.Forms.Platform.Blazor.Renderers.LabelRenderer))]

namespace Xamarin.Forms.Platform.Blazor.Renderers
{
	public class LabelRenderer : ViewRenderer<Label>
	{
		bool _needsTextMeasure = true;

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
					_needsTextMeasure = true;
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
			if (_needsTextMeasure)
			{
				_needsTextMeasure = false;
                var t = XFUilities.MeasureText(
                    this.Element.Text,
                    this.Element.FontFamily,
                    this.Element.FontSize);
                t.ContinueWith(t =>
				{
					this.DesiredSize = new Size(t.Result, this.Element.FontSize * this.ActualLineHeight);
					this.Element.NativeSizeChanged();
                    this.InvalidateRender();
				});
				return new Size(
                    this.Element.FontSize * 0.75 * this.Element.Text.Length,   // just a guess
                    this.Element.FontSize);
			}
			else
			{
				return this.DesiredSize;
			}
		}

		protected override void RenderContent(RenderTreeBuilder builder)
		{
			base.RenderContent(builder);
			builder.AddContent(0, this.Element.Text);
		}
	}
}
