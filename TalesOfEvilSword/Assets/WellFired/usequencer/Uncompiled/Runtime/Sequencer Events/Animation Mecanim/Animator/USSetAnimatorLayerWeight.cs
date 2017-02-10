using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set the layerweight on a Mecanim Animator at a given time. 
	/// </summary>
	[USequencerFriendlyName("Set Layer Weight")]
	[USequencerEvent("Animation (Mecanim)/Animator/Set Layer Weight")]
	[USequencerEventHideDuration()]
	public class USSetAnimatorLayerWeight : USEventBase 
	{
		public float layerWeight = 1.0f;
		public int layerIndex = -1;
		
		private float prevLayerWeight;
			
		public override void FireEvent()
		{
			Animator animator = AffectedObject.GetComponent<Animator>();
			if(!animator)
			{
				Debug.LogWarning("Affected Object has no Animator component, for uSequencer Event", this);
				return;
			}
			
			if(layerIndex < 0)
			{
				Debug.LogWarning("Set Animator Layer weight, incorrect index : " + layerIndex);
				return;
			}
			
			prevLayerWeight = animator.GetLayerWeight(layerIndex);
			animator.SetLayerWeight(layerIndex, layerWeight);
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
			
			if(layerIndex < 0)
				return;
			
			animator.SetLayerWeight(layerIndex, prevLayerWeight);
		}
	}
}