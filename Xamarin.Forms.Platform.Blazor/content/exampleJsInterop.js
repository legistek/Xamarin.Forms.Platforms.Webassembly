// This file is to show how a library package may provide JavaScript interop features
// wrapped in a .NET API

window.exampleJsFunctions = {
    showPrompt: function (message) {
        return prompt(message, 'Type anything here');
    },

    measureText: function (text, font) {
        var c = document.getElementById("formsCanvas");
        var ctx = c.getContext("2d");
        ctx.font = font;
        return ctx.measureText(text).width;
    }
};
