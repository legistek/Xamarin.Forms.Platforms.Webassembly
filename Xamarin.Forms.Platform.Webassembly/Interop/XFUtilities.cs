using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mono.WebAssembly.Interop;
using Microsoft.JSInterop;

namespace Xamarin.Forms.Platform.Webassembly.Interop
{
    public static class XFUtilities
    {
        private static MonoWebAssemblyJSRuntime _rt = new MonoWebAssemblyJSRuntime();

        public static Size MeasureText(
            string text, 
            string fontFamily, 
            double size, 
            double widthConstraint = 0)
        {
            var req = new JSTextMeasureRequest
            {
                font = $"{size}px {fontFamily}",
                text = text,
                widthConstraint = (float)widthConstraint
            };

            _rt.InvokeUnmarshalled<JSTextMeasureRequest, object>(
                "XFUtilities.measureText",
                req);

            return new Size(req.retWidth, req.retHeight);
        }

        public static void ForceResizeMainPage()
        {
            _rt.InvokeVoid("XFUtilities.resizeMainWindow");
        }

        [JSInvokable]
        public static void ResizeMainPage(double width, double height)
        {
            Application.Current.MainPage.Layout(new Rectangle(0, 0, width, height));
        }
    }
}
