using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set a boolean value on a Mecanim Animator.
	/// </summary>
	[USequencerFriendlyName("Set Mecanim Bool")]
	[USequencerEvent("Animation (Mecanim)/Animator/Set Value/Bool")]
	[USequencerEventHideDuration()]
	class USSetAnimatorBool : USEventBase 
	{
		/// <summary>
		/// The boolean that we will set on the Animator.
		/// </summary>
		public string valueName = "";

		/// <summary>
		/// The Booleadn that we will set.
		/// </summary>
		public bool Value = true;
		
		private bool prevValue;
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
			
			prevValue = animator.GetBool(hash);
			animator.SetBool(hash, Value);
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
			
			prevValue = animator.GetBool(hash);
			animator.SetBool(hash, Value);
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
			
			animator.SetBool(hash, prevValue);
		}
	}
}