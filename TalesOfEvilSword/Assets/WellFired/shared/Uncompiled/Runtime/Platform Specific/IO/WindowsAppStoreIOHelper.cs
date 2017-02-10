using System;
using System.IO;

#if NETFX_CORE
namespace WellFired.Shared
{
	public class IOHelper : IIOHelper
	{
		public bool FileExists(string file)
		{
			throw new NotImplementedException();
		}
	}
}
#endif