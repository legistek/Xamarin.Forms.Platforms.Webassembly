using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Text.Json.Serialization;
using System.Runtime.InteropServices;

namespace Xamarin.Forms.Platform.Webassembly.Interop
{
    [StructLayout(LayoutKind.Explicit)]
    internal class JSTextMeasureRequest
    {
        [FieldOffset(0)]
        public float retWidth;

        [FieldOffset(4)]
        public float retHeight;

        [FieldOffset(8)]
        public string text;

        [FieldOffset(12)]
        public string font;

        [FieldOffset(16)]
        public float widthConstraint;
    }
}
