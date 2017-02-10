using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USAttachToParentEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USAttachToParentEvent))]
	public class USAttachToParentEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USAttachToParentEvent attachEvent = TargetEvent as USAttachToParentEvent;
	
			DrawDefaultBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				if (attachEvent)
					GUILayout.Label(attachEvent.parentObject?attachEvent.parentObject.name:"null", DefaultLabel);
			}
			
			return myArea;
		}
	}
}