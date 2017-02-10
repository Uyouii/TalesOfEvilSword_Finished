using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set the playback speed on a Mecanim Animator. 
	/// </summary>
	[USequencerFriendlyName("Set Playback Speed")]
	[USequencerEvent("Animation (Mecanim)/Animator/Set Playback Speed")]
	[USequencerEventHideDuration()]
	public class USSetAnimatorPlaybackSpeed : USEventBase 
	{
		public float playbackSpeed = 1.0f;
		private float prevPlaybackSpeed;
			
		public override void FireEvent()
		{
			Animator animator = AffectedObject.GetComponent<Animator>();
			if(!animator)
			{
				Debug.LogWarning("Affected Object has no Animator component, for uSequencer Event", this);
				return;
			}
			
			prevPlaybackSpeed = animator.speed;
			animator.speed = playbackSpeed;
		}
		
		public override void ProcessEvent(float runningTime)
		{
			
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			Animator animator = AffectedObject.GetComponent<Animator>();
			if(!animator)
				return;
			
			animator.speed = prevPlaybackSpeed;
		}
	}
}