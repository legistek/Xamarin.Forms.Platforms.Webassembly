window.XFUtilities = {
    registerApplicationPage: (ref) => {
        window.addEventListener("resize", (e) => {
            ref.invokeMethodAsync("OnWindowResize", window.innerWidth, window.innerHeight);
        });
        return {
            width: window.innerWidth,
            height: window.innerHeight
        };
    },

    // "private"
    getDummyCanvas: () => {
        var dummyCanvas = window.xfDummyCanvas;
        if (!dummyCanvas) {
            dummyCanvas = document.createElement("canvas");
            window.xfDummyCanvas = dummyCanvas;
        }
        return dummyCanvas;
    },

    measureText: (text, font) => {
        var canvas = XFUtilities.getDummyCanvas();
        var ctx = canvas.getContext("2d");
        ctx.font = font;
        var sz = ctx.measureText(text);
        return sz.width;
    }
};