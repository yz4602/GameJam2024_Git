using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float TorqueForce = 120f;
	private float TorqueToAdd = 0;
	public float StandardAngularVelocity = 180f;
	Rigidbody2D rig;
	
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
		if(Input.GetKeyDown(KeyCode.W))
		{
			
		}
	}
	
	void FixedUpdate() 
	{
		rig.AddTorque(TorqueToAdd);	
		//Constrain the angular velocity of the weapon's rotation
		rig.angularDrag = Mathf.Abs(rig.angularVelocity)/StandardAngularVelocity;
	}
}
