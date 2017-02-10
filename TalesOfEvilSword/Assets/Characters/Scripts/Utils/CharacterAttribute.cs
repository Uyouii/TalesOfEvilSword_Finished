using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute : MonoBehaviour {

	public int HP;
	public int MP;
	public float attack;
	public float defence;
	public float attackDistance;
	public float skillDistance;


	public GameObject damageTextObject;

	public GameObject damageEffect;

	public int damageTextDuring = 3;

	private GameObject effectContainer;

	private List<GameObject> damageTexts = new List<GameObject>( );

	private bool isDeath;
	public bool IsDeath
	{
		get { return isDeath; }
	}


	// Use this for initialization
	void Start( ) {

		//HP = 100;
		//MP = 100;
		//attack = 10.0f;
		//defence = 10.0f;
		//attackDistance = 1.0f;
		//skillDistance = 7.0f;
		isDeath = false;

		effectContainer = GameObject.FindGameObjectWithTag("EffectContainer");
	}

	// Update is called once per frame
	void Update( ) {

		updateDamageText( );
	}

	void updateDamageText( ) {

		//var transform = mainCamera.transform.position;
		damageTexts.RemoveAll(item => item == null);
		//return;
		foreach ( var text in damageTexts ) {
			text.transform.Translate(new Vector3(0, 0.5f * Time.deltaTime, 0));
		}

	}

	public void TakeDamage( string str, bool isCritical = false ) {

		GameObject text = Instantiate(damageTextObject, this.transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
		text.GetComponent<TextMesh>( ).text = str;

		text.transform.LookAt(GameObject.FindGameObjectWithTag("MainCamera").gameObject.transform);
		text.transform.Rotate(new Vector3(0, 1, 0), 180);

		if ( isCritical ) {
			text.GetComponent<TextMesh>( ).color = Color.red;
		}
		
		damageTexts.Add(text);
		Destroy(text, 2f);           // last only 2 seconds

		GameObject effect = Instantiate(damageEffect) as GameObject;
		//effect.transform.SetParent(effectContainer.transform);
		effect.transform.position = this.gameObject.transform.position;
		effect.transform.position += new Vector3(0, 1, 0);
		Destroy(effect, 2f);

		this.HP -= int.Parse(str);
		if ( this.HP < 0 )
			isDeath = true;

	}
	

}
