using UnityEngine;
using System.Collections;

namespace WellFired
{
	public class AutoPlaySequence : MonoBehaviour 
	{
		public USSequencer sequence = null;
		public float delay = 1.0f;
		
		private float currentTime = 0.0f;
		private bool hasPlayed = false;
		
		// Use this for initialization
		void Start() 
		{
			if(!sequence)
			{
				Debug.LogError("You have added an AutoPlaySequence, however you haven't assigned it a sequence", gameObject);
				return;
			}
		}
		
		void Update()
		{
			if(hasPlayed)
				return;
			
			currentTime += Time.deltaTime;
				
			if(currentTime >= delay && sequence)
			{
				sequence.Play();
				hasPlayed = true;
			}
		}
	}
}