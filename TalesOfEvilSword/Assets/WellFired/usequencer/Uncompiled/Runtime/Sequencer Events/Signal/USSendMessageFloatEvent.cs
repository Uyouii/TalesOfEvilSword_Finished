using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that allows you to send a message to any script on the game object. 
	/// </summary>
	[USequencerFriendlyName("Send Message (Float)")]
	[USequencerEvent("Signal/Send Message (Float)")]
	[USequencerEventHideDuration()]
	public class USSendMessageFloatEvent : USEventBase 
	{
		public GameObject receiver = null;
		public string action = "OnSignal";
		
		[SerializeField]
		private float valueToSend;
		
		public override void FireEvent()
		{
			if(!Application.isPlaying)
				return;
			
			if(receiver)
				receiver.SendMessage(action, valueToSend);
			else
				Debug.LogWarning(string.Format("No receiver of signal \"{0}\" on object {1} ({2})", action, receiver.name, receiver.GetType().Name), receiver);
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			
		}
	}
}