using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task_Chris : MonoBehaviour
{
    void Start()
    {
        EventCenter.Instance.AddEventListener("PlayerADie", TP1Die);
        EventCenter.Instance.AddEventListener("PlayerBDie", TP2Die);
    }

    private void TP1Die(object obj)
    {
        Debug.Log("掉qg： " + (obj as float[])[0]);
        Debug.Log("Monster Die Task");

        UIManager.Instance.ShowPanel<PlayerADiePanel>("PlayerADiePanel");
        UIManager.Instance.ShowPanel<PlayerBDiePanel>("PlayerBDiePanel");
    }

    private void TP2Die(object obj)
    {
        Debug.Log("掉qg： " + (obj as float[])[0]);
        Debug.Log("Monster Die Task");
    }

    private void OnDestroy()
    {
        EventCenter.Instance.RemoveEventListener("PlayerADie", TP1Die);
        EventCenter.Instance.RemoveEventListener("PlayerBDie", TP2Die);
    }
}
