using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

[assembly: Xamarin.Forms.Platform.Webassembly.ExportRenderer(
	typeof(Xamarin.Forms.Layout),
	typeof(Xamarin.Forms.Platform.Webassembly.Renderers.LayoutRenderer))]

namespace Xamarin.Forms.Platform.Webassembly.Renderers
{
	public class LayoutRenderer : ViewRenderer<Layout>
	{
		static HashSet<string> _renderProperties = new HashSet<string>
		{
			nameof(Layout.BackgroundColor),
		};

		protected override bool AffectsRender(string propertyName)
		{
			return base.AffectsRender(propertyName) ||
				_renderProperties.Contains(propertyName);
		}

		protected override void SetBasicStyles()
		{
			base.SetBasicStyles();
			this.Styles["background"] = Element.BackgroundColor.ToHTMLColor();
		}

		protected override void RenderContent(RenderTreeBuilder builder)
		{
			base.RenderContent(builder);
			var elementController = this.Element as IElementController;
			if (elementController == null)
				return;
			foreach ( Element child in elementController.LogicalChildren )
			{
				var view = child as VisualElement;
				if (view == null)
					continue;
				RenderChild(builder, view);
			}
		}
	}
}
