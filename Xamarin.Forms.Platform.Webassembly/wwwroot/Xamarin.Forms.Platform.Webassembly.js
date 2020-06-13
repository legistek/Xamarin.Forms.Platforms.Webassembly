window.XFUtilities = {
    _textMeasurer: null,
   
    resizeMainWindow() {
        DotNet.invokeMethod(
            "Xamarin.Forms.Platform.Webassembly",
            "ResizeMainPage",
            window.innerWidth,
            window.innerHeight);
    },

    measureTextInternal (text, font, widthConstraint) {
        var tm = this._textMeasurer;
        if (!tm) {
            tm = document.createElement("div");
            tm.style.visibility = "hidden";
            tm.style.margin = null;
            tm.style.padding = null;
            tm.style.linespacing = 1;
            document.body.appendChild(tm);
            this._textMeasurer = tm;
        }

        tm.style.font = font;
        tm.innerHTML = text;
        if (widthConstraint !== 0)
            tm.style.maxWidth = widthConstraint + "px";
        else
            tm.style.maxWidth = null;
                
        return {
            Width: tm.clientWidth,
            Height: tm.clientHeight
        }
    },

    // Uses direct memory access, avoiding JSON serialize/deserialize which
    // is a performance killer
    measureText (request) {
        // request is a pointer to a JSTextMeasureRequest struct
        // Pointer offsets must match FieldOffset

        // Necessary to get field base because request is a C# class
        // so fields don't necessarily start at 0
        var offset = Blazor.platform.getObjectFieldsBaseAddress(request);

        var text = Blazor.platform.readStringField(offset, 8);
        var font = Blazor.platform.readStringField(offset, 12);
        var widthConstraint = Blazor.platform.readFloatField(offset, 16);

        var sz = this.measureTextInternal(text, font, widthConstraint);

        Module.setValue(offset + 0, sz.Width, 'float');
        Module.setValue(offset + 4, sz.Height, 'float');
    }
};

window.addEventListener("resize", (e) => {
    window.XFUtilities.resizeMainWindow();
});