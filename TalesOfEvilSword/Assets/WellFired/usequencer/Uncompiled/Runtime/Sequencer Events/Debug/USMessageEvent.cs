using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that prints a message to the log. 
	/// </summary>
	[USequencerFriendlyName("Debug Message")]
	[USequencerEvent("Debug/Log Message")]
	[USequencerEventHideDuration()]
	public class USMessageEvent : USEventBase 
	{
		public string message = "Default Message";
		
		public override void FireEvent()
		{
			Debug.Log(message);
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}