using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
	private string _playerName;
	// Start is called before the first frame update
	void Start()
	{
		EventCenter.Instance.AddEventListener("PlayerDie", PlayerDieDo);
	}
	
	private void PlayerDieDo(object playerName)
	{
		_playerName = playerName as string;
		UIManager.Instance.ShowPanel<ResultPanel>("ResultPanel");
		Invoke("ChangeTextOnResultPanel", 0.05f);
	}
	
	private void ChangeTextOnResultPanel()
	{
		ResultPanel resultPanel = UIManager.Instance.GetPanel<ResultPanel>("ResultPanel");
		Debug.Log(resultPanel == null);
		resultPanel.GetControl("Title").text = _playerName + " Die";
	}
}
