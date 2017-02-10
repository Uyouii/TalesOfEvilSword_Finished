using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will pause or resume audio at a given time.
	/// </summary>
	[USequencerFriendlyName("Pause Or Resume Audio")]
	[USequencerEvent("Audio/Pause Or Resume Audio")]
	[USequencerEventHideDuration()]
	public class USPauseResumeAudioEvent : USEventBase 
	{
		public bool pause = true;
		
		public override void FireEvent()
		{
			if(!AffectedObject)
			{
				Debug.Log("USSequencer is trying to play an audio clip, but you didn't give it Audio To Play from USPauseAudioEvent::FireEvent");
				return;
			}
			
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
			if(!audio)
			{
				Debug.Log("USSequencer is trying to play an audio source, but the GameObject doesn't contain an AudioClip from USPauseAudioEvent::FireEvent");
				return;
			}
			
			if(pause)
				audio.Pause();
			if(!pause)
				audio.Play();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
			if(!audio)
			{
				Debug.Log("USSequencer is trying to play an audio source, but the GameObject doesn't contain an AudioClip from USPauseAudioEvent::FireEvent");
				return;
			}
			
			if(audio.isPlaying)
				return;
		}
	}
}