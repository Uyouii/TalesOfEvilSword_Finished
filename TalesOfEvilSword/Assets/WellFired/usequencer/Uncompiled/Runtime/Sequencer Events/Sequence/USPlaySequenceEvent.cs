using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will let you play a USSequencer object at a given time. 
	/// </summary>
	[USequencerFriendlyName("Play uSequence")]
	[USequencerEvent("Sequence/Play uSequence")]
	[USequencerEventHideDuration()]
	public class USPlaySequenceEvent : USEventBase 
	{
		public USSequencer sequence = null;
		public bool restartSequencer = false;
		
		public override void FireEvent()
		{	
			if(!sequence)
			{
				Debug.LogWarning("No sequence for USPlaySequenceEvent : " + name, this);
				return;
			}
			
			if(!Application.isPlaying)
			{
				Debug.LogWarning("Sequence playback controls are not supported in the editor, but will work in game, just fine.");
				return;
			}
			
			if(!restartSequencer)
			{
				sequence.Play();
			}
			else
			{
				sequence.RunningTime = 0.0f;
				sequence.Play();
			}
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}