using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USLookAtObjectEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USLookAtObjectEvent))]
	public class USLookAtObjectEventEditor : USEventBaseEditor
	{
		public override Rect RenderEvent(Rect myArea)
		{
			USLookAtObjectEvent lookAtObjectEvent = TargetEvent as USLookAtObjectEvent;
			
			float fadeInStartTime = lookAtObjectEvent.FireTime;
			float fadeInEndTime = lookAtObjectEvent.FireTime + lookAtObjectEvent.inCurve[lookAtObjectEvent.inCurve.length-1].time;
			
			float fadeOutStartTime = fadeInEndTime + lookAtObjectEvent.lookAtTime;
			float fadeOutEndTime = fadeOutStartTime + lookAtObjectEvent.outCurve[lookAtObjectEvent.outCurve.length-1].time;
			
			lookAtObjectEvent.Duration = fadeOutEndTime - fadeInStartTime;
			
			lookAtObjectEvent.lookAtTime = Mathf.Max(lookAtObjectEvent.lookAtTime, 0.0f);
			
			// Draw our Whole Box.
			if (lookAtObjectEvent.Duration > 0)
			{
				float endPosition = ConvertTimeToXPos(lookAtObjectEvent.FireTime + lookAtObjectEvent.Duration);
				myArea.width = endPosition - myArea.x;
			}
			DrawDefaultBox(myArea);
			
			Rect FadeInBox = myArea;
			// Draw our FadeInBox
			if (lookAtObjectEvent.Duration > 0)
			{
				float endPosition = ConvertTimeToXPos(fadeInStartTime + (fadeInEndTime - fadeInStartTime));
				FadeInBox.width = endPosition - myArea.x;
			}
			DrawDefaultBox(FadeInBox);
			
			Rect FadeOutBox = myArea;
			// Draw our FadeOutBox
			if (lookAtObjectEvent.Duration > 0)
			{
				float startPosition = ConvertTimeToXPos(fadeOutStartTime);
				float endPosition = ConvertTimeToXPos(fadeOutEndTime);
	
				FadeOutBox.x = startPosition;
				FadeOutBox.width = endPosition - startPosition;
			}
			DrawDefaultBox(FadeOutBox);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				
				if(lookAtObjectEvent.objectToLookAt != null)
					GUILayout.Label(lookAtObjectEvent.objectToLookAt.name, DefaultLabel);
				else
					GUILayout.Label("NULL", DefaultLabel);
			}
	
			return myArea;
		}
	}
}