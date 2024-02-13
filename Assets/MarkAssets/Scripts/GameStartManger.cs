using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartManger : MonoBehaviour
{
	public Text CountDownText;
	public GameObject[] weapons;
	
	// Start is called before the first frame update
	void Start()
	{
		SoundMgr.Instance.PlayBKMusic("BGM1_battle");
		SoundMgr.Instance.PlaySound("321go", false);
		GameOverManager.Instance.isStop = true;
		StartCoroutine(DelayStart());
		DetermineCharacter();
	}
	
	
	void Update() 
	{
		Camera.main.aspect = 1920f / 1080f;
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

	private void DetermineCharacter()
	{
		if(SelectedPokemon.Instance != null)
		{
			if(SelectedPokemon.Instance.playerAPokemon == "Pikachu"){}
			else
			{
				weapons[0].SetActive(false);
				weapons[1].SetActive(true);
				weapons[2].SetActive(true);
				weapons[3].SetActive(false);
			} 
		}
	}
}
