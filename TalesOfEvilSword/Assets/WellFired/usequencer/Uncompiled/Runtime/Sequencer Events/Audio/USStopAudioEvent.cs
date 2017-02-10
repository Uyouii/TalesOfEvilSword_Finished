using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will stop audio playback at a given time.
	/// </summary>
	[USequencerFriendlyName("Stop Audio")]
	[USequencerEvent("Audio/Stop Audio")]
	[USequencerEventHideDuration()]
	public class USStopAudioEvent : USEventBase 
	{
		public override void FireEvent()
		{
			if(!AffectedObject)
			{
				Debug.Log("USSequencer is trying to play an audio clip, but you didn't give it Audio To Play from USPlayAudioEvent::FireEvent");
				return;
			}
			
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
			if(!audio)
			{
				Debug.Log("USSequencer is trying to play an audio source, but the GameObject doesn't contain an AudioClip from USPlayAudioEvent::FireEvent");
				return;
			}
			
			audio.Stop();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}