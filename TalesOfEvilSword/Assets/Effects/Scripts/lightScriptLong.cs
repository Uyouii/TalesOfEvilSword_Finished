using UnityEngine;
using System.Collections;

public class lightScriptLong : MonoBehaviour 
{
	bool Impact = false;
	public float Sqr;
	// Use this for initialization
	void Start () 
	{
		Impact = true;
		gameObject.GetComponent<Light>().intensity = 5;
		Sqr = gameObject.GetComponent<Light>().intensity * gameObject.GetComponent<Light>().intensity * (( gameObject.GetComponent<Light>().intensity < 0.0f ) ? -1.0f : 1.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Impact)
		{
			gameObject.GetComponent<Light>().intensity -= (1.0f / Time.deltaTime) * Sqr * .00005f;
			if (gameObject.GetComponent<Light>().intensity <= 0)
			{
				Destroy (gameObject);
			}
		}
	}
}