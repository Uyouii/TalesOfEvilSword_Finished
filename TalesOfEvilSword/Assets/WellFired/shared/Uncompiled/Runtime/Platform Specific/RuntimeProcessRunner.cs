#if UNITY_STANDALONE

using System;
using System.Diagnostics;

public class RuntimeProcessRunner
{
	#region Constants
	private static readonly int DEFAULT_TIMEOUT = 40000;
	private static readonly string DEFAULT_WORKING_DIRECTORY = "";
	#endregion

	#region Fields
	private string mCommand;
	private string mExecutable;
	private string mWorkingDirectory;
	private int mTimeoutMs;
	private Process mProcess;
	#endregion

	public Action OnProcessSuccesful
	{
		get;
		set;
	}
	
	public Action OnProcessFailed
	{
		get;
		set;
	}

	public bool IsComplete
	{
		get;
		private set;
	}

	public RuntimeProcessRunner(string executable, string args, string workingDirectory, int timeoutMs)
	{
		OnProcessSuccesful = delegate {};
		OnProcessFailed = delegate {};

		mExecutable = executable;
		mCommand = args;
		mWorkingDirectory = workingDirectory;
		mTimeoutMs = timeoutMs;
	}
	
	public RuntimeProcessRunner(string executable, string args)
	{
		OnProcessSuccesful = delegate {};
		OnProcessFailed = delegate {};

		mExecutable = executable;
		mCommand = args;
		mWorkingDirectory = DEFAULT_WORKING_DIRECTORY;
		mTimeoutMs = DEFAULT_TIMEOUT;
	}
	
	public void Execute()
	{
		try
		{
			mProcess = new Process
			{
				StartInfo =
				{
					UseShellExecute = false,
					FileName = mExecutable,
					Arguments = mCommand,
					WorkingDirectory = mWorkingDirectory,
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					CreateNoWindow = true
				}
			};
			
			mProcess.OutputDataReceived += (p, o) =>
			{
				if(!String.IsNullOrEmpty(o.Data))
					OutputLineReceived(o.Data.TrimEnd());
			};
			
			mProcess.Start();
			
			mProcess.BeginOutputReadLine();
			
			string error = mProcess.StandardError.ReadToEnd();
			
			if(!mProcess.WaitForExit(mTimeoutMs)) // wait up to 40 second
			{
				ProcessFailed(true, "", -1);
				return;
			}
			if(mProcess.ExitCode != 0 || error.Length != 0)
			{
				ProcessFailed(false, error, mProcess.ExitCode);
				return;
			}
			
			ProcessSuccesful();
		}
		catch(Exception e)
		{
			UnityEngine.Debug.LogError("Exception on process queue Thread: " + e.Message + "\n" + e.StackTrace);
		}
		finally
		{
			IsComplete = true;
		}
	}
	
	public void Abort()
	{
		mProcess.Kill();
	}
	
	protected virtual void OutputLineReceived(string line)
	{
		UnityEngine.Debug.Log(line);
	}
	
	protected virtual void ProcessSuccesful()
	{
		OnProcessSuccesful();
		UnityEngine.Debug.Log("Process Complete");
	}
	
	protected virtual void ProcessFailed(bool timedOut, string errorMessage, int errorCode)
	{
		OnProcessFailed();
		UnityEngine.Debug.Log(String.Format("Process Failed : {0} with code : {1}", errorMessage, errorCode));
	}
}
#endif