using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that allows you to blend two animation clips (scrubbing does not work.). 
	/// </summary>
	[USequencerFriendlyName("Blend Animation No Scrub (Legacy)")]
	[USequencerEvent("Animation (Legacy)/Blend Animation No Scrub")]
	[USequencerEventHideDuration()]
	public class USBlendAnimNoScrubEvent : USEventBase 
	{
		/// <summary>
		/// The animation to blend.
		/// </summary>
		public AnimationClip blendedAnimation = null;
		
		public void Update() 
		{
			if(Duration < 0.0f)
				Duration = blendedAnimation.length;
		}
		
		public override void FireEvent()
		{	
			Animation animation = AffectedObject.GetComponent<Animation>();
			if(!animation)
			{
				Debug.Log("Attempting to play an animation on a GameObject without an Animation Component from USPlayAnimEvent.FireEvent");
				return;
			}
			
	  	 	animation[blendedAnimation.name].wrapMode = WrapMode.Once;
			animation[blendedAnimation.name].layer = 1;
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			GetComponent<Animation>().CrossFade(blendedAnimation.name);
		}
		
		public override void StopEvent()
		{
			if(!AffectedObject)
				return;
			
			Animation animation = AffectedObject.GetComponent<Animation>();
	        if (animation)
				animation.Stop();
		}
	}
}