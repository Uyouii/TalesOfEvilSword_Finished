using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that plays an audio file at the given time. 
	/// </summary>
	[USequencerFriendlyName("Play Audio")]
	[USequencerEvent("Audio/Play Audio")]
	[USequencerEventHideDuration()]
	public class USPlayAudioEvent : USEventBase {
	    public AudioClip audioClip = null;
		public bool loop = false;
	
		private bool wasPlaying = false;
	
		public void Update()
		{
			if (!loop && audioClip)
				Duration = audioClip.length;
			else
				Duration = -1;
		}
	
		public override void FireEvent()
	    {
	        AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	        if (!audio) 
			{
	            audio = AffectedObject.AddComponent<AudioSource>();
				audio.playOnAwake = false;
			}
			
			if(audio.clip != audioClip)
				audio.clip = audioClip;
				
			audio.time = 0.0f;
			audio.loop = loop;
		
			if(!Sequence.IsPlaying)
				return;
			
			audio.Play();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	        if (!audio) 
			{
	            audio = AffectedObject.AddComponent<AudioSource>();
				audio.playOnAwake = false;
			}
			
			if(audio.clip != audioClip)
				audio.clip = audioClip;
			
			if(audio.isPlaying)
				return;
			
			audio.time = deltaTime;
			
			if(Sequence.IsPlaying && !audio.isPlaying)
				audio.Play();
		}
		
		public override void ManuallySetTime(float deltaTime)
		{
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
			if(!audio)
				return;
			
			audio.time = deltaTime;
		}
		
		public override void ResumeEvent()
		{
	        AudioSource audio = AffectedObject.GetComponent<AudioSource>();
			if (!audio)
				return;
			
			audio.time = Sequence.RunningTime - FireTime;
			
			if(wasPlaying)
				audio.Play();
		}
		
		public override void PauseEvent() 
		{
			AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	
			wasPlaying = false;
			if (audio && audio.isPlaying)
				wasPlaying = true;
	
	        if (audio)
				audio.Pause();
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
	
		public override void EndEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{
			if(!AffectedObject)
				return;
			
	        AudioSource audio = AffectedObject.GetComponent<AudioSource>();
	        if (audio)
				audio.Stop();
		}
	}
}