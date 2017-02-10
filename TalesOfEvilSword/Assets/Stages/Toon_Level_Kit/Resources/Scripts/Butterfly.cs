using UnityEngine;
using System.Collections;

public class Butterfly : MonoBehaviour {
	
	public Vector3 zoneSize = Vector3.one;
	public GameObject[] butterflyObjects;
	public int butterflyCount;
	
	public float maxSpeed=1;
	public float arrivalRadius = 0.2f;
	private Vector3[] targets;
	private Transform[] flies; 
	private Vector3[] velocities;
	

	void Start(){
		targets = new Vector3[butterflyCount];
		flies = new Transform[butterflyCount];
		velocities = new Vector3[butterflyCount];
		for(int i=0;i<butterflyCount;i++){
			GameObject go= (GameObject)Instantiate( butterflyObjects[ Random.Range(0,butterflyObjects.Length-1)], new Vector3( transform.position.x + Random.Range( -zoneSize.x,zoneSize.x)/2,transform.position.y + Random.Range( -zoneSize.y,zoneSize.y)/2,transform.position.z + Random.Range( -zoneSize.z,zoneSize.z)/2),Quaternion.identity);
			flies[i] = go.transform;
			targets[i] = GetRandomTarget(flies[i].position);	
		}
	}
	
	
	void Update(){
		for(int i=0;i<butterflyCount;i++){
			flies[i].LookAt( targets[i]);
			
			if (Seek(i)){
				targets[i] = GetRandomTarget(flies[i].position);	
			}
		}
	}
	
	Vector3 GetRandomTarget(Vector3 position){
		return new Vector3( transform.position.x + Random.Range( -zoneSize.x,zoneSize.x)/2f,transform.position.y + Random.Range( -zoneSize.y,zoneSize.y)/2f,transform.position.z + Random.Range( -zoneSize.z,zoneSize.z)/2f);
	}
	
	bool Seek( int index){
		
		flies[index].position += velocities[index] ;
		
		Vector3 linear = targets[index] - flies[index].position;
		
		
		
		if (linear.magnitude>arrivalRadius){
			linear.Normalize();
			linear *= maxSpeed* Time.deltaTime;
			
			velocities[index] = linear;
				
		}
		else{
			return true;	
		}
		
		
		return false;
	}
	
}
