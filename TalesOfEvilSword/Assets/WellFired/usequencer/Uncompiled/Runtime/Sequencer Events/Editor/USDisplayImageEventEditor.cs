using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USDisplayImageEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USDisplayImageEvent))]
	public class USDisplayImageEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USDisplayImageEvent DisplayImageEvent = TargetEvent as USDisplayImageEvent;
			
			float endPosition = ConvertTimeToXPos(DisplayImageEvent.FireTime + (DisplayImageEvent.Duration<=0.0f?3.0f:DisplayImageEvent.Duration));
			myArea.width = endPosition - myArea.x;
			
			DrawDefaultBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
	
			return myArea;
		}
	}
}