using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TP1 : MonoBehaviour
{
	public float currentHealth;
	public float currentBalance;

	public float maxHealth = 300;
	public float minBalance = 0;

	public HealthBar healthBar;
	public BalanceBar balanceBar;

	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

        currentBalance = minBalance;
        balanceBar.SetMinBalance(minBalance);

        EventCenter.Instance.AddEventListener("PlayerBGetDamage", DealDamage);
	}

	private void DealDamage(object hpAndQg)
	{
		float[] hpAndQgArray = hpAndQg as float[];

		currentHealth += hpAndQgArray[0];
		currentBalance += hpAndQgArray[1];

		healthBar.UpdateHealth(currentHealth);
		balanceBar.UpdateBalance(currentBalance);

		if(currentHealth <= 0)
		{
			EventCenter.Instance.EventTrigger("PlayerBDie", this);
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
