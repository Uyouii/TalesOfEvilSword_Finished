using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USTimeScaleEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USTimeScaleEvent))]
	public class USTimeScaleEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USTimeScaleEvent timeScaleEvent = TargetEvent as USTimeScaleEvent;
			
			timeScaleEvent.Duration = timeScaleEvent.scaleCurve[timeScaleEvent.scaleCurve.length-1].time;
			
			// Draw our Whole Box.
			if (timeScaleEvent.Duration > 0)
			{
				float endPosition = ConvertTimeToXPos(timeScaleEvent.FireTime + timeScaleEvent.Duration);
				myArea.width = endPosition - myArea.x;
			}
			DrawDefaultBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
			}
	
			return myArea;
		}
	}
}