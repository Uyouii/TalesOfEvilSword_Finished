using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that lets one object look at another object over a given time frame. 
	/// </summary>
	[USequencerFriendlyName("Rotate")]
	[USequencerEvent("Transform/Rotate Object")]
	public class USRotateObjectEvent : USEventBase
	{
		/// <summary>
		/// How long should we look at our object.
                		/// </summary>
		public float rotateSpeedPerSecond = 90.0f;
		public Vector3 rotationAxis = Vector3.up;
		
		private Quaternion sourceOrientation = Quaternion.identity;
		private Quaternion previousRotation = Quaternion.identity;
	
	    public override void FireEvent()
	    {
			previousRotation = AffectedObject.transform.rotation;
			sourceOrientation = AffectedObject.transform.rotation;
	    }
	
	    public override void ProcessEvent(float deltaTime)
	    {
			AffectedObject.transform.rotation = sourceOrientation;
			AffectedObject.transform.Rotate(rotationAxis, rotateSpeedPerSecond * deltaTime);
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

		public void Update() 
		{
			if(Duration < 0.0f)
				Duration = 4.0f;
		}
	}
}