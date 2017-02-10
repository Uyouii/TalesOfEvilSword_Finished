using UnityEngine;
using System.Collections;

namespace WellFired
{
	[RequireComponent(typeof(Collider))]
	public class SequenceTrigger : MonoBehaviour
	{
		public bool isPlayerTrigger = false;
		public bool isMainCameraTrigger = false;
		
		public GameObject triggerObject = null;
		
		public USSequencer sequenceToPlay = null;
		
	    void OnTriggerEnter(Collider other) 
		{	
			if(!sequenceToPlay)
			{
				Debug.LogWarning("You have triggered a sequence in your scene, however, you didn't assign it a Sequence To Play", gameObject);
				return;
			}
			
			if(sequenceToPlay.IsPlaying)
				return;
			
			if(other.CompareTag("MainCamera") && isMainCameraTrigger)
			{
				sequenceToPlay.Play();
				return;
			}
			
			if(other.CompareTag("Player") && isPlayerTrigger)
			{
				sequenceToPlay.Play();
				return;
			}
			
			if(other.gameObject == triggerObject)
			{
				sequenceToPlay.Play();
				return;
			}
		}
	}
}