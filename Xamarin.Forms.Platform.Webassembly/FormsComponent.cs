using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Xamarin.Forms.Platform.Webassembly.Interop;
using System.Threading;

namespace Xamarin.Forms.Platform.Webassembly
{
	public abstract class FormsComponent : ComponentBase, INotifyPropertyChanged
	{
		bool _isRenderInitialized;
		private Size _DesiredSize;
        SynchronizationContext _syncContext;

        public FormsComponent()
        {
            _syncContext = SynchronizationContext.Current;
        }

        public event PropertyChangedEventHandler PropertyChanged;

		protected int RenderCounter { get; set; }

		[Parameter]
		public VisualElement Element
		{
			get => GetElement();
			set => SetElement(value);
		}

		protected Dictionary<string, string> Styles { get; } = new Dictionary<string, string>();
	
		public Size DesiredSize
		{
			get
			{
				return _DesiredSize;
			}
			protected set
			{
				if (_DesiredSize != value)
				{
					_DesiredSize = value;
					OnPropertyChanged();
				}
			}
		}

		public FormsComponent VisualParent { get; private set; }

		protected abstract VisualElement GetElement();

		protected abstract void SetElement(VisualElement element);

		public void Measure(Size constraint)
		{
			var sz = this.MeasureOverride(constraint);
			this.DesiredSize = sz;
		}

		protected virtual Size MeasureOverride(Size constraint)
		{
			return new Size(1, 1);
		}

		public virtual void InvalidateMeasure()
		{
		}

		public void InvalidateRender()
		{
			if (!_isRenderInitialized)
				return;
            this.SafeInvoke(() =>
            {
                this.StateHasChanged();
            });			
		}

		protected void OnPropertyChanged(
			[CallerMemberName]
			string name = null)
		{
			this.PropertyChanged?.Invoke(
				this,
				new PropertyChangedEventArgs(name));
		}

		protected virtual void BuildSelectorStyles(RenderTreeBuilder builder, string baseStyle)
		{
		}

		protected override sealed void BuildRenderTree(RenderTreeBuilder builder)
		{
			RenderCounter = 0;
			this._isRenderInitialized = true;

			this.SetBasicStyles();

			string style = $"style{this.Element.GetHashCode()}";

			base.BuildRenderTree(builder);

			builder.OpenElement(RenderCounter++, "style");
			builder.AddContent(RenderCounter++, $@".{style} 
				{{
					all: initial;
					{this.ConstructCSS()}
				}}");
			builder.CloseElement();

			BuildSelectorStyles(builder, style);

			builder.OpenElement(RenderCounter++, "div");
			builder.AddAttribute(RenderCounter++, "class", style);

			AddAdditionalAttributes(builder);

			builder.AddContent(RenderCounter++, RenderContent);
			builder.CloseElement();
		}

		protected virtual void AddAdditionalAttributes(RenderTreeBuilder builder)
		{
		}

		protected virtual void RenderContent(RenderTreeBuilder builder)
		{
		}

		private string ConstructCSS()
		{
			return string.Concat(
				this.Styles.Select(k => $"{k.Key}: {k.Value};\r"));
		}

		protected virtual void SetBasicStyles()
		{
		}

		protected void RenderChild(RenderTreeBuilder builder, VisualElement child)
		{
			if (child == null)
				return;
			Type t = Platform.GetOrCreateRendererType(child);
			builder.OpenComponent(0, t);
			builder.AddAttribute(1, nameof(FormsComponent.Element), child);
			builder.CloseComponent();
		}

        internal void SafeInvoke(Action action)
        {
            if (SynchronizationContext.Current != _syncContext)
                InvokeAsync(() => action());
            else
                action();
        }
    }
}