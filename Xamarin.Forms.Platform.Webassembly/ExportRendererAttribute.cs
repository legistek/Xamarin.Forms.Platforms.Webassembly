using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.Webassembly
{
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class ExportRendererAttribute : HandlerAttribute
	{
		public ExportRendererAttribute(Type handler, Type target) : base(handler, target)
		{
		}
	}
}
