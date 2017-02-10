using UnityEngine;
using System.Collections;

public class FireProjectile : MonoBehaviour {

    RaycastHit hit;
    public GameObject[] projectiles;
    public Transform spawnPosition;
    [HideInInspector]
    public int currentProjectile = 0;

    MyGUI _GUI;

	void Start () {
        _GUI = GameObject.Find("_GUI").GetComponent<MyGUI>();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextEffect();
			//getEffectNames();
        }

		if (Input.GetKeyDown(KeyCode.D))
		{
			nextEffect();
			//getEffectNames();
		}

		if (Input.GetKeyDown(KeyCode.A))
		{
			previousEffect();
			//getEffectNames();
		}
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            previousEffect();
			//getEffectNames();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_GUI.overButton())
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
                {
                    GameObject projectile = Instantiate(projectiles[currentProjectile], spawnPosition.position, Quaternion.identity) as GameObject;
                    projectile.transform.LookAt(hit.point);
                    projectile.GetComponent<Rigidbody>().AddForce(projectile.transform.forward * 1000);
                    projectile.GetComponent<ProjectileScript>().impactNormal = hit.normal;
                }  
            }
        }
        //Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction*100, Color.yellow);
	}

    public void nextEffect()
    {
        if (currentProjectile < projectiles.Length - 1)
            currentProjectile++;
        else
            currentProjectile = 0;
    }

    public void previousEffect()
    {
        if (currentProjectile > 0)
            currentProjectile--;
        else
            currentProjectile = projectiles.Length-1;
    }
}
