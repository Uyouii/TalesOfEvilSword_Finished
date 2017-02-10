using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that allows you to blend two Legacy animation clips together.
	/// This is legacy functionality and will likely be removed in the future.
	/// </summary>
	[USequencerFriendlyName("Blend Animation (Legacy)")]
	[USequencerEvent("Animation (Legacy)/Blend Animation")]
	public class USBlendAnimEvent : USEventBase 
	{
		/// <summary>
		/// The anim clip to blend from.
		/// </summary>
		public AnimationClip animationClipSource = null;

		/// <summary>
		/// The anim clip to blend too.
		/// </summary>
		public AnimationClip animationClipDest = null;

		/// <summary>
		/// The point at which we will start our blend.
		/// </summary>
		public float blendPoint = 1.0f;
		
		public void Update() 
		{
			if(Duration < 0.0f)
				Duration = 2.0f;
		}
		
		public override void FireEvent()
		{	
			Animation animation = AffectedObject.GetComponent<Animation>();
			if(!animation)
			{
				Debug.Log("Attempting to play an animation on a GameObject without an Animation Component from USPlayAnimEvent.FireEvent");
				return;
			}
			
	        animation.wrapMode = WrapMode.Loop;
	        animation.Play(animationClipSource.name);
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			Animation animation = AffectedObject.GetComponent<Animation>();
	
			if (!animation)
			{
				Debug.LogError("Trying to play an animation : " + animationClipSource.name + " but : " + AffectedObject + " doesn't have an animation component, we will add one, this time, though you should add it manually");
				animation = AffectedObject.AddComponent<Animation>();
			}
	
			if (animation[animationClipSource.name] == null)
			{
				Debug.LogError("Trying to play an animation : " + animationClipSource.name + " but it isn't in the animation list. I will add it, this time, though you should add it manually.");
				animation.AddClip(animationClipSource, animationClipSource.name);
			}
	
			if (animation[animationClipDest.name] == null)
			{
				Debug.LogError("Trying to play an animation : " + animationClipDest.name + " but it isn't in the animation list. I will add it, this time, though you should add it manually.");
				animation.AddClip(animationClipDest, animationClipDest.name);
			}
	
			if(deltaTime < blendPoint)
				animation.CrossFade(animationClipSource.name);
			else
				animation.CrossFade(animationClipDest.name);
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