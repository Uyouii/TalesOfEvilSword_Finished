using System;
using System.IO;

#if !NETFX_CORE && !UNITY_WEBPLAYER
namespace WellFired.Shared
{
	public class IOHelper : IIOHelper
	{
		public bool FileExists(string file)
		{
			return File.Exists(file);
		}
	}
}
#endif