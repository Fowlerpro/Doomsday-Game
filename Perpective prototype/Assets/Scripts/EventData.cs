using UnityEngine;

public class EventData
{

    public string name; // this is the event name
    public string text { get; private set; }// this is what is shown to the player
    private int[] resources1; // 
    public bool Completed = false;
    private int[] resources2;
    public string splash1 { get; private set; }
    public string splash2 { get; private set; }
    private int[] extraRes1;
    private int[] extraRes2;
    public int cost { get; private set; }
    public int paid { get; private set; }
    public bool isMajor { get; private set; }
    public bool isSoOver; // its so over (if you lose)
    private EventData secondaryEvent = null;
    public int turnsLeft = 0;
    public int toPay = 0;

    public int deadline;
    public EventData(string name, bool isMajor)
    { // this sets up the skeleton for the events that we can search through.
        this.name = name;
        this.isMajor = isMajor;
        paid = 0;
        //Debug.Log("PLEASE WORK PLEASEE");
    }

    public string InitialSplash()
    {
        return this.text;
    }
    public string EndingSplash()
    {
        if (Completed)
        {
            return splash1;
        }
        else
        {
            return splash2;
        }
    }
    public void EventInitialize(string threat, string splash1, string splash2, int[] resources1, int[] resources2, int[] extra1, int[] extra2, int deadline, int cost, string runover, string prereq1, string prereq2)/* bool chosen,int timeCost*/
    {
        //threat, splash1, splash2, res1, res2, rext1, rext2, time, cost, runover, prereq1, prereq2
        // 1     2         3        4     5     6      7      8     9     10       11       12
        // once an event has been chosen it is initialized, which fills out all the information for the event
        this.text = threat;
        this.splash1 = splash1;
        this.splash2 = splash2;
        this.resources1 = resources1;
        this.resources2 = resources2;
        this.extraRes1 = extra1;
        this.extraRes2 = extra2;
        this.deadline = deadline;
        this.cost = cost;
        this.isSoOver = (runover == "1");
        paid = 0;
        //this.secondaryEvent = secondaryEvent; // implement later im tired.
        Completed = false;
        turnsLeft = deadline;
        //Debug.Log("SAVE MY SOUL");
        
    }
    public void PayCost()
    {
        //int change = 0;
        // 20    15 + 10       change should be 5
        // 25 - 20
        /*
        if (cost < pay + paid)
        {
            change = pay + paid - cost;
            pay -= change;
        }
        */
        //if (pay =)
        paid += toPay;
        toPay = 0;
        //return change;
    }
    public bool CheckEventDone()
    {
        if (cost <= paid)
        {
            Completed = true;
            return true;
        }
        else if (turnsLeft == 0)
        {
            Completed = false;
            return true;
        }
        return false;
    }


    public int[] EventChoice(bool baseResources) // This will output the resources effected
    {
        if (baseResources)
        {
            if (Completed)
            {
                return resources1;
            }
            else
            {
                return resources2;
            }
        }
        else
        {
            if (Completed)
            { // put this in its own function, make code much much much much neater.
                return extraRes1;
            }
            else
            {
                return extraRes2;
            }
        }
        

    }

    
}
