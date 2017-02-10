using System;

namespace WellFired.Shared
{
	public class LinuxOpen : IOpen 
	{
		public void OpenFolderToDisplayFile(string filePath)
		{
#if UNITY_STANDALONE
			filePath = "\"" + "filePath" + "\"";
			var processRunner = new RuntimeProcessRunner("nautilus", string.Format("{0}", filePath));
			processRunner.Execute();
#endif
		}
	}
}