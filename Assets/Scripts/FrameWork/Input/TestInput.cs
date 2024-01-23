using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		InputMgr.Instance.StartOrEndCheck(true);
		
		EventCenter.Instance.AddEventListener("KeyDown", CheckInputDown);
		EventCenter.Instance.AddEventListener("KeyUp", CheckInputUp);
	}

	private void CheckInputDown(object key)
	{
		KeyCode code = (KeyCode)key;
		switch(code)
		{
			case KeyCode.W:
				Debug.Log("W Down");
				break;
			case KeyCode.A:
				Debug.Log("A Down");
				break;
			case KeyCode.S:
				Debug.Log("S Down");
				break;
			case KeyCode.D:
				Debug.Log("D Down");
				break;
		}	
	}
	
	private void CheckInputUp(object key)
	{
		KeyCode code = (KeyCode)key;
		switch(code)
		{
			case KeyCode.W:
				Debug.Log("W Up");
				break;
			case KeyCode.A:
				Debug.Log("A Up");
				break;
			case KeyCode.S:
				Debug.Log("S Up");
				break;
			case KeyCode.D:
				Debug.Log("D Up");
				break;
		}	
	}
}
