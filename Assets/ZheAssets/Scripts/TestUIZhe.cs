using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUIZhe : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		UIManager.Instance.ShowPanel<StartPanel>("StartPanel");
		//UIManager.Instance.ShowPanel<Player2SelectPanel>("Player2SelectPanel");
		//UIManager.Instance.ShowPanel<SelectPanel>("SelectPanel");
		//Invoke("HidePanel",1f);
	}

	// Update is called once per frame
	void HidePanel()
	{
		 UIManager.Instance.HidePanel("SelectPanel");
	}
}
