using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.JSInterop;

namespace Xamarin.Forms.Platform.Blazor.Interop
{
    internal class XFUtilities
    {
        IJSRuntime _runtime;

        public XFUtilities(IJSRuntime runtime)
        {
            _runtime = runtime;
        }

        public async Task<double> MeasureText(string text, string fontFamily, double size)
        {
            if (_runtime == null)
                return default;

            var width = await _runtime.InvokeAsync<double>(
                "XFUtilities.measureText",
                text,
                $"{size}px {fontFamily}");

            return width;
       }

        public async Task<Size> RegisterApplicationPage(FormsApplicationPage page)
        {
            if (_runtime == null)
                return default;
            var sz = await _runtime.InvokeAsync<JSSize>("XFUtilities.registerApplicationPage", DotNetObjectReference.Create(page));
            return new Size(sz.Width, sz.Height);
        }
    }
}
