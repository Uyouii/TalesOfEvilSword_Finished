using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will fade audio using the specified curve at a given time. 
	/// </summary>
	[USequencerFriendlyName("Fade Audio")]
	[USequencerEvent("Audio/Fade Audio")]
	[USequencerEventHideDuration()]
	public class USFadeAudioEvent : USEventBase 
	{
		private float previousVolume = 1.0f;
			
		public AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0.0f, 1.0f), new Keyframe(1.0f, 0));
		
		public void Update()
		{
			Duration = fadeCurve.length;
		}
		
		public override void FireEvent()
	    {
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	        if (!audio) 
			{
				Debug.LogWarning("Trying to fade audio on an object without an AudioSource");
				return;
			}
			
			previousVolume = audio.volume;
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	        if (!audio) 
			{
				Debug.LogWarning("Trying to fade audio on an object without an AudioSource");
				return;
			}
			
			audio.volume = fadeCurve.Evaluate(deltaTime);
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	        if (!audio) 
			{
				Debug.LogWarning("Trying to fade audio on an object without an AudioSource");
				return;
			}
			
			audio.volume = previousVolume;
		}
	}
}