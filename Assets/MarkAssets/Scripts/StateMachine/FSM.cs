using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum StateType
	{
		Idle, BeHit, Block, Die
	}
	
	[Serializable]
	public class Parameter
	{
		public Animator animator;
		public bool isDefend;
		public bool isHit;
		public bool isDie;
		
		public PlayerController player;
		public GameObject LoseBalanceDot;
		public string playerName;
		
		public float currentHealth;
		public float currentBalance;

		public float maxHealth = 300;
		public float minBalance = 0;

		public HealthBar healthBar;
		public BalanceBar balanceBar;
		
		[Header("Balance Related Parameter")]
		[HideInInspector] public bool isLostBalance;
		[HideInInspector] public float balanceRecoverTime = 4f;
		[HideInInspector] public float currentLoseBalance;
		[HideInInspector] public float[] hpAndQGStamp;
	}

public class FSM : MonoBehaviour
{
	private IState currentState;
	private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
	
	public Parameter parameter;
	
	void Start()
	{
		states.Add(StateType.Idle, new IdleState(this));
		states.Add(StateType.BeHit, new BeHitState(this));
		states.Add(StateType.Block, new BlockState(this));
		states.Add(StateType.Die, new DieState(this));
		
		parameter.player = gameObject.GetComponent<PlayerController>();  
		parameter.playerName = parameter.player.gameObject.name;
		
		parameter.currentHealth = parameter.maxHealth;
		parameter.healthBar.SetMaxHealth(parameter.maxHealth);

		parameter.currentBalance = parameter.minBalance;
		parameter.balanceBar.SetMinBalance(parameter.minBalance);

		
		EventCenter.Instance.AddEventListener(parameter.playerName + "GetDamage", DealDamage);
		InvokeRepeating("RecoverBalance", 1, 1);
		
		parameter.animator = gameObject.GetComponent<Animator>();
		
		TransitionState(StateType.Idle);
	}

	void Update()
	{
		if(parameter.isLostBalance) parameter.currentLoseBalance += Time.deltaTime;
		if(parameter.currentLoseBalance >= parameter.balanceRecoverTime)
		{
			parameter.isLostBalance = false;
			parameter.player.isLostBalance = false;
			parameter.currentLoseBalance = 0;
			parameter.LoseBalanceDot.SetActive(false);
			
			parameter.currentBalance = 100 - 2.5F;
			parameter.balanceBar.UpdateBalance(parameter.currentBalance);
		}
		
		currentState.OnUpdate();
	}
	
	public void TransitionState(StateType type)
	{
		if (currentState != null)
			currentState.OnExit();
		currentState = states[type];
		currentState.OnEnter();
	}
	
	private void DealDamage(object hpAndQg)
	{
		float[] hpAndQgArray = hpAndQg as float[];
		
		if(hpAndQgArray[0] < 0)
		{
			if(parameter.playerName == "PlayerB") SoundMgr.Instance.PlaySound("umm", false);
			else SoundMgr.Instance.PlaySound("ahh", false);
		}
		
		if(parameter.isLostBalance)
		{
			Debug.Log("一击必杀");
			if(hpAndQgArray[0] < 0)
			{	
				if(parameter.playerName == "PlayerB")
				{
					UIManager.Instance.ShowPanel<PkqFinishPanel>("PkqFinishPanel", E_UI_Layer.Top);
					SoundMgr.Instance.PlaySound("ChuSfx",false);
				} 
				else
				{
					UIManager.Instance.ShowPanel<MwFinishPanel>("MwFinishPanel", E_UI_Layer.Top);
					SoundMgr.Instance.PlaySound("LetYouDown",false);
				} 
				
				parameter.hpAndQGStamp = hpAndQgArray;
				GameOverManager.Instance.isStop = true;
				
				Invoke("TriggerPlayerFinish", 3);
			}	
		}
		
		if(!parameter.isDefend)
		{
			parameter.isHit = true;
			//anim.SetTrigger("BeHit");
		} 
		
		parameter.currentHealth += hpAndQgArray[0];	
		parameter.currentBalance += hpAndQgArray[1];

		parameter.healthBar.UpdateHealth(parameter.currentHealth);
		parameter.balanceBar.UpdateBalance(parameter.currentBalance);

		if(parameter.currentHealth <= 0 && !parameter.isLostBalance)
		{
			parameter.isDie = true;
			//anim.SetTrigger("Die");
			
			parameter.player.GetComponentInChildren<KeepRotation>().enabled = false;
			parameter.player.transform.GetChild(0).rotation = quaternion.identity;
			EventCenter.Instance.EventTrigger("PlayerDie", parameter.player.name);
		}
		
		if(parameter.currentBalance >= 100)
		{
			parameter.isLostBalance = true;
			parameter.player.isLostBalance = true;
			SoundMgr.Instance.PlaySound("WeakHeartBeat", false);
			parameter.LoseBalanceDot.SetActive(true);
			//currentLoseBalance += Time.deltaTime;
		}
	}

	private void OnDestroy() 
	{
		EventCenter.Instance.RemoveEventListener(parameter.playerName + "GetDamage", DealDamage);
		CancelInvoke();
	}
	
	private void RecoverBalance()
	{
		if(parameter.currentBalance >= 0 && !parameter.isLostBalance && !GameOverManager.Instance.isStop && !parameter.player.isDefend)
		{
			parameter.currentBalance -= 2.5f;
			parameter.balanceBar.UpdateBalance(parameter.currentBalance);
		}
	}
	
	private void TriggerPlayerFinish()
	{
		parameter.currentHealth += parameter.hpAndQGStamp[0] < 0 ? -999 : 0;
		parameter.healthBar.UpdateHealth(parameter.currentHealth);
		
		parameter.isDie = true;
		//anim.SetTrigger("Die");
		
		parameter.player.GetComponentInChildren<KeepRotation>().enabled = false;
		parameter.player.transform.GetChild(0).rotation = quaternion.identity;
		EventCenter.Instance.EventTrigger("PlayerDie", parameter.player.name);
	}
}
