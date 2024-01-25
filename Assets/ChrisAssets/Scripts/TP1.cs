using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP1 : MonoBehaviour
{
	public float currentHealth;
	public float maxHealth = 300;

	public HealthBar healthBar;

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
		EventCenter.Instance.AddEventListener("PlayerBGetDamage", DealDamage);
	}

	private void DealDamage(object hpAndQg)
	{
		float[] hpAndQgArray = hpAndQg as float[];
		currentHealth += hpAndQgArray[0];
		healthBar.UpdateHealth(currentHealth);
		if(currentHealth <= 0)
		{
			EventCenter.Instance.EventTrigger("PlayerDie", this);
		}
	}
	
	// For Test Only
	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			float[] hg = {-30, 10};
			EventCenter.Instance.EventTrigger("PlayerBGetDamage", hg);
		}
	}
	
	private void OnDestroy() 
	{
		EventCenter.Instance.RemoveEventListener("PlayerBGetDamage", DealDamage);
	}
}
