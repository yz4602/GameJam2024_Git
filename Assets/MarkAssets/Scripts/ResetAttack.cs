using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : MonoBehaviour
{
	public bool canResetAttack = true;
	
	private void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.gameObject.tag == "Weapon")
			canResetAttack = false;
	}
	
	private void OnTriggerExit2D(Collider2D other) 
	{
		if(other.gameObject.tag == "Weapon")
		{
			canResetAttack = true;
			PlayerController pc = GetComponentInParent<PlayerController>();
			if(pc && pc.GetRecoverTimeBounceOver()) 
				GetComponentInParent<PlayerController>().SetInvulnerable(false);
		}			
	}
}
