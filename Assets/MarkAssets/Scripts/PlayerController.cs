using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float TorqueForce = 120f;
	public float StandardAngularVelocity = 180f;
	private float TorqueToAdd = 0;
	public bool isAttacked;
	public bool isDefend;
	public float defendTime;
	private Rigidbody2D rig;
	
	// Start is called before the first frame update
	void Start()
	{
		rig = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		TorqueToAdd = 0;
		//Debug.Log(rig.angularVelocity);
		if(Input.GetKey(KeyCode.A))
		{
			TorqueToAdd = TorqueForce;
		}
		if(Input.GetKey(KeyCode.D))
		{
			TorqueToAdd = -TorqueForce;
		}
		
		if(Input.GetKey(KeyCode.S))
		{
			isDefend = true;
			defendTime += Time.deltaTime;
		}
		else
		{
			isDefend = false;
			defendTime = 0;
		}
	}
	
	void FixedUpdate() 
	{
		rig.AddTorque(TorqueToAdd);	
		//Constrain the angular velocity of the weapon's rotation
		rig.angularDrag = Mathf.Abs(rig.angularVelocity)/StandardAngularVelocity;
	}
}
