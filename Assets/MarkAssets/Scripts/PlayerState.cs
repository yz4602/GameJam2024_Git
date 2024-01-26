using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	public GameObject player;
	public GameObject LoseBalanceDot;
	private string playerName;
	
	public float currentHealth;
	public float currentBalance;

	public float maxHealth = 300;
	public float minBalance = 0;

	public HealthBar healthBar;
	public BalanceBar balanceBar;
	
	[Header("Balance Related Parameter")]
	private bool isLostBalance;
	private float balanceRecoverTime = 3f;
	private float currentLoseBalance;

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
		
		if(currentBalance >= 100)
		{
			isLostBalance = true;
			LoseBalanceDot.SetActive(true);
			//currentLoseBalance += Time.deltaTime;
		}
	}
	
	// For Test Only
	void Update()
	{
		if(isLostBalance) currentLoseBalance += Time.deltaTime;
		if(currentLoseBalance >= balanceRecoverTime)
		{
			isLostBalance = false;
			currentLoseBalance = 0;
			LoseBalanceDot.SetActive(false);
			
			currentBalance = 100 - 5;
			balanceBar.UpdateBalance(currentBalance);
		}
	}
	
	private void OnDestroy() 
	{
		EventCenter.Instance.RemoveEventListener(playerName + "GetDamage", DealDamage);
		CancelInvoke();
	}
	
	private void RecoverBalance()
	{
		if(currentBalance >= 0 && !isLostBalance)
		{
			currentBalance -= 5f;
			balanceBar.UpdateBalance(currentBalance);
		}
	}
}
