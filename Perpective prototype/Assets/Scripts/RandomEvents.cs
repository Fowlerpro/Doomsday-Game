using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class resourcePkg // ??? replace with an int array
{
    private int resource1;
    private int resource2;
    private int resource3;
    private int polution;

    public resourcePkg(int resource1, int resource2, int resource3, int polution)
    {
        this.resource1 = resource1;
        this.resource2 = resource2;
        this.resource3 = resource3;
        this.polution = polution;
    }
    public int[] ResourceGet()
    {
        int[] theReturned = { resource1, resource2, resource3, polution };

        return theReturned;
    }

   
}
public class EventType
{
    string name; // this is the event name
    private string text; // this is what is shown to the player
    private resourcePkg option1;
    public bool chosen = false;
    private resourcePkg option2;

    private EventType secondaryEvent = null;

    public int timeCost;
    public EventType(string name, string text, resourcePkg option1,resourcePkg option2)/* bool chosen,int timeCost*/
    {
        this.name = name;
        this.text = text;
        this.option1 = option1;
        //this.chosen = chosen;
        this.option2 = option2;
        this.secondaryEvent = secondaryEvent;
        this.timeCost = timeCost;
    }



    public resourcePkg EventChoice(bool Choice) // This will output the resources effected
    {
        if (Choice)
        {
            return option1;
        }
        else
        {
            return option2;
        }
        
    }
    
}


public class RandomEvents : MonoBehaviour
{
    public List<EventType> eventList = new List<EventType>();
    EventType test1;
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
            // string string resousepkg bool resourcepkg int\
            //for(int i = 0)
            resourcePkg Line1 = new resourcePkg(int.Parse(fullLine[4]), int.Parse(fullLine[5]), int.Parse(fullLine[6]), int.Parse(fullLine[7]));
            resourcePkg Line2 = new resourcePkg(int.Parse(fullLine2[4]), int.Parse(fullLine2[5]), int.Parse(fullLine2[6]), int.Parse(fullLine2[7]));
            // choice, name, text, money, res1, res2, res3, pol
            // 0      1      2     3      4     5     6     7
            test1 = new EventType(fullLine[1], fullLine[2], Line1, Line2);
            eventList.Add(test1);
            
            // name, text, pkg, bool, pkg, time
            //eventList.Add(new EventType(fullLine[0], fullLine[1],));
            
           
        }
        //event
        
        resourcePkg output = eventList[1].EventChoice(true);
        int[] outputt2 = output.ResourceGet();
        foreach(int i in outputt2)
        {
            Debug.Log(i);
        }
        Debug.Log(output.ResourceGet());
        
        //Debug.Log(eventList.Count);
        
        reader.Close();

    }

}
