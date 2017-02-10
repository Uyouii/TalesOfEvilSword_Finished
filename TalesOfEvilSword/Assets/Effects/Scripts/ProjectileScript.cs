using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public GameObject impactParticle;
    public GameObject projectileParticle;
    public GameObject[] trailParticles;
    [HideInInspector]
    public Vector3 impactNormal; //Used to rotate impactparticle.

	public float impactRange = 2;

	private GameObject projectileParticleObj;
	private GameObject impactParticleObj;

	// Use this for initialization
	void Start () {
        projectileParticleObj = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
        projectileParticleObj.transform.parent = transform;
	}

	public void Impact(GameObject[] enemies) {

		//transform.DetachChildren();
		impactParticleObj = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
		//Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

		//if ( hit.gameObject.tag == "Enemy" ) // Projectile will destroy objects tagged as Destructible
		//{
		//	//Destroy(hit.gameObject);
		//	hit.gameObject.GetComponent<EnemyCharacter>( ).BeAttacked( );
		//}
		foreach(var enemy in enemies ) {
			if( Vector3.Distance(enemy.transform.position, this.transform.position) < impactRange ) {
				enemy.GetComponent<EnemyCharacter>( ).BeAttacked( );
			}
		}

		//yield WaitForSeconds (0.05);
		foreach ( GameObject trail in trailParticles ) {
			var tmp = transform.Find(projectileParticle.name + "/" + trail.name);
			if ( tmp == null )
				continue;
			GameObject curTrail = tmp.gameObject;
			curTrail.transform.parent = null;
			Destroy(curTrail, 3f);
		}
		Destroy(projectileParticleObj, 3f);
		Destroy(impactParticleObj, 5f);
		Destroy(this.gameObject);
		//projectileParticle.Stop();

	}

	// Update is called once per frame
	void OnCollisionEnter (Collision hit) {

        //transform.DetachChildren();
        impactParticleObj = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal)) as GameObject;
        //Debug.DrawRay(hit.contacts[0].point, hit.contacts[0].normal * 1, Color.yellow);

        if (hit.gameObject.tag == "Enemy") // Projectile will destroy objects tagged as Destructible
        {
			//Destroy(hit.gameObject);
			hit.gameObject.GetComponent<EnemyCharacter>( ).BeAttacked( );
        }

        //yield WaitForSeconds (0.05);
        foreach (GameObject trail in trailParticles)
	    {
			var tmp = transform.Find(projectileParticle.name + "/" + trail.name);
			if ( tmp == null )
				continue;
			GameObject curTrail = tmp.gameObject;
            curTrail.transform.parent = null;
            Destroy(curTrail, 3f); 
	    }
        Destroy(projectileParticleObj, 3f);
        Destroy(impactParticleObj, 5f);
        Destroy(this.gameObject);
        //projectileParticle.Stop();

	}
}
