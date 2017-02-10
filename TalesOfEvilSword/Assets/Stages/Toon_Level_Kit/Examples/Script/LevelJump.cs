using UnityEngine;
using System.Collections;

public class LevelJump : MonoBehaviour {
	
	public string levelName;
	
	void OnTriggerEnter(){
		//Application.LoadLevel(levelName);
		Debug.Log("Enter Transport");
	}
	

}
