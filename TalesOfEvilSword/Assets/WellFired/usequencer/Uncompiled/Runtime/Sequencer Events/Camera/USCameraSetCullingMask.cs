using UnityEngine;
using System.Collections;
using System;

namespace WellFired
{
	/// <summary>
	/// A custom event that will dissolve one camera into another camera.
	/// </summary>
	[USequencerFriendlyName("Set Culling Mask")]
	[USequencerEvent("Camera/Set Culling Mask")]
	[USequencerEventHideDuration]
	[ExecuteInEditMode]
	public class USCameraSetCullingMask : USEventBase
	{
		[SerializeField]
		private LayerMask newLayerMask;

		private int prevLayerMask;
		private Camera cameraToAffect;

		public override void FireEvent()
		{
			if(AffectedObject != null)
				cameraToAffect = AffectedObject.GetComponent<Camera>();

			if(cameraToAffect)
			{
				prevLayerMask = cameraToAffect.cullingMask;
				cameraToAffect.cullingMask = newLayerMask;
			}
		}

		public override void ProcessEvent(float deltaTime)
		{

		}

		public override void EndEvent()
		{

		}

		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(cameraToAffect)
				cameraToAffect.cullingMask = prevLayerMask;
		}
	}
}