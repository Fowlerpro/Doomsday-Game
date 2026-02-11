using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


// random events
public class RandomEvents : MonoBehaviour
{
    
    public List<EventData> eventList = new List<EventData>();
    public List<EventData> MajorEventList = new List<EventData>();
    private EventData tempEvent;

    public void setupEvents()
    {
        eventList.Clear();
        //EventType test
        //eventList.Add();
        
    }
    
    public EventData GetRandomEvent(bool isMajorEvent)
    {
        int eventNum;
        EventData currentEvent;
        if (isMajorEvent)
        {
            eventNum = Random.Range(0, MajorEventList.Count);
            currentEvent = MajorEventList[eventNum];
        }
        else
        {
            eventNum = Random.Range(0, eventList.Count);
            currentEvent = eventList[eventNum];
        }
        
        
        using StreamReader reader = new("Assets/Scripts/excelltest.csv"); // todo mupdate path
        string line1;
        string line2;
        //EventData NewEvent;
        reader.ReadLine();
        bool eventFound = false;
        while (!reader.EndOfStream && !eventFound)
        {
            line1 = reader.ReadLine();
            line2 = reader.ReadLine();
            string[] fullLine= line1.Split(',');
            if (fullLine[2] == currentEvent.name)
            {

                string[] fullLine2 = line1.Split(',');


                int[] Res1 = { int.Parse(fullLine[6]), int.Parse(fullLine[7]), int.Parse(fullLine[8]), int.Parse(fullLine[9]) };
                int[] Res2 = { int.Parse(fullLine2[6]), int.Parse(fullLine2[7]), int.Parse(fullLine2[8]), int.Parse(fullLine2[9]) };
                // choice, name, major, Threat, desc,  money, res1, res2, res3, pol, time
                // 0      1      2       3      4      5      6     7     8     9    10


                //threat, splash1, splash2, res1, res2, time
                // 1     2         3        4     5     7
                currentEvent = new EventData(fullLine[1], (fullLine[2] == "major"));
                currentEvent.EventInitialize(fullLine[3], fullLine[4], fullLine2[4], Res1, Res2, int.Parse(fullLine[10]), int.Parse(fullLine[11]));
            }
        }
        if (isMajorEvent)
        {
            MajorEventList.RemoveAt(eventNum);
        }
        else
        {
            eventList.RemoveAt(eventNum);
        }
        
        reader.Close();
        return currentEvent;

        
    }

    void Start()
    {
        ImportEvents();
    }
    void ImportEvents()
    {

        using StreamReader reader = new("Assets/Scripts/excelltest.csv"); // todo mupdate path
        //
        string line = reader.ReadLine();
        string line1;
        string line2;
        
        while (!reader.EndOfStream)
        {

            Debug.Log("test");
            line1 = reader.ReadLine();
            line2 = reader.ReadLine();
            string[] fullLine = line1.Split(',');
            string[] fullLine2 = line1.Split(',');
            // choice, name, major, Threat, desc,  money, res1, res2, res3, pol, time
            // 0      1      2       3      4      5      6     7     8     9    10


            // name, major, threat, splash1, splash2, res1, res2, cost, time
            // 1     2      3       4        5        6     7     8     9
            tempEvent = new EventData(fullLine[1], (fullLine[2] == "major"));
            if (fullLine[2] == "major")
            {
                MajorEventList.Add(tempEvent);
            }
            else
            {
                eventList.Add(tempEvent);
            }
                
           
        }
        reader.Close();

    }

}
