using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
	float[] HpAndQg = {0, 0};
	private void Start() 
	{
		Dead();	
	}
	
	// Start is called before the first frame update
	void Dead()
	{
		HpAndQg[0] = -20f;
		HpAndQg[1] = -10f;
		EventCenter.Instance.EventTrigger("MonsterDie", HpAndQg);
	}
}
