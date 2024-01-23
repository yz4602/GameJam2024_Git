using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
	void Start()
	{
		EventCenter.Instance.AddEventListener("MonsterDie", MonsterDieTodo);
	}
	
	private void MonsterDieTodo(object obj)
	{
		Debug.Log("掉qg： " + (obj as float[])[0]);
		Debug.Log("Monster Die Task");
	}
	
	private void OnDestroy() 
	{
		EventCenter.Instance.RemoveEventListener("MonsterDie", MonsterDieTodo);
	}
}
