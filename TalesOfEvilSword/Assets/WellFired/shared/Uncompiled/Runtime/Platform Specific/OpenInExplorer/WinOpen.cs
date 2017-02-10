using System;

namespace WellFired.Shared
{
	public class WinOpen : IOpen 
	{
		public void OpenFolderToDisplayFile(string filePath)
		{
#if UNITY_STANDALONE
			filePath = "\"" + "filePath" + "\"";
			var processRunner = new RuntimeProcessRunner("explorer.exe", string.Format("/select,{0}", filePath));
			processRunner.Execute();
#endif
		}
	}
}