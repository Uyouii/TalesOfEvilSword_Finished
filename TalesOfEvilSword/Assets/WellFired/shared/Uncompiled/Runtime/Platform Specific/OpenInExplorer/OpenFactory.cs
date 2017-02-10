using System;
using System.Collections;
using UnityEngine;

namespace WellFired.Shared
{
	public static class OpenFactory 
	{
		public static bool PlatformCanOpen()
		{
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
			return true;
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
			return true;
#elif UNITY_STANDALONE_LINUX || UNITY_EDITOR
			return true;
			#else
			return false;
#endif
		}

		public static IOpen CreateOpen()
		{
#if UNITY_STANDALONE_OSX || UNITY_EDITOR_OSX
			return new OSXOpen();
#elif UNITY_STANDALONE_WIN || UNITY_EDITOR
			return new WinOpen();
#elif UNITY_STANDALONE_LINUX || UNITY_EDITOR
			return new LinuxOpen();
#else
			throw new System.Exception("Platform doesn't support open commands, try to call OpenFactory.PlatformCanOpen");
#endif

		}
	}
}