using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
	public PlayerController player;
	public Animator anim;
	public FSM fsm;
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
	private float balanceRecoverTime = 4f;
	private float currentLoseBalance;
	private float[] hpAndQGStamp;
	

	void Start()
	{		
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);

		currentBalance = minBalance;
		balanceBar.SetMinBalance(minBalance);

		playerName = player.gameObject.name;
		EventCenter.Instance.AddEventListener(playerName + "GetDamage", DealDamage);
		InvokeRepeating("RecoverBalance", 1, 1);
	}

	private void DealDamage(object hpAndQg)
	{
		float[] hpAndQgArray = hpAndQg as float[];
		
		if(hpAndQgArray[0] < 0)
		{
			if(playerName == "PlayerB") SoundMgr.Instance.PlaySound("umm", false);
			else SoundMgr.Instance.PlaySound("ahh", false);
		}
		
		if(isLostBalance)
		{
			Debug.Log("一击必杀");
			if(hpAndQgArray[0] < 0)
			{	
				if(playerName == "PlayerB")
				{
					UIManager.Instance.ShowPanel<PkqFinishPanel>("PkqFinishPanel", E_UI_Layer.Top);
					SoundMgr.Instance.PlaySound("ChuSfx",false);
				} 
				else
				{
					UIManager.Instance.ShowPanel<MwFinishPanel>("MwFinishPanel", E_UI_Layer.Top);
					SoundMgr.Instance.PlaySound("LetYouDown",false);
				} 
				
				hpAndQGStamp = hpAndQgArray;
				GameOverManager.Instance.isStop = true;
				
				Invoke("TriggerPlayerFinish", 3);
			}	
		}
		
		if(!fsm.parameter.isDefend)
		{
			fsm.parameter.isHit = true;
			//anim.SetTrigger("BeHit");
		} 
		
		currentHealth += hpAndQgArray[0];	
		currentBalance += hpAndQgArray[1];

		healthBar.UpdateHealth(currentHealth);
		balanceBar.UpdateBalance(currentBalance);

		if(currentHealth <= 0 && !isLostBalance)
		{
			fsm.parameter.isDie = true;
			//anim.SetTrigger("Die");
			
			player.GetComponentInChildren<KeepRotation>().enabled = false;
			player.transform.GetChild(0).rotation = quaternion.identity;
			EventCenter.Instance.EventTrigger("PlayerDie", player.name);
		}
		
		if(currentBalance >= 100)
		{
			isLostBalance = true;
			player.isLostBalance = true;
			SoundMgr.Instance.PlaySound("WeakHeartBeat", false);
			LoseBalanceDot.SetActive(true);
			//currentLoseBalance += Time.deltaTime;
		}
	}
	
	// For Test Only
	void Update()
	{
		if(isLostBalance) currentLoseBalance += Time.deltaTime;
		
		//TODO:JIA SHI
		if(player.isDefend)
		{
			currentBalance += Time.deltaTime * 8;
			balanceBar.UpdateBalance(currentBalance);
		}
		
		if(currentLoseBalance >= balanceRecoverTime)
		{
			isLostBalance = false;
			player.isLostBalance = false;
			currentLoseBalance = 0;
			LoseBalanceDot.SetActive(false);
			
			currentBalance = 100 - 2.5F;
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
		if(currentBalance >= 0 && !isLostBalance && !GameOverManager.Instance.isStop && !player.isDefend)
		{
			currentBalance -= 2.5f;
			balanceBar.UpdateBalance(currentBalance);
		}
	}
	
	private void TriggerPlayerFinish()
	{
		currentHealth += hpAndQGStamp[0] < 0 ? -999 : 0;
		healthBar.UpdateHealth(currentHealth);
		
		fsm.parameter.isDie = true;
		//anim.SetTrigger("Die");
		
		player.GetComponentInChildren<KeepRotation>().enabled = false;
		player.transform.GetChild(0).rotation = quaternion.identity;
		EventCenter.Instance.EventTrigger("PlayerDie", player.name);
	}
}
