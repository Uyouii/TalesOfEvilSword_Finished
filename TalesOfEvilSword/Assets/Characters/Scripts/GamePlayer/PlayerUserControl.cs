using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;


[RequireComponent(typeof(PlayerCharacter))]
public class PlayerUserControl : MonoBehaviour {
	private PlayerCharacter m_Character; // A reference to the PlayerCharacter on the object
	private Transform m_Cam;                  // A reference to the main camera in the scenes transform
	private Vector3 m_CamForward;             // The current forward direction of the camera
	private Vector3 m_Move;
	private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

	private static KeyCode[ ] AttackKeys = {
			KeyCode.J,				// For Jab
			KeyCode.H,				// For Kick
			KeyCode.U,				// For Rise
			KeyCode.L,				// For Offence
			KeyCode.Alpha1,		// For Skill1
			KeyCode.Alpha2,		// For Skill1
			KeyCode.Alpha3,		// For Skill1
			KeyCode.Alpha4,		// For Skill1
		};

	private static string[ ] Attacks = {
			"Jab",
			"Kick",
			"Rise",
			"Offence",
			"Skill1",
			"Skill2",
			"Skill3",
			"Skill4",
		};

	private void Start( ) {
		// get the transform of the main camera
		if ( Camera.main != null ) {
			m_Cam = Camera.main.transform;
		} else {
			Debug.LogWarning(
				"Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
			// we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
		}

		// get the third person character ( this should never be null due to require component )
		m_Character = GetComponent<PlayerCharacter>( );
	}


	public static string[] getAttackStrings( ) {
		return Attacks;
	}

	private void Update( ) {
		if ( !m_Jump ) {
			m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
	}


	// Fixed update is called in sync with physics
	private void FixedUpdate( ) {
		// read inputs
		float h = CrossPlatformInputManager.GetAxis("Horizontal");
		float v = CrossPlatformInputManager.GetAxis("Vertical");
		bool crouch = Input.GetKey(KeyCode.C);
		bool dodge = Input.GetKeyDown(KeyCode.LeftShift);

		for ( int i = 0; i < AttackKeys.Length; ++i ) {
			if ( Input.GetKeyDown(AttackKeys[i]) ) {
				m_Character.Attack(Attacks[i]);
				//m_Character.UpdateAnimator(Attacks[i]);
				//Input.ResetInputAxes( );
			}
		}

		if ( dodge ) {
			m_Character.UpdateAnimator("Dodge");
			//Input.ResetInputAxes( );
		}

		// calculate move direction to pass to character
		if ( m_Cam != null ) {
			// calculate camera relative direction to move:
			m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
			m_Move = v * m_CamForward + h * m_Cam.right;
		} else {
			// we use world-relative directions in the case of no main camera
			m_Move = v * Vector3.forward + h * Vector3.right;
		}
#if !MOBILE_INPUT
		// walk speed multiplier
		if ( Input.GetKey(KeyCode.LeftControl) ) m_Move *= 0.5f;
#endif

		// pass all parameters to the character control script
		// [ This class could ONLY invoke this function to control the character
		m_Character.Move(m_Move, crouch, m_Jump);
		m_Jump = false;
		//Debug.Log(m_Character.isAttacking( ));
	}



}