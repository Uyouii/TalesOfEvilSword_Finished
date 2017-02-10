using UnityEngine;
using System.Collections;

namespace WellFired
{
	/// <summary>
	/// A custom event that will start a Legacy Emitter. 
	/// </summary>
	[USequencerFriendlyName("Start Emitter (Legacy)")]
	[USequencerEvent("Particle System/Start Emitter")]
	[USequencerEventHideDuration()]
	public class USParticleEmitterStartEvent : USEventBase 
	{
		public void Update() 
		{
			if(!AffectedObject)
				return;
			
			ParticleSystem particleSystem = AffectedObject.GetComponent<ParticleSystem>();
			
			if(particleSystem)
				Duration = particleSystem.duration + particleSystem.startLifetime;
		}
		
		public override void FireEvent()
		{	
			if(!AffectedObject)
				return;
			
			ParticleSystem particleSystem = AffectedObject.GetComponent<ParticleSystem>();
			if(!particleSystem)
			{
				Debug.Log("Attempting to emit particles, but the object has no particleSystem USParticleEmitterStartEvent::FireEvent");
				return;
			}
			
			if(!Application.isPlaying)
				return;
			
			particleSystem.Play();
		}
		
		public override void ProcessEvent(float deltaTime)
		{
			if(Application.isPlaying)
				return;
				
			ParticleSystem particleSystem = AffectedObject.GetComponent<ParticleSystem>();
			particleSystem.Simulate(deltaTime);
		}
		
		public override void StopEvent()
		{
			UndoEvent();
		}
		
		public override void UndoEvent()
		{	
			if(!AffectedObject)
				return;
			
			ParticleSystem particleSystem = AffectedObject.GetComponent<ParticleSystem>();
			if(particleSystem)
				particleSystem.Stop();
		}
	}
}