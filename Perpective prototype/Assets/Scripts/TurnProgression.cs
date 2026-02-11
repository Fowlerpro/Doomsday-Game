using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class TurnProgression
{
    bool sceneEnd = false;
    public int polution = 0;
    public int money = 0;
    public int resource1;
    public int resource2;
    public int resource3;
    public int turnCounter = 0;
    public RandomEvents randomEvents;
    public List<EventData> CurrentEvents;
    











    void TurnEnd(bool sceneEnd) // this runs when the turn ends
    {
        if (sceneEnd)
        {
            
            turnCounter += 1;
            foreach (EventData currentEvent in CurrentEvents)
            {
                currentEvent.turnsLeft -= 1;
                if (currentEvent.CheckEventDone())
                {
                    int[] resourcesAffected = currentEvent.EventChoice();
                    CurrentEvents.Remove(currentEvent);
                    resource1 += resourcesAffected[0];
                    resource2 += resourcesAffected[1];
                    resource3 += resourcesAffected[2];
                    money += resourcesAffected[3];
                    polution += resourcesAffected[4];
                    CurrentEvents.Remove(currentEvent);
                    
                }
            }
            CurrentEvents.Add(randomEvents.GetRandomEvent(true));
            CurrentEvents.Add(randomEvents.GetRandomEvent(false));
            if (turnCounter == 24)
            {
                // game end
            }
            // scene.load;
            sceneEnd = false;
        }

    }
    
}
