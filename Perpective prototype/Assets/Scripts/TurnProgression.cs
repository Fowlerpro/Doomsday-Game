using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;




public class TurnProgression : MonoBehaviour
{
    bool sceneEnd = false;
    public int polution = 0;
    
    public int culture; // rep
    public int rep;

    public int industry; // eng
    public int energy;

    public int trade;
    public int money { get; private set; } = 0;
    public int turnCounter = 0;
    public int bexlyMoney = 5;
    public RandomEvents randomEvents;
    public List<EventData> CurrentEvents = new List<EventData>(1);
    private EventUI UiEvents;
    bool firstrun = true;
    public GameObject MoneySlider;

    void Start()
    {
        randomEvents.ImportEvents(); // imports the basic info about events
        UiEvents = this.GetComponent<EventUI>();
        AddEvent(true);
        AddEvent(false);
        MoneyChange(8);
    }
    public bool MoneyChange(int Change)
    {
        if(money + Change >= 0 && money + Change <= 8)
        {
            money += Change;
            MoneySlider.GetComponent<Slider>().value = money;
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void FixedUpdate()
    {
        /*
        if(Input.GetMouseButtonDown(0) && firstrun)
        {
            AddEvent(true);
            AddEvent(false);
            firstrun =false;
        }
        */
    }

    private void AddEvent(bool IsMajor)
    {
        CurrentEvents.Add(randomEvents.GetRandomEvent(IsMajor));
        UiEvents.addEvent(CurrentEvents[CurrentEvents.Count - 1].name);
    }

    public int FindEvent(string eventName)
    {
        int EventNumber = -1;
        for (int i = 0; i < CurrentEvents.Count; i++)
        {
            if (CurrentEvents[i].name == eventName)
            {
                EventNumber = i;
            }
        }
        return EventNumber;
    }
    void TurnEnd(bool sceneEnd) // this runs when the turn ends
    {
        if (sceneEnd)
        {
            
            turnCounter += 1;
            foreach (EventData currentEvent in CurrentEvents)
            {
                currentEvent.turnsLeft -= 1;
                currentEvent.PayCost();
                if (currentEvent.CheckEventDone())
                {
                    int[] resourcesAffected = currentEvent.EventChoice(true);
                    int[] extraResAffected = currentEvent.EventChoice(false);
                    //CurrentEvents.Remove(currentEvent);
                    if (currentEvent.isMajor)
                    {
                        AddEvent(true);
                    }
                    trade += resourcesAffected[0];
                    culture += resourcesAffected[1];
                    industry += resourcesAffected[2];
                    bexlyMoney += extraResAffected[0];
                    polution += extraResAffected[1];
                    CurrentEvents.Remove(currentEvent);
                    
                }
            }
            //AddEvent(true); // todo, adjust, it shouldn't add a new major event every time
            if(CurrentEvents.Count < 3)
            {
                AddEvent(false);
            }
            
            if (turnCounter == 24)
            {
                // game end
            }
            // scene.load;
            sceneEnd = false;
        }

    }
    
}
