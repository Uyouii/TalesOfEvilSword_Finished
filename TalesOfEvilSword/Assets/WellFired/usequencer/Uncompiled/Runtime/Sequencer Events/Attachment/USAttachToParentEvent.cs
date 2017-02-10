using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will parent one object to another. 
	/// </summary>
	[USequencerFriendlyName("Attach Object To Parent")]
	[USequencerEvent("Attach/Attach To Parent")]
	[USequencerEventHideDuration()]
	public class USAttachToParentEvent : USEventBase 
	{
		/// <summary>
		/// The new parent object.
		/// </summary>
		public Transform parentObject = null;
		
		private Transform originalParent = null;
		
		public override void FireEvent()
		{
			if(!parentObject)
			{
				Debug.Log("USAttachEvent has been asked to attach an object, but it hasn't been given a parent from USAttachEvent::FireEvent");
				return;
			}
			
			originalParent = AffectedObject.transform.parent;
			AffectedObject.transform.parent = parentObject;
		}
	
		public override void ProcessEvent(float deltaTime)
		{
			
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(!AffectedObject)
				return;
			
			AffectedObject.transform.parent = originalParent;
		}
	}
}