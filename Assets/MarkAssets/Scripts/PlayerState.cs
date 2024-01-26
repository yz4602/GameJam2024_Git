using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	public GameObject player;
	private string playerName;
	
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

		playerName = player.name;
		EventCenter.Instance.AddEventListener(playerName + "GetDamage", DealDamage);
		InvokeRepeating("RecoverBalance", 1, 1);
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
			EventCenter.Instance.EventTrigger(playerName + "Die", this);
		}
	}
	
	// For Test Only
	void Update()
	{
		// if (Input.GetMouseButtonDown(0))
		// {
		// 	float[] hg = {-30, 10};
		// 	EventCenter.Instance.EventTrigger(player.name + "GetDamage", hg);
		// }
	}
	
	private void OnDestroy() 
	{
		EventCenter.Instance.RemoveEventListener(playerName + "GetDamage", DealDamage);
		CancelInvoke();
	}
	
	private void RecoverBalance()
	{
		if(currentBalance >= 0)
		{
			currentBalance -= 5f;
			balanceBar.UpdateBalance(currentBalance);
		}
	}
}
