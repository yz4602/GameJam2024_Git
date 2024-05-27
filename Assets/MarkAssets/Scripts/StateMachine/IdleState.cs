using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
	private FSM manager;
	private Parameter parameter;
	
	public IdleState(FSM manager)
	{
		this.manager = manager;
		this.parameter = manager.parameter;
	}
	
	public void OnEnter()
	{
		parameter.animator.Play("Idle");
	}

	public void OnUpdate()
	{
		if(parameter.isHit)
		{		
			manager.TransitionState(StateType.BeHit);
			parameter.isHit = false;
		}
		if(parameter.isDie)
		{
			manager.TransitionState(StateType.Die);
		}
		if(parameter.isDefend)
		{
			manager.TransitionState(StateType.Block);
		}
	}
	
	public void OnExit()
	{
		
	}
}

public class BeHitState : IState
{
	private FSM manager;
	private Parameter parameter;
	private AnimatorStateInfo info;
	
	public BeHitState(FSM manager)
	{
		this.manager = manager;
		this.parameter = manager.parameter;
	}
	
	public void OnEnter()
	{
		parameter.animator.Play("BeHit");
	}

	public void OnUpdate()
	{
		info = parameter.animator.GetCurrentAnimatorStateInfo(0);
		
		if(parameter.isDie)
		{
			manager.TransitionState(StateType.Die);
		}
		if(info.normalizedTime >= .95f)
		{
			manager.TransitionState(StateType.Idle);
		}
	}

	public void OnExit()
	{
		
	}
}

public class BlockState : IState
{
	private FSM manager;
	private Parameter parameter;
	
	public BlockState(FSM manager)
	{
		this.manager = manager;
		this.parameter = manager.parameter;
	}
	public void OnEnter()
	{
		parameter.animator.Play("Block");
	}

	public void OnUpdate()
	{
		if(!parameter.isDefend)
		{
			manager.TransitionState(StateType.Idle);
		}
		parameter.currentBalance += Time.deltaTime * 8;
		parameter.balanceBar.UpdateBalance(parameter.currentBalance);
	}
	
	public void OnExit()
	{
		
	}
}

public class DieState : IState
{
	private FSM manager;
	private Parameter parameter;
	
	public DieState(FSM manager)
	{
		this.manager = manager;
		this.parameter = manager.parameter;
	}
	public void OnEnter()
	{
		parameter.animator.Play("Die");
	}

	public void OnUpdate()
	{
		
	}

	public void OnExit()
	{
		
	}
}