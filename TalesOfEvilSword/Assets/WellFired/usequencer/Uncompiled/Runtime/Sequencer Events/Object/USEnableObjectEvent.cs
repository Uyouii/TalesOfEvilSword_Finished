using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set a GameObject's enable state at a given time. 
	/// </summary>
	[USequencerFriendlyName("Toggle Object")]
	[USequencerEvent("Object/Toggle Object")]
	[USequencerEventHideDuration()]
	public class USEnableObjectEvent : USEventBase
	{
		/// <summary>
		/// Should we enable the object at the given time.
		/// </summary>
	    public bool enable = false;
		private bool prevEnable = false;
		
		public override void FireEvent()
		{
			prevEnable = AffectedObject.activeSelf;
			AffectedObject.SetActive(enable);
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

			AffectedObject.SetActive(prevEnable);
		}
	}
}