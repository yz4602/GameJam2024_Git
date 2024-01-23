using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
	private float[] hpAndQg = {-10f, -10f};

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//Debug.Log(collision.gameObject.name);
		
		foreach (ContactPoint2D contact in collision.contacts)
		{
			Collider2D collider = contact.collider;
			GameObject collidedObject = collider.gameObject;
			
			// Now you have the specific GameObject that was part of the collision
			if(collidedObject.layer == LayerMask.NameToLayer("Body"))
			{
				if(collision.gameObject.name == "PlayerB")
				{
					EventCenter.Instance.EventTrigger("PlayerBGetDamage", hpAndQg);
				}
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision)

	{
		
	}
}
