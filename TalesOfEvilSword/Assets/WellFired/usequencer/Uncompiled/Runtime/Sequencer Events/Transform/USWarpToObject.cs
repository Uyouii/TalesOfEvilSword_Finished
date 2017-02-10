using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// This custom event allows you to warp one object to the position of another object, optionally taking the target objects oriantation.
	/// </summary>
	[USequencerFriendlyName("Warp To Object")]
	[USequencerEvent("Transform/Warp To Object")]
	[USequencerEventHideDuration()]
	public class USWarpToObject : USEventBase
	{
	    public GameObject objectToWarpTo = null;
	    public bool useObjectRotation = false;
		
		private Transform previousTransform = null;
	
	    public override void FireEvent()
	    {
	        if (objectToWarpTo)
	        {
	            AffectedObject.transform.position = objectToWarpTo.transform.position;
	
	            if (useObjectRotation)
	            {
	                AffectedObject.transform.rotation = objectToWarpTo.transform.rotation;
	            }
	        }
	        else
	        {
	            Debug.LogError(AffectedObject.name + ": No Object attached to WarpToObjectSequencer Script");
	        }
	
			previousTransform = AffectedObject.transform;
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
			if(previousTransform)
			{
				AffectedObject.transform.position = previousTransform.position;
				AffectedObject.transform.rotation = previousTransform.rotation;
			}
		}
	}
}