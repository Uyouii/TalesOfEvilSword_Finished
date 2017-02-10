using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USMessageEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USMessageEvent))]
	public class USMessageEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USMessageEvent messageEvent = TargetEvent as USMessageEvent;
	
			myArea = DrawDurationDefinedBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				if (messageEvent)
					GUILayout.Label(messageEvent.message, DefaultLabel);
			}
	
			return myArea;
		}
	}
}