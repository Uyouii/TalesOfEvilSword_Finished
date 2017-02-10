using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will stop uSequencer from recording. 
	/// </summary>
	[USequencerFriendlyName("Stop Recording")]
	[USequencerEvent("Recording/Stop Recording")]
	[USequencerEventHideDuration()]
	public class USStopRecordingEvent : USEventBase
	{
		public override void FireEvent()
		{
			if(!Application.isPlaying)
			{
				Debug.Log("Recording events only work when in play mode");
				return;
			}

			USRuntimeUtility.StopRecordingSequence();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}