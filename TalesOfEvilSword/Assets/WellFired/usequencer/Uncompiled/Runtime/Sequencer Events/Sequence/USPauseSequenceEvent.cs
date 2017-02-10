using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that pauses a sequence at the given time. 
	/// </summary>
	[USequencerFriendlyName("Pause uSequence")]
	[USequencerEvent("Sequence/Pause uSequence")]
	[USequencerEventHideDuration()]
	public class USPauseSequenceEvent : USEventBase 
	{
		/// <summary>
		/// The sequence to pause.
		/// </summary>
		public USSequencer sequence = null;
		
		public override void FireEvent()
		{	
			if(!sequence)
				Debug.LogWarning("No sequence for USPauseSequenceEvent : " + name, this);
			
			if (sequence)
				sequence.Pause();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}