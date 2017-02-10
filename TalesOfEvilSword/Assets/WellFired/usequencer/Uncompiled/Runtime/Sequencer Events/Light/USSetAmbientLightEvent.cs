using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will set the ambient light at a given time. 
	/// </summary>
	[USequencerFriendlyName("Set Ambient Light")]
	[USequencerEvent("Light/Set Ambient Light")]
	[USequencerEventHideDuration()]
	public class USSetAmbientLightEvent : USEventBase
	{
	    public Color lightColor = Color.red;
		private Color prevLightColor;
		
		public override void FireEvent()
		{
			prevLightColor = RenderSettings.ambientLight;
			RenderSettings.ambientLight = lightColor;
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			RenderSettings.ambientLight = prevLightColor;
		}
	}
}