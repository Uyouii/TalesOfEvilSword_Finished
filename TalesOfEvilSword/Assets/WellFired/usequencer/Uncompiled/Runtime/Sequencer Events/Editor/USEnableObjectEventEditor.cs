using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USEnableObjectEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USEnableObjectEvent))]
	public class USEnableObjectEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USEnableObjectEvent toggleEvent = TargetEvent as USEnableObjectEvent;
	
			DrawDefaultBox(myArea);

			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(toggleEvent.enable?"Enable : ":"Disable : ", DefaultLabel);
				GUILayout.Label(toggleEvent.AffectedObject.name, DefaultLabel);
			}
	
			return myArea;
		}
	}
}