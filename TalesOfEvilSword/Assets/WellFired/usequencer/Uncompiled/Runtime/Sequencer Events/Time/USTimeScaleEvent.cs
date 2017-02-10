using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that allows you to alter the game's TimeScale over a given time period with the specified curve. 
	/// </summary>
	[USequencerFriendlyName("Time Scale")]
	[USequencerEvent("Time/Time Scale")]
	[USequencerEventHideDuration()]
	public class USTimeScaleEvent : USEventBase 
	{
		/// <summary>
		/// This curve defines the TimeScale over a given time period.
		/// </summary>
	    public AnimationCurve scaleCurve = new AnimationCurve(new Keyframe(0.0f, 1.0f), new Keyframe(0.1f, 0.2f), new Keyframe(2.0f, 1.0f));
	    private float currentCurveSampleTime = 0.0f;
	    private float prevTimeScale = 1.0f;
	
	   public override void FireEvent()
	   {
	        prevTimeScale = Time.timeScale;
	   }
	   
	   public override void ProcessEvent(float deltaTime)
	   {
	        currentCurveSampleTime = deltaTime;
	        Time.timeScale = Mathf.Max(0f, scaleCurve.Evaluate(currentCurveSampleTime));
	   }
	
	    public override void EndEvent()
	    {
	        float sampleTime = scaleCurve.keys[scaleCurve.length - 1].time;
	        Time.timeScale = Mathf.Max(0f, scaleCurve.Evaluate(sampleTime));
	    }
	
	    public override void StopEvent()
	    {
	        UndoEvent();
	    }
	
	    public override void UndoEvent()
	    {
	        currentCurveSampleTime = 0.0f;
	        Time.timeScale = prevTimeScale;
	    }
	}
}