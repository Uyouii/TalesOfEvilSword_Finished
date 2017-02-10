using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public Vector3 startPosition;
	public float underY;
	
	void Start(){
		transform.position = startPosition;	
	}
	
	void Update(){
		
		if (transform.position.y< underY){
			transform.position = startPosition;	
		}
	}
}

