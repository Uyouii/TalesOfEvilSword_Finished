using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will toggle the Apply Root Motion flag on a Mecanim Animator. 
	/// </summary>
	[USequencerFriendlyName("Toggle Apply Root Motion")]
	[USequencerEvent("Animation (Mecanim)/Animator/Toggle Apply Root Motion")]
	[USequencerEventHideDuration()]
	public class USToggleAnimatorApplyRootMotion : USEventBase 
	{
		/// <summary>
		/// Should we apply Root Motion?
		/// </summary>
		public bool applyRootMotion = true;

		private bool prevApplyRootMotion;
			
		public override void FireEvent()
		{
			Animator animator = AffectedObject.GetComponent<Animator>();
			if(!animator)
			{
				Debug.LogWarning("Affected Object has no Animator component, for uSequencer Event", this);
				return;
			}
			
			prevApplyRootMotion = animator.applyRootMotion;
			animator.applyRootMotion = applyRootMotion;
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
			
			animator.applyRootMotion = prevApplyRootMotion;
		}
	}
}