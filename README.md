# Xamarin.Forms.Platforms.Blazor
A VERY BAREBONES proof of concept for a Blazor backend rendering platform for Xamarin.Forms

## Prerequisites
Visual Studio 2017, latest update.

.NET Core 2.1 SDK

ASP.NET Core Blazor Language Services Visual Studio Extension

## Getting Started
Compile and run the `Xamarin.Forms.Blazor.TestApp.BlazorServer` project. You can also run the `Xamarin.Forms.Blazor.TestApp.Reference` (Universal Windows) project for a side-by-side view of the application using the UWP backend as a reference.

## How it Works
The basic concept is that every XF view results in a `<div>` with its own custom CSS class based on the XF control properties. Since XF handles layout, each `<div>` is styled with `position: absolute` and given `left` and `top` coordinates based on the final layout bounds of the XF control. Individual control renderers then add to these styles based on properties unique to their control by overriding `SetBasicStyles`.

## Adding More Renderers
The basic process for creating a control renderer is as follows:
- Make a derived class from `ViewRenderer<T>` where `T` is the Xamarin.Forms element
- Override `AffectsRender` to tell the platform which XF control properties should trigger a Blazor component render (always or (`||`) with `base.AffectsRender`)
- Override `SetBasicStyles` to translate XF control properties into CSS styles for the containing `<div>`.
- Optionally override `RenderContent` if the XF control has child content. For example, the `ButtonRenderer` adds text content corresponding to `Button.Text`. If the child content is another XF view, use `RenderChild`.
- Optionally override `MeasureOverride` if the control needs to measure text or other complex content. (See below regarding text measuring).
- Optionally override `AddAdditionalAttributes` to add UI event handlers.

## About Text Measuring
By far the trickiest issue here is measuring text so that XF can handle layout, rather than the browser. Currently, a very hackish method is being employed: a dummy 1x1 canvas is set on the main page, and we're using JavaScript interop to call `canvas.measureText` to get the text size. Unfortunately this has to be done asynchronously, so the intiial text is rendered at the wrong size and then re-rendered after measurement. One presumes there is a better way to do this. 

## Contributing
Again, this is a very barebones proof of concept. I don't really have the time to review pull requests, but I encourage anyone with the time and interest to take this project on as an active maintained community effort. Contact me if you'd like to be a maintainer, or feel free to fork. Enjoy!
