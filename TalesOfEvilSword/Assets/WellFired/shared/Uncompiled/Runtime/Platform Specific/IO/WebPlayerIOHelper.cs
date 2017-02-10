using System;
using System.IO;

#if UNITY_WEBPLAYER
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