using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace WellFired
{
	/// <summary>
	/// A custom event that will dissolve one camera into another camera.
	/// </summary>
	[USequencerFriendlyName("Dissolve Transition")]
	[USequencerEvent("Camera/Transition/Dissolve")]
	[ExecuteInEditMode]
	public class USCameraDissolveTransition : USEventBase
	{
		private WellFired.Shared.BaseTransition transition;

		[SerializeField]
		private Camera sourceCamera;
		
		[SerializeField]
		private Camera destinationCamera;

		private void OnGUI()
		{
			if(sourceCamera == null || destinationCamera == null || transition == null)
				return;

			transition.ProcessTransitionFromOnGUI();
		}
		
		public override void FireEvent()
		{
			if(transition == null)
				transition = new WellFired.Shared.BaseTransition();
			
			if(sourceCamera == null || destinationCamera == null || transition == null)
			{
				Debug.LogError("Can't continue this transition with null cameras.");
				return;
			}

			transition.InitializeTransition(sourceCamera, destinationCamera, new List<Camera>(), new List<Camera>(), WellFired.Shared.TypeOfTransition.Dissolve);
		}

		public override void ProcessEvent(float deltaTime)
		{
			if(sourceCamera == null || destinationCamera == null || transition == null)
				return;

			transition.ProcessEventFromNoneOnGUI(deltaTime, Duration);
		}

		public override void EndEvent()
		{
			if(sourceCamera == null || destinationCamera == null || transition == null)
				return;

			transition.TransitionComplete();
		}

		public override void StopEvent()
		{
			if(sourceCamera == null || destinationCamera == null || transition == null)
				return;

			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(sourceCamera == null || destinationCamera == null || transition == null)
				return;

			transition.RevertTransition();
		}
	}
}