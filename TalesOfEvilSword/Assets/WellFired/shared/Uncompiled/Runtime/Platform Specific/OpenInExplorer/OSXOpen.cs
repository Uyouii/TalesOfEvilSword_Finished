using System;

namespace WellFired.Shared
{
	public class OSXOpen : IOpen 
	{
		public void OpenFolderToDisplayFile(string filePath)
		{
#if UNITY_STANDALONE
			filePath = "\"" + filePath + "\"";
			var processRunner = new RuntimeProcessRunner("open", string.Format("-n -R {0}", filePath));
			processRunner.Execute();
#endif
		}
	}
}