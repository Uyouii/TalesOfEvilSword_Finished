using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public abstract class BaseCharacter : MonoBehaviour {
	[SerializeField]
	protected float m_MovingTurnSpeed = 360;
	[SerializeField]
	protected float m_StationaryTurnSpeed = 180;
	[SerializeField]
	protected float m_JumpPower = 12f;
	[Range(1f, 4f)]
	[SerializeField]
	protected float m_GravityMultiplier = 2f;
	[SerializeField]
	protected float m_RunCycleLegOffset = 0.2f; //specific to the character in sample assets, will need to be modified to work with others
	[SerializeField]
	protected float m_MoveSpeedMultiplier = 1f;
	[SerializeField]
	protected float m_AnimSpeedMultiplier = 1f;
	[SerializeField]
	protected float m_GroundCheckDistance = 0.1f;

	protected Rigidbody m_Rigidbody;
	protected Animator m_Animator;
	protected bool m_IsGrounded;
	protected float m_OrigGroundCheckDistance;
	protected const float k_Half = 0.5f;
	protected float m_TurnAmount;
	protected float m_ForwardAmount;
	protected Vector3 m_GroundNormal;
	protected float m_CapsuleHeight;
	protected Vector3 m_CapsuleCenter;
	protected CapsuleCollider m_Capsule;
	protected bool m_Crouching;

	public void Start( ) {
		m_Animator = GetComponent<Animator>( );
		m_Rigidbody = GetComponent<Rigidbody>( );
		m_Capsule = GetComponent<CapsuleCollider>( );
		m_CapsuleHeight = m_Capsule.height;
		m_CapsuleCenter = m_Capsule.center;

		m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
		m_OrigGroundCheckDistance = m_GroundCheckDistance;
	}

	public void Move( Vector3 move, bool crouch, bool jump ) {

		// convert the world relative moveInput vector into a local-relative
		// turn amount and forward amount required to head in the desired
		// direction.
		if ( move.magnitude > 1f ) move.Normalize( );
		move = transform.InverseTransformDirection(move);
		CheckGroundStatus( );
		move = Vector3.ProjectOnPlane(move, m_GroundNormal);
		m_TurnAmount = Mathf.Atan2(move.x, move.z);
		m_ForwardAmount = move.z;

		ApplyExtraTurnRotation( );

		// control and velocity handling is different when grounded and airborne:
		if ( m_IsGrounded ) {
			HandleGroundedMovement(crouch, jump);
		} else {
			HandleAirborneMovement( );
		}

		ScaleCapsuleForCrouching(crouch);
		PreventStandingInLowHeadroom( );

		// send input and other state parameters to the animator
		UpdateAnimator(move);
	}




	protected void ScaleCapsuleForCrouching( bool crouch ) {
		if ( m_IsGrounded && crouch ) {
			if ( m_Crouching ) return;
			m_Capsule.height = m_Capsule.height / 2f;
			m_Capsule.center = m_Capsule.center / 2f;
			m_Crouching = true;
		} else {    // Not on ground or not crouch
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if ( Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore) ) {
				m_Crouching = true;
				return;
			}
			m_Capsule.height = m_CapsuleHeight;
			m_Capsule.center = m_CapsuleCenter;
			m_Crouching = false;
		}
	}

	protected void PreventStandingInLowHeadroom( ) {
		// prevent standing up in crouch-only zones
		if ( !m_Crouching ) {
			Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Capsule.radius * k_Half, Vector3.up);
			float crouchRayLength = m_CapsuleHeight - m_Capsule.radius * k_Half;
			if ( Physics.SphereCast(crouchRay, m_Capsule.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore) )     // check hit or not
			{
				m_Crouching = true;
			}
		}
	}



	protected virtual void UpdateAnimator( Vector3 move) {
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
		m_Animator.SetBool("Crouch", m_Crouching);
		m_Animator.SetBool("OnGround", m_IsGrounded);

		if ( !m_IsGrounded ) {
			m_Animator.SetFloat("Jump", m_Rigidbody.velocity.y);
		}


		// calculate which leg is behind, so as to leave that leg trailing in the jump animation
		// (This code is reliant on the specific run cycle offset in our animations,
		// and assumes one leg passes the other at the normalized clip times of 0.0 and 0.5)
		float runCycle =
			Mathf.Repeat(
				m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime + m_RunCycleLegOffset, 1);

		float jumpLeg = ( runCycle < k_Half ? 1 : -1 ) * m_ForwardAmount;

		if ( m_IsGrounded ) {
			m_Animator.SetFloat("JumpLeg", jumpLeg);
		}

		// the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
		// which affects the movement speed because of the root motion.
		if ( m_IsGrounded && move.magnitude > 0 ) {
			m_Animator.speed = m_AnimSpeedMultiplier;
		} else {
			// don't use that while airborne
			m_Animator.speed = 1;
		}
	}


	protected void HandleAirborneMovement( ) {
		// apply extra gravity from multiplier:
		Vector3 extraGravityForce = ( Physics.gravity * m_GravityMultiplier ) - Physics.gravity;
		m_Rigidbody.AddForce(extraGravityForce);

		m_GroundCheckDistance = m_Rigidbody.velocity.y < 0 ? m_OrigGroundCheckDistance : 0.01f;
	}


	protected void HandleGroundedMovement( bool crouch, bool jump ) {
		// check whether conditions are right to allow a jump:
		if ( jump && !crouch && m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Grounded") ) {
			// jump!
			m_Rigidbody.velocity = new Vector3(m_Rigidbody.velocity.x * 1.2f, m_JumpPower, m_Rigidbody.velocity.z * 1.2f);
			m_IsGrounded = false;
			m_Animator.applyRootMotion = false;
			m_GroundCheckDistance = 0.1f;
		}
	}

	protected void ApplyExtraTurnRotation( ) {
		// help the character turn faster (this is in addition to root rotation in the animation)
		float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
		transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
	}


	protected void OnAnimatorMove( ) {
		// we implement this function to override the default root motion.
		// this allows us to modify the positional speed before it's applied.
		if ( m_IsGrounded && Time.deltaTime > 0 ) {

			Vector3 moveForward = transform.forward * m_Animator.GetFloat("motionZ") * Time.deltaTime;

			Vector3 v = ( ( m_Animator.deltaPosition + moveForward ) * m_MoveSpeedMultiplier ) / Time.deltaTime;

			// we preserve the existing y part of the current velocity.
			v.y = m_Rigidbody.velocity.y;
			m_Rigidbody.velocity = v;
		}
	}


	protected void CheckGroundStatus( ) {
		RaycastHit hitInfo;
#if UNITY_EDITOR
		// helper to visualise the ground check ray in the scene view
		Debug.DrawLine(transform.position + ( Vector3.up * 0.1f ), transform.position + ( Vector3.up * 0.1f ) + ( Vector3.down * m_GroundCheckDistance ));
#endif
		// 0.1f is a small offset to start the ray from inside the character
		// it is also good to note that the transform position in the sample assets is at the base of the character
		if ( Physics.Raycast(transform.position + ( Vector3.up * 0.1f ), Vector3.down, out hitInfo, m_GroundCheckDistance) ) {
			if ( !hitInfo.collider.isTrigger ) {            // [ ONLY walk onto this ground if there is no any trigger ]
				m_GroundNormal = hitInfo.normal;
				m_IsGrounded = true;
				m_Animator.applyRootMotion = true;
			}
		} else {
			m_IsGrounded = false;
			m_GroundNormal = Vector3.up;
			m_Animator.applyRootMotion = false;
		}
	}
}
