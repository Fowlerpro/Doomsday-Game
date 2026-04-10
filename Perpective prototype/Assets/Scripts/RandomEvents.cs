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
    public List<EventData> eventList = new List<EventData>(1);
    //public List<EventData> eventList = new List<EventData>();
    public List<EventData> MajorEventList = new List<EventData>(1);
    public EventData powerOut;
    public EventData repOut;
    //public EventData moneyOut;
    private string SheetPath = "Assets/Scripts/EventMasterList.txt";
    private string Delimiter = "	"; // im tired
    public void setupEvents()
    {
        eventList.Clear();
        //EventType test
        //eventList.Add();
        
    }
    

    public EventData GetDisasterEvent(EventData currentDisEvent) // 1 is power out 2 is rep 3 is broke
    {
        using StreamReader reader = new(SheetPath); // todo mupdate path
        string line1;
        string line2;
        //EventData NewEvent;
        reader.ReadLine();
        bool eventFound = false;
        while (!reader.EndOfStream && !eventFound)
        {
            line1 = reader.ReadLine();
            line2 = reader.ReadLine();
            string[] fullLine = line1.Split(Delimiter);
            if (fullLine[2] == currentDisEvent.name)
            {

                string[] fullLine2 = line1.Split(Delimiter);


                int[] Res1 = { int.Parse(fullLine[5]), int.Parse(fullLine[6]), int.Parse(fullLine[7]) };
                int[] Res2 = { int.Parse(fullLine2[5]), int.Parse(fullLine2[6]), int.Parse(fullLine2[7]) };

                int[] ResExt1 = { int.Parse(fullLine[8]), int.Parse(fullLine[9]) };
                int[] ResExt2 = { int.Parse(fullLine2[8]), int.Parse(fullLine2[9]) };
                // choice, major, name, Threat, desc,  money,    rep,   eng,   bexinc,   pol, time, cost, runover,  prereq
                // 0       1      2       3      4      5        6      7      8        9    10,     11   12        13


                //threat, splash1, splash2, res1, res2, rext1, rext2, time, cost, runover, prereq1, prereq2
                // 1     2         3        4     5     6      7      8     9     10       11       12
                //currentEvent = new EventData(fullLine[1], (fullLine[2] == "major"));
                currentDisEvent.EventInitialize(fullLine[3], fullLine[4], fullLine2[4], Res1, Res2, ResExt1, ResExt2, int.Parse(fullLine[10]), int.Parse(fullLine[11]), fullLine2[12], fullLine[13], fullLine2[13]);
            }
        }
        return currentDisEvent;

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
            //eventNum = Random.Range(0, eventList.Count) -1;
            eventNum = Random.Range(0, eventList.Count);
            currentEvent = eventList[eventNum];
        }
        
        
        using StreamReader reader = new(SheetPath); // todo mupdate path
        string line1;
        string line2;
        //EventData NewEvent;
        reader.ReadLine();
        bool eventFound = false;
        while (!reader.EndOfStream && !eventFound)
        {
            line1 = reader.ReadLine();
            line2 = reader.ReadLine();
            string[] fullLine= line1.Split(Delimiter);
            if (fullLine[2] == currentEvent.name)
            {

                string[] fullLine2 = line1.Split(Delimiter);

                //Debug.Log(line1);
                //Debug.Log(fullLine[1]);
                int[] Res1 = { int.Parse(fullLine[5]), int.Parse(fullLine[6]), int.Parse(fullLine[7])};
                int[] Res2 = { int.Parse(fullLine2[5]), int.Parse(fullLine2[6]), int.Parse(fullLine2[7])};

                int[] ResExt1 = { int.Parse(fullLine[8]),  int.Parse(fullLine[9]) };
                int[] ResExt2 = { int.Parse(fullLine2[8]), int.Parse(fullLine2[9]) };
                // choice, major, name, Threat, desc,  money,    rep,   eng,   bexinc,   pol, time, cost, runover,  prereq
                // 0       1      2       3      4      5        6      7      8        9    10,     11   12        13


                //threat, splash1, splash2, res1, res2, rext1, rext2, time, cost, runover, prereq1, prereq2
                // 1     2         3        4     5     6      7      8     9     10       11       12
                //currentEvent = new EventData(fullLine[1], (fullLine[2] == "major"));
                currentEvent.EventInitialize(fullLine[3], fullLine[4], fullLine2[4], Res1, Res2, ResExt1, ResExt2, int.Parse(fullLine[10]), int.Parse(fullLine[11]), fullLine2[12], fullLine[13], fullLine2[13]);
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
        //ImportEvents();
        
    }
    public void ImportEvents()
    {

        using StreamReader reader = new(SheetPath); // todo mupdate path make it a variable later bleh
        //
        string line = reader.ReadLine();
        string line1;
        string line2;
        while (!reader.EndOfStream)
        {
            //Debug.Log("test");
            line1 = reader.ReadLine();
            line2 = reader.ReadLine();
            string[] fullLine = line1.Split(Delimiter);
            string[] fullLine2 = line1.Split(Delimiter);
            // choice, major, name, Threat, desc,  money,    rep,   eng,   bexinc,   pol, time, cost  prereq
            // 0       1      2       3      4      5        6      7      8        9    10,     11   12


            // name, major, threat, splash1, splash2, res1, res2, cost, time
            // 1     2      3       4        5        6     7     8     9
            //tempEvent = ;
            //Debug.Log(line1);
            if (fullLine[2] == "City wide Power Outage")
            {
                powerOut = new EventData(fullLine[2], true);
            }
            else if (fullLine[2] == "Local protests")
            {
                repOut = new EventData(fullLine[2], true);
            }
            else if (fullLine[1] == "major")
            {

                MajorEventList.Add(new EventData(fullLine[2], true));
                //Debug.Log(MajorEventList[MajorEventList.Count-1].name);
            }
            else
            {
                eventList.Add(new EventData(fullLine[2], false));
            }

            //Debug.Log("event added");
        }
        reader.Close();
        //Debug.Log("events imported");
    }

}
