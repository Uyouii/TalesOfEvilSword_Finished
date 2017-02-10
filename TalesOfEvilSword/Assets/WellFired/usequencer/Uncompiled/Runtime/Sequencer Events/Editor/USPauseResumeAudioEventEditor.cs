using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USPauseResumeAudioEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USPauseResumeAudioEvent))]
	public class USPauseResumeAudioEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USPauseResumeAudioEvent audioEvent = TargetEvent as USPauseResumeAudioEvent;
	
			DrawDefaultBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				if(audioEvent)
					GUILayout.Label(audioEvent.pause?"Pause Audio":"Resume Audio", DefaultLabel);
			}
	
			return myArea;
		}
	}
}