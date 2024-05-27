using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float TorqueForce = 120f;
	public float StandardAngularVelocity = 180f;
	public float JumpForce = 20f;
	private float TorqueToAdd = 0;
	private float JumpForceToAdd = 0;
	private bool canController = true;
	[Tooltip("The time to recover from attack")]
	public float recoverTimeInvulnerable = 0.5f; // The time to recover from attack
	[Tooltip("The time to recover from bounce")]
	public float recoverTimeBounce = 0.8f; // The time to recover from Bounce
	public GameObject defenseShied;
	public bool isDefend;
	public bool isJump;
	public float defendTime;
	private Rigidbody2D rig;
	private bool isDazed;
	private bool isInvulnerable;
	public bool isLostBalance;
	
	private Animator anim;
	
	private AudioSource defendSound;
	
	private KeyCode clockwiseKey, anticlockwiseKey, defendKey, jumpKey;
	private ResetAttack resetAttack;
	private bool recoverTimeBounceOver;
	public Parameter FMSParameter;
	
	// Start is called before the first frame update
	void Awake()
	{
		anim = GetComponent<Animator>();
		rig = GetComponent<Rigidbody2D>();
		resetAttack = GetComponentInChildren<ResetAttack>();
		FMSParameter = GetComponentInChildren<FSM>().parameter;
		
		if(gameObject.name == "PlayerA")
		{
			clockwiseKey = KeyCode.A;
			anticlockwiseKey = KeyCode.D;
			defendKey = KeyCode.S;
			jumpKey = KeyCode.W;
		}
		else if(gameObject.name == "PlayerB")
		{
			clockwiseKey = KeyCode.LeftArrow;
			anticlockwiseKey = KeyCode.RightArrow;
			defendKey = KeyCode.DownArrow;
			jumpKey = KeyCode.UpArrow;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.DrawLine(transform.position, transform.position + new Vector3(0,-1.1f,0), Color.red);
		TorqueToAdd = 0;
		JumpForceToAdd = 0;
		canController = !isDefend && !isDazed;
		
		if(isJump && !Input.GetKey(jumpKey)) GroundCheck();
		
		//Debug.Log(rig.angularVelocity);
		if(!GameOverManager.Instance.isStop)
		{
			if(canController)
			{
				if(Input.GetKey(clockwiseKey))
				{
					TorqueToAdd = TorqueForce;
				}
				if(Input.GetKey(anticlockwiseKey))
				{
					TorqueToAdd = -TorqueForce;
				}
				if(Input.GetKeyDown(jumpKey) && !isJump)
				{
					isJump = true;
					JumpForceToAdd = JumpForce;
					rig.AddForce(Vector2.up * JumpForceToAdd, ForceMode2D.Impulse);
				}
			}
			
			
			if(Input.GetKey(defendKey) && !isLostBalance && !isDazed)
			{
				
				if(defendSound == null && isDefend == false)
					SoundMgr.Instance.PlaySound("Block1", false, (s) =>
					{
						defendSound = s;
					});
					
				isDefend = true;
				// else
				// {
				// 	if(!defendSound.isPlaying)
				// 		SoundMgr.Instance.PlaySound("Block1", false, (s) =>
				// 		{
				// 			defendSound = s;
				// 		});
				// }
				
				FMSParameter.isDefend = true;
				//anim.SetBool("isDefend", true);
				
				defenseShied.SetActive(true);
				defendTime += Time.deltaTime;
			}
			else
			{
				if(isDefend)
				{
					isDefend = false;
					
					FMSParameter.isDefend = false;
					//anim.SetBool("isDefend", false);
					
					defendTime = 0;
					defenseShied.SetActive(false);
				} 
			}
		}
	}
	
	void FixedUpdate() 
	{
		rig.AddTorque(TorqueToAdd);	
		//Constrain the angular velocity of the weapon's rotation
		rig.angularDrag = Mathf.Abs(rig.angularVelocity)/StandardAngularVelocity;
	}
	
	void GroundCheck()
	{
		if(Physics2D.Raycast(transform.position, Vector2.down, 1.1f, 1 << LayerMask.NameToLayer("Ground")))
		{
			isJump = false;
		}
	}
	
	public void GetBounced(Vector2 HeadPosition, Vector2 contactPosition)
	{
		//TODO:调整弹反的力
		rig.AddForceAtPosition((HeadPosition - contactPosition) * Math.Max(Mathf.Abs(rig.angularVelocity), 50f) * 15f , contactPosition);
		isDazed = true;
		Invoke("RecoverFromDaze", recoverTimeBounce);
	}
	
	public void RecoverFromDaze()
	{
		isDazed = false;
	}
	
	#region vulnerability setting
	public void BeVulnerableDelay()
	{
		Invoke("GetVulnerable", recoverTimeInvulnerable);
	}
	
	private void GetVulnerable()
	{
		recoverTimeBounceOver = true;
		if(resetAttack.canResetAttack)
		{
			SetInvulnerable(false);
		}
	}
	
	public void SetInvulnerable(bool b)
	{
		isInvulnerable = b;
		recoverTimeBounceOver = false;
	}
	
	public bool GetInvulnerability()
	{
		return isInvulnerable;
	}
	
	public bool GetRecoverTimeBounceOver()
	{
		return recoverTimeBounceOver;
	}
	#endregion
}
