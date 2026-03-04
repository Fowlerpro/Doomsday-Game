using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;




public class TurnProgression : MonoBehaviour
{
    bool sceneEnd = false;
    public int polution = 0;
    public int money = 0;
    public int culture; // rep
    public int industry; // eng
    public int trade;
    public int turnCounter = 0;
    public int bexlyMoney = 5;
    public RandomEvents randomEvents;
    public List<EventData> CurrentEvents = new List<EventData>(1);
    


    void Start()
    {
        randomEvents.ImportEvents(); // imports the basic info about events
    }

    private void FixedUpdate()
    {
        /*
        if (Input.GetKeyDown(KeyCode.E))
        {
            TurnEnd(true);
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            //Debug.Log("hmmm");
            AddEvent(true);
        }
        */
    }

    private void AddEvent(bool IsMajor)
    {
        CurrentEvents.Add(randomEvents.GetRandomEvent(IsMajor));
    }


    public void TurnEnd(bool sceneEnd) // this runs when the turn ends
    {
        if (sceneEnd)
        {
            
            turnCounter += 1;
            for (int i = 0; i < CurrentEvents.Count; i++)
            {
                CurrentEvents[i].turnsLeft -= 1;
                if (CurrentEvents[i].CheckEventDone())
                {
                    int[] resourcesAffected = CurrentEvents[i].EventChoice(true);
                    int[] extraResAffected = CurrentEvents[i].EventChoice(false);
                    //CurrentEvents.Remove(CurrentEvents[i]);
                    trade += resourcesAffected[0];
                    culture += resourcesAffected[1];
                    industry += resourcesAffected[2];
                    bexlyMoney += extraResAffected[0];
                    polution += extraResAffected[1];
                    CurrentEvents.Remove(CurrentEvents[i]);
                    
                }
            }
            CurrentEvents.Add(randomEvents.GetRandomEvent(true)); // todo, adjust, it shouldn't add a new major event every time
            CurrentEvents.Add(randomEvents.GetRandomEvent(false));
            if (turnCounter == 24)
            {
                // game end
            }
            // scene.load;
            sceneEnd = false;
            //Debug.Log("gameEnd");
        }

    }
    
}
