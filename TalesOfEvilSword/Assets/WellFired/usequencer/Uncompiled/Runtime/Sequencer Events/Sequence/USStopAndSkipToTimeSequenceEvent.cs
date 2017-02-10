using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will stop a USSequencer from playing back. 
	/// </summary>
	[USequencerFriendlyName("Stop and Skip sequencer")]
	[USequencerEvent("Sequence/Stop And Skip")]
	[USequencerEventHideDuration()]
	public class USStopAndSkipToTimeSequenceEvent : USEventBase 
	{
		[SerializeField]
		private USSequencer sequence = null;

		[SerializeField]
		private float timeToSkipTo;
		
		public override void FireEvent()
		{	
			if(!sequence)
				Debug.LogWarning("No sequence for USstopSequenceEvent : " + name, this);
			
			if (sequence)
			{
				sequence.Stop();
				sequence.SkipTimelineTo(timeToSkipTo);
				sequence.UpdateSequencer(0.0f);
			}
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}