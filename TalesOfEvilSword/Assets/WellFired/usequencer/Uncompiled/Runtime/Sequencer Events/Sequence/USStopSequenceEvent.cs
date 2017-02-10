using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will stop a USSequencer from playing back. 
	/// </summary>
	[USequencerFriendlyName("stop uSequence")]
	[USequencerEvent("Sequence/Stop uSequence")]
	[USequencerEventHideDuration()]
	public class USStopSequenceEvent : USEventBase 
	{
		public USSequencer sequence = null;
		
		public override void FireEvent()
		{	
			if(!sequence)
				Debug.LogWarning("No sequence for USstopSequenceEvent : " + name, this);
			
			if (sequence)
				sequence.Stop();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}