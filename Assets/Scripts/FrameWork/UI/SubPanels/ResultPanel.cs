using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResultPanel : BasePanel {
	protected override void Awake()
	{
		//一定不能少 因为需要执行父类的awake来初始化一些信息 比如找控件 加事件监听
		base.Awake();
		//在下面处理自己的一些初始化逻辑
	}

	// Use this for initialization
	void Start () {
		GetControl<Button>("btnResume").interactable = false;
		GetControl<Button>("btnMain").interactable = false;
		Invoke("ActiveButtons", 2.2f);
	}

	public override void ShowMe()
	{
		base.ShowMe();
		//显示面板时 想要执行的逻辑 这个函数 在UI管理器中 会自动帮我们调用
		//只要重写了它  就会执行里面的逻辑
	}

	protected override void OnClick(string btnName)
	{
		switch(btnName)
		{
			case "btnResume":
				GameOverManager.Instance.isStop = false;
				UIManager.Instance.HidePanel("ResultPanel");
				ScenesMgr.Instance.LoadScene("MotionScene");
				Debug.Log("btnResume被点击");
				break;
			case "btnMain":
				Debug.Log("btnMain被点击");
				UIManager.Instance.HidePanel("ResultPanel");
				ScenesMgr.Instance.LoadScene("TestUIScene");
				//UIManager.Instance.ShowPanel<MainPanel>("MainPanel");
				break;			
		}
	}

	protected override void OnValueChanged(string toggleName, bool value)
	{
		//在这来根据名字判断 到底是那一个单选框或者多选框状态变化了 当前状态就是传入的value
	}
	
	public TMP_Text GetControl(string controlName)
	{
		return GetControl<TMP_Text>(controlName);
	}


	public void InitInfo()
	{
		Debug.Log("初始化数据");
	}
	
	public void ActiveButtons()
	{
		GetControl<Button>("btnResume").interactable = true;
		GetControl<Button>("btnMain").interactable = true;
	}
}
