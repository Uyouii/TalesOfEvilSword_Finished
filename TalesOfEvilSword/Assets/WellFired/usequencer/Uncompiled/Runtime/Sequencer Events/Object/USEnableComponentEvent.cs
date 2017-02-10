using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set a GameObject's specific component enable state at a given time. 
	/// </summary>
	[USequencerFriendlyName("Toggle Component")]
	[USequencerEvent("Object/Toggle Component")]
	[USequencerEventHideDuration()]
	public class USEnableComponentEvent : USEventBase
	{
		/// <summary>
		/// Should we enable the object at the given time.
		/// </summary>
	    public bool enableComponent = false;
		private bool prevEnable = false;

		[HideInInspector]
		[SerializeField]
		private string componentName;

		public string ComponentName
		{
			get { return componentName; }
			set { componentName = value; }
		}

		public override void FireEvent()
		{
			var component = AffectedObject.GetComponent(ComponentName) as Behaviour;
			
			if(!component)
				return;
		
			prevEnable = component.enabled;
			component.enabled = enableComponent;
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

			var component = AffectedObject.GetComponent(ComponentName) as Behaviour;

			if(!component)
				return;

			component.enabled = prevEnable;
		}
	}
}