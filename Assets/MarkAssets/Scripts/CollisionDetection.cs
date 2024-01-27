using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
	public PlayerController player;
	private float[] hpAndQg = {-30f, 0f};
	private Vector2 contactPosition;

	private void OnCollisionEnter2D(Collision2D collision)
	{
		hpAndQg = new float[]{-300f, 0f};
		//Debug.Log(collision.gameObject.name);
		
		foreach (ContactPoint2D contact in collision.contacts)
		{
			Collider2D collider = contact.collider;
			GameObject collidedObject = collider.gameObject;			
			// Now you have the specific GameObject that was part of the collision
			if(collidedObject.layer == LayerMask.NameToLayer("Body"))
			{
				contactPosition = contact.point;
				PlayerController attackedPlayer = collision.gameObject.GetComponent<PlayerController>();
				if(!attackedPlayer.GetInvulnerability())
				{
					if(!CheckDefense(attackedPlayer))
					{
						EventCenter.Instance.EventTrigger(collision.gameObject.name + "GetDamage", hpAndQg);
						attackedPlayer.SetInvulnerable(true);
						attackedPlayer.BeVulnerableDelay();
					}
				}	
			}
		}
	}
	
	private bool CheckDefense(PlayerController attackedPlayer)
	{
		if(attackedPlayer.isDefend)
		{
			if(attackedPlayer.defendTime <= 0.1f)
			{
				player.GetBounced(transform.position, contactPosition);
				hpAndQg[0] = 0;
				hpAndQg[1] = 50;
				EventCenter.Instance.EventTrigger(player.gameObject.name + "GetDamage", hpAndQg);
				Debug.Log(player.name + " 被弹反");
			}
			else
			{
				hpAndQg[0] /= 2;
				hpAndQg[1] = 15;
				EventCenter.Instance.EventTrigger(attackedPlayer.gameObject.name + "GetDamage", hpAndQg);
				attackedPlayer.SetInvulnerable(true);
				attackedPlayer.BeVulnerableDelay();
				Debug.Log(attackedPlayer.name + " 防御");
			}
			return true; //Attack be blocked
		}
		return false; //Attack not be blocked
	}
}
