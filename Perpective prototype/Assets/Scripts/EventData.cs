using UnityEngine;

public class EventData
{

    public string name; // this is the event name
    private string text; // this is what is shown to the player
    private int[] resources1; // 
    public bool Completed = false;
    private int[] resources2;
    private string splash1;
    private string splash2;
    public int cost { get; private set; }
    public int paid { get; private set; }
    private bool isMajor;
    private EventData secondaryEvent = null;
    public int turnsLeft = 0;

    public int deadline;
    public EventData(string name, bool isMajor)
    { // this sets up the skeleton for the events that we can search through.
        this.name = name;
        this.isMajor = isMajor;
    }
    public void EventInitialize(string threat, string splash1, string splash2, int[] resources1, int[] resources2, int deadline, int cost)/* bool chosen,int timeCost*/
    {
        // once an event has been chosen it is initialized, which fills out all the information for the event
        this.text = threat;
        this.resources1 = resources1;
        this.resources2 = resources2;
        this.secondaryEvent = secondaryEvent; // todo
        this.deadline = deadline;
        turnsLeft = deadline;
        this.splash1 = splash1;
        this.splash2 = splash2;
        this.cost = cost;
    }
    public int PayCost(int pay)
    {
        int change = 0;
        // 20    15 + 10       change should be 5
        // 25 - 20
        if (cost < pay + paid)
        {
            change = pay + paid - cost;
            pay -= change;
        }
        paid += pay;

        return change;
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


    public int[] EventChoice() // This will output the resources effected
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


}
