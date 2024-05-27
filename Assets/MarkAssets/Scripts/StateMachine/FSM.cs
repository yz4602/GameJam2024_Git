using System;
using System.Collections;
using System.Collections.Generic;
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
		
		TransitionState(StateType.Idle);
		
		parameter.animator = gameObject.GetComponent<Animator>();
	}

	void Update()
	{
		currentState.OnUpdate();
	}
	
	public void TransitionState(StateType type)
	{
		if (currentState != null)
			currentState.OnExit();
		currentState = states[type];
		currentState.OnEnter();
	}
}
