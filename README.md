# Xamarin.Forms.Platforms.Webassembly
A VERY BAREBONES proof of concept for a Webassembly backend rendering platform for Xamarin.Forms, currently using Blazor webassembly for the backend.

## Prerequisites
Visual Studio 2019, latest update.

.NET Core 3.1 SDK

## Getting Started
Compile and run the `Xamarin.Forms.Platforms.Webassembly.TestApp` project. You can also run the `Xamarin.Forms.Webassembly.TestApp.Reference` (Universal Windows) project for a side-by-side view of the application using the UWP backend as a reference.

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

## About Blazor
The project has been renamed from Xamarin.Forms.Platforms.Blazor to Xamarin.Forms.Platform.Webassembly so as not to tie it to Blazor long term. Blazor ultimately does not offer sufficient performance to support a XF backend. That's not true of Webassembly itself though. 
Currently we're using Blazor for the low level WASM bootstrapping and the component rendering, but we would like to use less and less of it as time goes on. Even if Blazor did have better performance, it's overkill because a great deal of Blazor is dedicated to building and comparing render trees for updating, whereas XF does its own visual tree building and change detection. A better backend would queue up changes received from XF Core and periodically update the DOM through WASM-Javascript interop, similar to WPF's render queue which interops with C++, treating the browser as little more than a composition engine.

## Contributing
Again, this is a very barebones proof of concept. I don't really have the time to review pull requests, but I encourage anyone with the time and interest to take this project on as an active maintained community effort. Contact me if you'd like to be a maintainer, or feel free to fork. Enjoy!
