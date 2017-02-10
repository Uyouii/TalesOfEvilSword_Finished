using UnityEngine;
using System.Collections;

namespace WellFired
{
	[ExecuteInEditMode()]
	public class AmbientLightAdjuster : MonoBehaviour 
	{
		public Color ambientLightColor = Color.red;
	
		// Update is called once per frame
		void Update () 
		{
			RenderSettings.ambientLight = ambientLightColor;
		}
	}
}