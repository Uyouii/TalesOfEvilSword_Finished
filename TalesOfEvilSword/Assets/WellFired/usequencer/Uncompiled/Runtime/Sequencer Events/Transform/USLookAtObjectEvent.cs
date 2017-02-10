using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that lets one object look at another object over a given time frame. 
	/// </summary>
	[USequencerFriendlyName("Look At Object")]
	[USequencerEvent("Transform/Look At Object")]
	public class USLookAtObjectEvent : USEventBase
	{
		/// <summary>
		/// The object to Look At.
		/// </summary>
	    public GameObject objectToLookAt = null;

		/// <summary>
		/// The curve defining how we tween into our Look At.
		/// </summary>
		public AnimationCurve inCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 1.0f));

		/// <summary>
		/// The cureve defining how we tween out of our Look At.
		/// </summary>
		public AnimationCurve outCurve = new AnimationCurve(new Keyframe(0.0f, 1.0f), new Keyframe(1.0f, 0.0f));

		/// <summary>
		/// How long should we look at our object.
		/// </summary>
		public float lookAtTime = 2.0f;
		
		private Quaternion sourceOrientation = Quaternion.identity;
		private Quaternion previousRotation = Quaternion.identity;
	
	    public override void FireEvent()
	    {
			if(!objectToLookAt)
			{
				Debug.LogWarning("The USLookAtObject event does not provice a object to look at", this);
				return;
			}    
			
			previousRotation = AffectedObject.transform.rotation;
			sourceOrientation = AffectedObject.transform.rotation;
	    }
	
	    public override void ProcessEvent(float deltaTime)
	    {
			if(!objectToLookAt)
			{
				Debug.LogWarning("The USLookAtObject event does not provice a object to look at", this);
				return;
			}
			
			float inDuration = inCurve[inCurve.length-1].time;
			float holdDuration = lookAtTime + inDuration;
			
			float ratio = 1.0f;
			if(deltaTime <= inDuration)
				ratio = Mathf.Clamp(inCurve.Evaluate(deltaTime), 0.0f, 1.0f);
			else if(deltaTime >= holdDuration)
				ratio = Mathf.Clamp(outCurve.Evaluate(deltaTime - holdDuration), 0.0f, 1.0f);
			
			Vector3 sourcePosition = AffectedObject.transform.position;
			Vector3 destinationPosition = objectToLookAt.transform.position;
			Vector3 toTarget = destinationPosition - sourcePosition;
			Quaternion targetOrientation = Quaternion.LookRotation(toTarget);
			
			AffectedObject.transform.rotation = Quaternion.Slerp(sourceOrientation, targetOrientation, ratio);
	    }
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(!AffectedObject)
				return;
			
			AffectedObject.transform.rotation = previousRotation;
		}
	}
}