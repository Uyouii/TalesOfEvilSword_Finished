using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USWarpToObject event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USWarpToObject))] 
	public class USWarpToObjectEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USWarpToObject warpEvent = TargetEvent as USWarpToObject;
			
			myArea.width += 10.0f;
			DrawDefaultBox(myArea);
			
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				if (warpEvent)
				{
					GUILayout.Label(warpEvent.objectToWarpTo?warpEvent.objectToWarpTo.name:"null", DefaultLabel);
					GUILayout.Label(warpEvent.useObjectRotation?"Using Warp Rotation":"Keep Original Rotation", DefaultLabel);
				}
			}
	
			return myArea;
		}
	}
}