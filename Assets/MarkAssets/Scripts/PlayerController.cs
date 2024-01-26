using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float TorqueForce = 120f;
	public float StandardAngularVelocity = 180f;
	private float TorqueToAdd = 0;
	private bool canController = true;
	[Tooltip("The time to recover from bounce and attack")]
	public float recoverTime = 0.3f; // The time to recover from bounce and attack
	public GameObject defenseShied;
	public bool isDefend;
	public float defendTime;
	private Rigidbody2D rig;
	private bool isDazed;
	private bool isInvulnerable;
	public bool isLostBalance;
	
	private KeyCode clockwiseKey, anticlockwiseKey, defendKey;
	
	// Start is called before the first frame update
	void Awake()
	{
		rig = GetComponent<Rigidbody2D>();
		if(gameObject.name == "PlayerA")
		{
			clockwiseKey = KeyCode.A;
			anticlockwiseKey = KeyCode.D;
			defendKey = KeyCode.S;
		}
		else if(gameObject.name == "PlayerB")
		{
			clockwiseKey = KeyCode.LeftArrow;
			anticlockwiseKey = KeyCode.RightArrow;
			defendKey = KeyCode.DownArrow;
		}
	}
	
	// Update is called once per frame
	void Update()
	{
		//Debug.Log(rig.angularVelocity);
		TorqueToAdd = 0;
		canController = !isDefend && !isDazed;
		
		//Debug.Log(rig.angularVelocity);
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
		}
		
		if(Input.GetKey(defendKey))
		{
			isDefend = true;
			defenseShied.SetActive(true);
			defendTime += Time.deltaTime;
		}
		else
		{
			if(isDefend)
			{
				isDefend = false;
				defendTime = 0;
				defenseShied.SetActive(false);
			} 
		}
	}
	
	void FixedUpdate() 
	{
		rig.AddTorque(TorqueToAdd);	
		//Constrain the angular velocity of the weapon's rotation
		rig.angularDrag = Mathf.Abs(rig.angularVelocity)/StandardAngularVelocity;
	}
	
	public void GetBounced(Vector2 HeadPosition, Vector2 contactPosition)
	{
		//TODO:调整弹反的力
		rig.AddForceAtPosition((HeadPosition - contactPosition) * Math.Max(Mathf.Abs(rig.angularVelocity), 50f) * 15f , contactPosition);
		isDazed = true;
		Invoke("RecoverFromDaze", recoverTime);
	}
	
	public void RecoverFromDaze()
	{
		isDazed = false;
	}
	
	#region vulnerability setting
	public void BeVulnerableDelay()
	{
		Invoke("GetVulnerable", recoverTime);
	}
	
	private void GetVulnerable()
	{
		SetInvulnerable(false);
	}
	
	public void SetInvulnerable(bool b)
	{
		isInvulnerable = b;
	}
	
	public bool GetInvulnerability()
	{
		return isInvulnerable;
	}
	#endregion
}
