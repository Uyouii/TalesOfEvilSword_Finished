using UnityEngine;
using System.Collections;

public class BillboardLaub: MonoBehaviour {

	private Transform mainCamTransform;
	private Transform cachedTransform;
	
	// Use this for initialization
	void Start () {
		mainCamTransform = Camera.main.transform;
		cachedTransform = transform;
	}
	
	void Update(){
	
		if (mainCamTransform.InverseTransformPoint( cachedTransform.position).z>=0){
			Vector3 v = mainCamTransform.position - cachedTransform.position;
			v.x=v.z=0;
			cachedTransform.LookAt( mainCamTransform.position-v);
			GetComponent<Renderer>().enabled = true;
		}
		else{
			GetComponent<Renderer>().enabled = false;	
		}

	}

}
