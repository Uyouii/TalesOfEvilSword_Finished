using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set a float on a Mecanim Animator. 
	/// </summary>
	[USequencerFriendlyName("Set Mecanim Float")]
	[USequencerEvent("Animation (Mecanim)/Animator/Set Value/Float")]
	[USequencerEventHideDuration()]
	public class USSetAnimatorFloat : USEventBase 
	{
		public string valueName = "";
		public float Value = 0.0f;
		
		private float prevValue;
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
			
			prevValue = animator.GetFloat(hash);
			animator.SetFloat(hash, Value);
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
			
			prevValue = animator.GetFloat(hash);
			animator.SetFloat(hash, Value);
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
			
			animator.SetFloat(hash, prevValue);
		}
	}
}