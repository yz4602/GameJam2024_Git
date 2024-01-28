using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : SingletonMono<GameOverManager>
{
	private string _playerName;
	public bool isStop;
	// Start is called before the first frame update
	
	protected override void Awake()
	{
		base.Awake();	
		DontDestroyOnLoad(this);
	}
	
	void Start()
	{
		EventCenter.Instance.AddEventListener("PlayerDie", PlayerDieDo);
	}
	
	private void PlayerDieDo(object playerName)
	{
		isStop = true;
		_playerName = playerName as string;
		
		UIManager.Instance.ShowPanel<ResultPanel>("ResultPanel");
		Invoke("ChangeTextOnResultPanel", 0.1f);
	}
	
	private void ChangeTextOnResultPanel()
	{
		Debug.Log(UIManager.Instance.GetPanel<ResultPanel>("ResultPanel") == null);
		ResultPanel resultPanel = UIManager.Instance.GetPanel<ResultPanel>("ResultPanel");
		if(_playerName == "PlayerA")
		{
			SoundMgr.Instance.PlaySound("GreenfrogWin", false);
			resultPanel.GetControl("Title").text = "Green Frog" + " Win";
		}
		else
		{
			SoundMgr.Instance.PlaySound("PikachuWin", false);
			resultPanel.GetControl("Title").text = "Pikachu" + " Win";
		} 
	}
}
