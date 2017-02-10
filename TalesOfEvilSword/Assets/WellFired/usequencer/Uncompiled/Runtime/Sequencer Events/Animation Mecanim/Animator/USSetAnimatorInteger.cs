using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set an integer on a Mecanim animator. 
	/// </summary>
	[USequencerFriendlyName("Set Mecanim Integer")]
	[USequencerEvent("Animation (Mecanim)/Animator/Set Value/Integer")]
	[USequencerEventHideDuration()]
	public class USSetAnimatorInteger : USEventBase 
	{
		public string valueName = "";
		public int Value = 0;
		
		private int prevValue;
		private int hash;
			
		public override void FireEvent()
		{
			Animator animator = AffectedObject.GetComponent<Animator>();
			if(!animator)
			{
				Debug.LogWarning("Affected Object has no Animator component, for uSequencer Event", this);
				return;
			}
			
			if(valueName.Length == 0)
			{
				Debug.LogWarning("Invalid name passed to the uSequencer Event Set Float", this);
				return;
			}
			
			hash = Animator.StringToHash(valueName);
			
			prevValue = animator.GetInteger(hash);
			animator.SetInteger(hash, Value);
		}
		
		public override void ProcessEvent(float runningTime)
		{
			Animator animator = AffectedObject.GetComponent<Animator>();
			if(!animator)
			{
				Debug.LogWarning("Affected Object has no Animator component, for uSequencer Event", this);
				return;
			}
			
			if(valueName.Length == 0)
			{
				Debug.LogWarning("Invalid name passed to the uSequencer Event Set Float", this);
				return;
			}
			
			hash = Animator.StringToHash(valueName);
			
			prevValue = animator.GetInteger(hash);
			animator.SetInteger(hash, Value);
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
			
			if(valueName.Length == 0)
				return;
			
			animator.SetInteger(hash, prevValue);
		}
	}
}