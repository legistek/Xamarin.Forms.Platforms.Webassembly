using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.Blazor
{
	public interface IVisualElementRenderer : IRegisterable, IDisposable
	{
		//BlazorComponent GetNativeElement();

		VisualElement Element { get; }

		event EventHandler<VisualElementChangedEventArgs> ElementChanged;

		SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);
	}
}
