using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Xamarin.Forms.Platform.Webassembly
{
	public class FormsPage : FormsComponent
	{
		VisualElement _nativeElement;
		protected override VisualElement GetElement()
		{
			return _nativeElement;
		}

		protected override void SetElement(VisualElement element)
		{
			_nativeElement = element;
		}

		protected override void RenderContent(RenderTreeBuilder builder)
		{
			RenderChild(builder, (_nativeElement as ContentPage).Content);
		}
	}
}
