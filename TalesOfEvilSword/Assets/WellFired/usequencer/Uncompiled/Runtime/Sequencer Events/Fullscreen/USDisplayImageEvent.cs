using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will display an image at the given time. 
	/// </summary>
	[USequencerFriendlyName("Display Image")]
	[USequencerEvent("Fullscreen/Display Image")]
	[USequencerEventHideDuration()]
	public class USDisplayImageEvent : USEventBase 
	{
		/// <summary>
		/// The UILayer on which to display this image.
		/// </summary>
		public UILayer uiLayer = UILayer.Front;

		/// <summary>
		/// This curve defines how the image will be displayed, giving you absolute control.
		/// </summary>
		public AnimationCurve fadeCurve = new AnimationCurve(new Keyframe(0.0f, 0.0f), new Keyframe(1.0f, 1.0f), new Keyframe(3.0f, 1.0f), new Keyframe(4.0f, 0.0f));

		/// <summary>
		/// The image to display/
		/// </summary>
		public Texture2D displayImage = null;

		/// <summary>
		/// The display position.
		/// </summary>
		public UIPosition displayPosition = UIPosition.Center;

		/// <summary>
		/// The anchor position.
		/// </summary>
		public UIPosition anchorPosition = UIPosition.Center;
		
		private float currentCurveSampleTime = 0.0f;
		
		public override void FireEvent()
		{
			if(!displayImage)
				Debug.LogWarning("Trying to use a DisplayImage Event, but you didn't give it an image to display", this);
		}
		
		public override void ProcessEvent(float deltaTime)
		{		
			currentCurveSampleTime = deltaTime;
		}
		
		public override void EndEvent()
		{
			float alpha = fadeCurve.Evaluate(fadeCurve.keys[fadeCurve.length - 1].time);
			
			alpha = Mathf.Min(Mathf.Max(0.0f, alpha), 1.0f);
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{	
			currentCurveSampleTime = 0.0f;
		}
		
		void OnGUI()
		{	
			if(!Sequence.IsPlaying)
				return;
	
			float maxTime = 0.0f;
			foreach(Keyframe key in fadeCurve.keys)
			{
				if(key.time > maxTime)
					maxTime = key.time;
			}
			
			Duration = maxTime;
			
			float alpha = fadeCurve.Evaluate(currentCurveSampleTime);
			alpha = Mathf.Min(Mathf.Max(0.0f, alpha), 1.0f);
			
			if(!displayImage)
				return;
			
			Rect position = new Rect(Screen.width * 0.5f, Screen.height * 0.5f, displayImage.width, displayImage.height);
			
			switch(displayPosition)
			{
			case UIPosition.TopLeft:
				position.x = 0;
				position.y = 0;
				break;
			case UIPosition.TopRight:
				position.x = Screen.width;
				position.y = 0;
				break;
			case UIPosition.BottomLeft:
				position.x = 0;
				position.y = Screen.height;
				break;
			case UIPosition.BottomRight:
				position.x = Screen.width;
				position.y = Screen.height;
				break;
			}
			
			switch(anchorPosition)
			{
			case UIPosition.Center:
				position.x -= displayImage.width * 0.5f;
				position.y -= displayImage.height * 0.5f;
				break;
			case UIPosition.TopRight:
				position.x -= displayImage.width;
				break;
			case UIPosition.BottomLeft:
				position.y -= displayImage.height;
				break;
			case UIPosition.BottomRight:
				position.x -= displayImage.width;
				position.y -= displayImage.height;
				break;
			}
			
			GUI.depth = (int)uiLayer;
			Color prevColour = GUI.color;
			GUI.color = new Color(1.0f, 1.0f, 1.0f, alpha);
			GUI.DrawTexture(position, displayImage);
			GUI.color = prevColour;
		}
	}
}