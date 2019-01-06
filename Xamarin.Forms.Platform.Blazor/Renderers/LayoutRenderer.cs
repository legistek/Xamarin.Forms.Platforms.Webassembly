using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Blazor.RenderTree;

[assembly: Xamarin.Forms.Platform.Blazor.ExportRenderer(
	typeof(Xamarin.Forms.Layout),
	typeof(Xamarin.Forms.Platform.Blazor.Renderers.LayoutRenderer))]

namespace Xamarin.Forms.Platform.Blazor.Renderers
{
	public class LayoutRenderer : ViewRenderer<Layout>
	{
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
