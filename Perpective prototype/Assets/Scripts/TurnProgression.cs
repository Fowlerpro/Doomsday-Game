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

    public List<EventType> CurrentEvents;
    











    void sceneEnding(bool sceneEnd) // this runs when the turn ends
    {
        if (sceneEnd)
        {
            turnCounter += 1;
            foreach (EventType currentEvent in CurrentEvents)
            {
                currentEvent.timeCost -= 1;
                if (currentEvent.timeCost <= 0)
                {
                    resourcePkg resourcesAffected = currentEvent.EventChoice(currentEvent.chosen);
                    CurrentEvents.Remove(currentEvent);
                    int[] FinalPackage = resourcesAffected.ResourceGet();
                    resource1 += FinalPackage[0];
                    resource2 += FinalPackage[1];
                    resource3 += FinalPackage[2];
                    polution += FinalPackage[3];

                    
                }
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
