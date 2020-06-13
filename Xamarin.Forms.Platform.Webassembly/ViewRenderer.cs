using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Xamarin.Forms.Platform.Webassembly
{
	public class ViewRenderer<TElement> : 
		FormsComponent,
		IVisualElementRenderer, 
		IEffectControlProvider
			where TElement : VisualElement 
	{
		readonly List<EventHandler<VisualElementChangedEventArgs>> _elementChangedHandlers =
			new List<EventHandler<VisualElementChangedEventArgs>>();

		private static HashSet<string> _renderProperties = new HashSet<string>
		{
			nameof(VisualElement.X),
			nameof(VisualElement.Y),
			nameof(VisualElement.Width),
			nameof(VisualElement.Height)
		};

		VisualElement IVisualElementRenderer.Element => this.Element;

		public TElement Element { get; private set; }

		public event EventHandler<ElementChangedEventArgs<TElement>> ElementChanged;

		event EventHandler<VisualElementChangedEventArgs> IVisualElementRenderer.ElementChanged
		{
			add { _elementChangedHandlers.Add(value); }
			remove { _elementChangedHandlers.Remove(value); }
		}

		public void Dispose()
		{
		}

		protected virtual bool AffectsRender(string propertyName)
		{
			return _renderProperties.Contains(propertyName);
		}

		public SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (Element == null)
				return new SizeRequest();

			var constraint = new Size(widthConstraint, heightConstraint);

			this.Measure(constraint);

			return new SizeRequest(
				new Size(
					Math.Ceiling(this.DesiredSize.Width), 
					Math.Ceiling(this.DesiredSize.Height)));
		}

		public void RegisterEffect(Effect effect)
		{
			throw new NotImplementedException();
		}

		protected override VisualElement GetElement()
		{
			return this.Element;
		}

		protected override void SetElement(VisualElement element)
		{
			TElement oldElement = this.Element;
			Element = (TElement)element;

			Platform.SetRenderer(element, this);

			if (oldElement != null)
			{
				oldElement.PropertyChanged -= OnElementPropertyChanged;
				//oldElement.FocusChangeRequested -= OnModelFocusChangeRequested;
			}

			Element.PropertyChanged += OnElementPropertyChanged;
			//Element.FocusChangeRequested += OnModelFocusChangeRequested;

			OnElementChanged(new ElementChangedEventArgs<TElement>(oldElement, Element));
			
			var controller = (IElementController)oldElement;
			if (controller != null && controller.EffectControlProvider == this)
				controller.EffectControlProvider = null;

			controller = element;
			if (controller != null)
				controller.EffectControlProvider = this;

			this.Element.NativeSizeChanged();
		}

		protected virtual void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if ( this.AffectsRender(e.PropertyName))
				this.InvalidateRender();
		}

		protected virtual void OnElementChanged(ElementChangedEventArgs<TElement> e)
		{
			var args = new VisualElementChangedEventArgs(e.OldElement, e.NewElement);
			for (var i = 0; i < _elementChangedHandlers.Count; i++)
				_elementChangedHandlers[i](this, args);

			ElementChanged?.Invoke(this, e);
		}

		protected override void SetBasicStyles()
		{
			base.SetBasicStyles();

			this.Styles["top"] = $"{this.Element.Bounds.Top}px; ";
			this.Styles["left"] = $"{this.Element.Bounds.Left}px; ";
			this.Styles["width"] = $"{this.Element.Bounds.Width}px; ";
			this.Styles["height"] = $"{this.Element.Bounds.Height}px; ";
			this.Styles["position"] = "absolute";
		}
	}
}
