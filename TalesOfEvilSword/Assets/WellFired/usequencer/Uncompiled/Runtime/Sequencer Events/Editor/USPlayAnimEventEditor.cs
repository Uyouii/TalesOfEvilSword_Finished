using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USPlayAnim event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USPlayAnimEvent))]
	public class USPlayAnimEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USPlayAnimEvent animEvent = TargetEvent as USPlayAnimEvent;
	
			float endPosition = ConvertTimeToXPos(animEvent.FireTime + (animEvent.Duration<=0.0f?3.0f:animEvent.Duration));
			myArea.width = endPosition - myArea.x;
	
			DrawDefaultBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				if(animEvent)
					GUILayout.Label("Animation : " + animEvent.animationClip, DefaultLabel);
			}
	
			return myArea;
		}
	}
}