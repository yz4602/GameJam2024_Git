using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManger : MonoBehaviour
{
	public Text CountDownText;
	
	// Start is called before the first frame update
	void Start()
	{
		SoundMgr.Instance.PlayBKMusic("BGM1_battle");
		GameOverManager.Instance.isStop = true;
		StartCoroutine(DelayStart());
	}

	private IEnumerator DelayStart()
	{
		int number = 3;
		while(true)
		{
			if(number > 0)
			{
				CountDownText.text = number.ToString();
				number--;
			} 
			else
			{
				CountDownText.text = "Start!";
				break;
			} 
			yield return new WaitForSeconds(1);
		}
		
		GameOverManager.Instance.isStop = false;
		yield return new WaitForSeconds(1);
		CountDownText.text = "";
		CountDownText.gameObject.SetActive(false);
		yield return null;
	}
}
