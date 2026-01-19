using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
public class EventType
{
    string name; // this is the event name
    private string text; // this is what is shown to the player
    private int resource1y; // true resources affected
    private int resource2y;
    private int resource3y;

    private int resource1n; // false resources affected
    private int resource2n;
    private int resource3n;

    int timeCost;

    public Vector3Int EventChoice(bool Choice)
    {
        Vector3Int ResourcesAffected;
        if (Choice)
        {
            ResourcesAffected = new Vector3Int(resource1y,  resource2y, resource3y);
        }
        else
        {
            ResourcesAffected = new Vector3Int(resource1n, resource2n, resource3n);
        }
        return ResourcesAffected;
    }
    
}


public class RandomEvents
{
    private List<EventType> eventList;
    public void setupEvents()
    {
        eventList.Clear();
        //EventType test
        //eventList.Add();
    }
    public EventType GetRandomEvent()
    {
        int eventNum =Random.Range(0, eventList.Count);
        EventType currentEvent = eventList[eventNum];
        eventList.RemoveAt(eventNum);
        return currentEvent;
    }
}
