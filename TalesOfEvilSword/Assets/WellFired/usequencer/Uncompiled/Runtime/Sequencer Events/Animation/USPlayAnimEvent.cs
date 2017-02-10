using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that allows you to play back a Legacy Animation on a game object at a given time.
	/// If you want this animation to loop, simply set the wrapMode to loop.
	/// If you want to specify the duration of this animation, simply set the wrapMode to loop and then alter the duration.
	/// </summary>
	[USequencerFriendlyName("Play Animation (Legacy)")]
	[USequencerEvent("Animation (Legacy)/Play Animation")]
	public class USPlayAnimEvent : USEventBase 
	{
		/// <summary>
		/// The Animation Clip to play.
		/// </summary>
		public AnimationClip animationClip = null;

		/// <summary>
		/// The wrap mode for this Animation Clip.
		/// </summary>
	    public WrapMode wrapMode = WrapMode.Default;
	
		/// <summary>
		/// The playback speed.
		/// </summary>
		public float playbackSpeed = 1.0f;
		
		public void Update() 
		{
	        if (wrapMode != WrapMode.Loop && animationClip)
				Duration = animationClip.length / playbackSpeed;
		}
		
		public override void FireEvent()
		{
			if(!animationClip)
			{
				Debug.Log("Attempting to play an animation on a GameObject but you haven't given the event an animation clip from USPlayAnimEvent::FireEvent");
				return;
			}
			
			Animation animation = AffectedObject.GetComponent<Animation>();
			if(!animation)
			{
				Debug.Log("Attempting to play an animation on a GameObject without an Animation Component from USPlayAnimEvent.FireEvent");
				return;
			}
			
	        animation.wrapMode = wrapMode;
	        animation.Play(animationClip.name);
			
			AnimationState state = animation[animationClip.name];
			if(!state)
				return;
			
			state.speed = playbackSpeed;
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			Animation animation = AffectedObject.GetComponent<Animation>();
	
			if(!animationClip)
				return;

			if (!animation)
			{
				Debug.LogError("Trying to play an animation : " + animationClip.name + " but : " + AffectedObject + " doesn't have an animation component, we will add one, this time, though you should add it manually");
				animation = AffectedObject.AddComponent<Animation>();
			}
	
			if (animation[animationClip.name] == null)
			{
				Debug.LogError("Trying to play an animation : " + animationClip.name + " but it isn't in the animation list. I will add it, this time, though you should add it manually.");
				animation.AddClip(animationClip, animationClip.name);
			}
	
			AnimationState state = animation[animationClip.name];
	
	        if (!animation.IsPlaying(animationClip.name))
	        {
	            animation.wrapMode = wrapMode;
	            animation.Play(animationClip.name);
	        }
			
			state.speed = playbackSpeed;
			state.time = deltaTime * playbackSpeed;
			state.enabled = true;
			animation.Sample();
			state.enabled = false;
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