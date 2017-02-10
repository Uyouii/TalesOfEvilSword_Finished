using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will start uSequencer recording. 
	/// </summary>
	[USequencerFriendlyName("Start Recording")]
	[USequencerEvent("Recording/Start Recording")]
	[USequencerEventHideDuration()]
	public class USStartRecordingEvent : USEventBase
	{
		public override void FireEvent()
		{
			if(!Application.isPlaying)
			{
				Debug.Log("Recording events only work when in play mode");
				return;
			}

			USRuntimeUtility.StartRecordingSequence(Sequence, USRecordRuntimePreferences.CapturePath, USRecord.GetFramerate(), USRecord.GetUpscaleAmount());
		}
		
		public override void ProcessEvent(float deltaTime)
		{

		}
	}
}