using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MyGUI : MonoBehaviour {

    public GUISkin skin;
    public float buttonsX;
    public float buttonsY;
    public float buttonsSizeX;
    public float buttonsSizeY;
    public float buttonsDistance;

    public float labelX;
    public float labelY;
    public float labelSizeX;
    public float labelSizeY;

    string impactParticleName;
    string projectileParticleName;

    FireProjectile effectScript;

    float originalWidth = 1920f;  // define here the original resolution
    float originalHeight = 1080f; // you used to create the GUI contents 
    private Vector3 scale;

    void Start()
    {
        effectScript = GameObject.Find("FireProjectile").GetComponent<FireProjectile>();
        getEffectNames();
    }

	void Update()
	{
		getEffectNames();
	}

	void OnGUI () {
        GUI.skin = skin;

        scale.x = Screen.width / originalWidth; // calculate hor scale
        scale.y = Screen.height / originalHeight; // calculate vert scale
        scale.z = 1;
        var svMat = GUI.matrix; // save current matrix
        // substitute matrix - only scale is altered from standard
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, scale);

        if (GUI.Button(new Rect(buttonsX, buttonsY, buttonsSizeX, buttonsSizeY), "Previous"))
        {
            effectScript.previousEffect();
            getEffectNames();
        }

        if (GUI.Button(new Rect(buttonsX + buttonsDistance, buttonsY, buttonsSizeX, buttonsSizeY), "Next"))
        {
            effectScript.nextEffect();
            getEffectNames();
        }

        GUI.Label(new Rect(labelX, labelY, labelSizeX, labelSizeY), projectileParticleName + "\n" + impactParticleName + "\n\n" + "Click to fire a missile!");
		//GUI.Label(new Rect(labelX, labelY, labelSizeX, labelSizeY), "Current effects:"  + "\n\n" + projectileParticleName + "\n" + impactParticleName + "\n\n" + "Click to fire");

        GUI.matrix = svMat; // restore matrix
	}

    public bool overButton()
    {
        Rect button1 = new Rect(buttonsX, buttonsY, buttonsSizeX, buttonsSizeY);
        Rect button2 = new Rect(buttonsX + buttonsDistance, buttonsY, buttonsSizeX, buttonsSizeY);
        
        if (button1.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)) ||
            button2.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y)))
        {
            return true;
        }
        else
            return false;
    }

    void getEffectNames()
    {
        ProjectileScript projectileScript = effectScript.projectiles[effectScript.currentProjectile].GetComponent<ProjectileScript>();
        projectileParticleName = projectileScript.projectileParticle.name;
        impactParticleName = projectileScript.impactParticle.name;
    }

}
