using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set the playback rate on a USSequencer object. 
	/// </summary>
	[USequencerFriendlyName("Set uSequence Playback Rate")]
	[USequencerEvent("Sequence/Set Playback Rate")]
	[USequencerEventHideDuration()]
	public class USSetPlaybackRateEvent : USEventBase 
	{
		/// <summary>
		/// The sequence we will alter.
		/// </summary>
		public USSequencer sequence = null;

		/// <summary>
		/// The new playback rate.
		/// </summary>
		public float playbackRate = 1.0f;

		private float prevPlaybackRate = 1.0f;
		
		public override void FireEvent()
		{	
			if(!sequence)
				Debug.LogWarning("No sequence for USSetPlaybackRate : " + name, this);
			
			if (sequence)
			{
				prevPlaybackRate = sequence.PlaybackRate;
				sequence.PlaybackRate = playbackRate;
			}
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(sequence)
				sequence.PlaybackRate = prevPlaybackRate;
		}
	}
}