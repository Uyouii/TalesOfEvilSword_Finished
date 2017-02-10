using UnityEngine;
using System.Collections;

public class Fog : MonoBehaviour {
	
	public Color fogColor;
	
	void Start(){
	
		RenderSettings.fogColor = fogColor;
	}
}
