using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.Webassembly
{
	public class WebassemblyPlatformServices : IPlatformServices
	{
		public bool IsInvokeRequired => false;

		public string RuntimePlatform => "Webassembly";

        public OSAppTheme RequestedTheme => throw new NotImplementedException();

        public void BeginInvokeOnMainThread(Action action)
		{           
            action?.Invoke();
		}

		public Ticker CreateTicker()
		{
			throw new NotImplementedException();
		}

		public Assembly[] GetAssemblies()
		{
			//return new Assembly[0];
			return AppDomain.CurrentDomain.GetAssemblies();
		}

		public string GetMD5Hash(string input)
		{
			return null;
			// MSDN - Documentation -https://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx
			using (MD5 md5Hash = MD5.Create())
			{
				// Convert the input string to a byte array and compute the hash.
				byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				StringBuilder sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data 
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString();
			}
		}

        public Color GetNamedColor(string name)
        {
            throw new NotImplementedException();
        }

        public double GetNamedSize(NamedSize size, Type targetElementType, bool useOldSizes)
		{
			switch (size)
			{
				case NamedSize.Default:
					return 12;
				case NamedSize.Large:
					return 18;
				case NamedSize.Medium:
					return 12;
				case NamedSize.Small:
					return 10;
				case NamedSize.Micro:
					return 8;
				default:
					throw new ArgumentOutOfRangeException("size");
			}
		}

		public SizeRequest GetNativeSize(VisualElement view, double widthConstraint, double heightConstraint)
		{
			if (widthConstraint > 0 && heightConstraint > 0 && Platform.GetRenderer(view) != null)
			{
				IVisualElementRenderer element = Platform.GetRenderer(view);
				return element.GetDesiredSize(widthConstraint, heightConstraint);
			}

			return new SizeRequest();
		}

		public Task<Stream> GetStreamAsync(Uri uri, CancellationToken cancellationToken)
		{
			throw new NotImplementedException();
		}

		public IIsolatedStorageFile GetUserStoreForApplication()
		{
			throw new NotImplementedException();
		}

		public void OpenUriAction(Uri uri)
		{
			throw new NotImplementedException();
		}

		public void QuitApplication()
		{
			throw new NotImplementedException();
		}

		public void StartTimer(TimeSpan interval, Func<bool> callback)
		{
			throw new NotImplementedException();
		}
	}
}
