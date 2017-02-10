using UnityEditor;
using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event editor, this renders a custom display for the USPlayAudioEvent event in the uSequencer window. 
	/// </summary>
	[CustomUSEditor(typeof(USPlayAudioEvent))]
	public class USPlayAudioEventEditor : USEventBaseEditor
	{
		Texture2D audioWaveTexture = null;
		
		public override Rect RenderEvent(Rect myArea)
		{
			USPlayAudioEvent audioEvent = TargetEvent as USPlayAudioEvent;
			
			if (audioEvent.Duration > 0)
			{
				float endPosition = ConvertTimeToXPos(audioEvent.FireTime + audioEvent.Duration);
				myArea.width = endPosition - myArea.x;
			}
	
			if (audioEvent && audioEvent.loop)
			{
				float endPosition = ConvertTimeToXPos(audioEvent.Sequence.Duration);
				myArea.width = endPosition - myArea.x;
			}
	
			DrawDefaultBox(myArea);
	
			using(new WellFired.Shared.GUIBeginArea(myArea))
			{
				if(audioWaveTexture)
				{
					audioWaveTexture.filterMode = FilterMode.Point;
					audioWaveTexture.anisoLevel = 1;
				
					Rect imageArea = new Rect(0.0f, 0.0f, audioWaveTexture.width, myArea.height);
					GUI.DrawTexture(imageArea, audioWaveTexture, ScaleMode.StretchToFill);
				}
			
				GUILayout.Label(GetReadableEventName(), DefaultLabel);
				if (audioEvent)
					GUILayout.Label("Audio : " + audioEvent.audioClip, DefaultLabel);
			}
	
			return myArea;
		}
	}
}