using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that let's us skip a USSequencer to a specified point, or straight to the end.
	/// </summary>
	[USequencerFriendlyName("Skip uSequence")]
	[USequencerEvent("Sequence/Skip uSequence")]
	[USequencerEventHideDuration()]
	public class USSkipSequenceEvent : USEventBase 
	{
		/// <summary>
		/// The sequence to skip.
		/// </summary>
		public USSequencer sequence = null;

		/// <summary>
		/// Skip straight to the end.
		/// </summary>
		public bool skipToEnd = true;

		/// <summary>
		/// Skip to a specified time.
		/// </summary>
		public float skipToTime = -1.0f;
		
		public override void FireEvent()
		{	
			if(!sequence)
			{
				Debug.LogWarning("No sequence for USSkipSequenceEvent : " + name, this);
				return;
			}
			
			if(!skipToEnd && skipToTime < 0.0f && skipToTime > sequence.Duration)
			{
				Debug.LogWarning("You haven't set the properties correctly on the Sequence for this USSkipSequenceEvent, either the skipToTime is invalid, or you haven't flagged it to skip to the end", this);
				return;
			}
			
			if(skipToEnd)
				sequence.SkipTimelineTo(sequence.Duration);
			else
				sequence.SkipTimelineTo(skipToTime);
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}