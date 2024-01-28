using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 1. Dictionary
/// 2. Delegate
/// 3. Observer design pattern
/// </summary>
public class EventCenter : BaseManager<EventCenter>
{
	//key - events' names
	//value - delegates which listen to the event
	private Dictionary<string, UnityAction<object>> eventDict = new Dictionary<string, UnityAction<object>>();
	
	/// <summary>
	/// Add Listener (Remember to remove)
	/// </summary>
	/// <param name="name">event's name</param>
	/// <param name="action">delegates which listen to the event</param>
	public void AddEventListener(string name, UnityAction<object> action)
	{
		if(eventDict.ContainsKey(name))
		{
			eventDict[name] += action;
		}
		else
		{
			eventDict.Add(name, action);
		}
	}
	
	/// <summary>
	/// Remove Listener
	/// </summary>
	/// <param name="name">event's name</param>
	/// <param name="action">delegates which listen to the event</param>
	public void RemoveEventListener(string name, UnityAction<object> action)
	{
		if(eventDict.ContainsKey(name))
			eventDict[name] -= action;
	}
	
	/// <summary>
	/// Trigger event
	/// </summary>
	/// <param name="name">Event's name</param>
	public void EventTrigger(string name, object info)
	{
		if(eventDict.ContainsKey(name))
		{
			eventDict[name].Invoke(info);
		}
	}
	
	/// <summary>
	/// Clear event center
	/// Used when change scenes
	/// </summary>
	public void Clear()
	{
		eventDict.Clear();
	}
}
