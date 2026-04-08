using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;




public class TurnProgression : MonoBehaviour
{
    bool sceneEnd = true;
    public int polution = 0;
    
    public int culture; // rep
    public int rep;

    public int industry; // eng
    public int energy;

    public int trade;
    public int money { get; private set; } = 0;
    public int turnCounter = 0;
    public int bexlyMoney = 4;
    public RandomEvents randomEvents;
    public List<EventData> CurrentEvents = new List<EventData>(1);
    private EventUI UiEvents;
    bool firstrun = true;
    public GameObject MoneySlider;
    public GameObject RepSlider;
    public GameObject EnergySlider;
    public bool turnover = true;
    private SliderScript ValueSlider;

    private EventData MajorEventonHold;

    public List<EventData> EndingEvents = new List<EventData>(1);
    




    [Header("states")] // i stg theres a better way to link these but i can't find it im sorry :(
    private GameObject currentState;

    public GameObject defaultState;
    public GameObject pandemicState;
    public GameObject foreignState;
    public GameObject powerOutageState;
    public GameObject tornadoState;

    [Header("map 2")]
    private GameObject currentState2;

    public GameObject defaultState2;
    public GameObject tornadoState2;
    public GameObject invasionState2;
    public GameObject communintyCenter2;
    public GameObject commCenter2;

    void Start()
    {
        currentState = defaultState;
        currentState2 = defaultState2;
        randomEvents.ImportEvents(); // imports the basic info about events
        UiEvents = this.GetComponent<EventUI>();
        AddEvent(true);
        AddEvent(false);
        MoneyChange(8);
        ValueSlider = this.GetComponent<SliderScript>();
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
    public void EnergyChange(int Change)
    {
        if (energy + Change >= 0)
        {
            if (energy + Change <= 8)
            {
                energy += Change;
                
            }
            else
            {
                energy = 8;
            }
        }
        else
        {
            energy = 0;
        }
        EnergySlider.GetComponent<Slider>().value = energy;
    }
    public void RepChange(int Change)
    {
        if (rep + Change >= 0 )
        {
            if (rep + Change <= 8)
            {
                rep += Change;
                RepSlider.GetComponent<Slider>().value = rep;
            }
            else
            {
                rep = 8;
            }
        }
        else
        {
            rep = 0;
        }
        RepSlider.GetComponent<Slider>().value = rep;

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
        if (Input.GetKeyDown(KeyCode.E) && false)
        {
            
            //StartCoroutine

            if (turnover)
            {
                //Debug.Log("test2");
                turnover = false;
                StartCoroutine(WaitLike5Seconds());

                TurnEnd();

            }
        }
    }

    IEnumerator WaitLike5Seconds()
    {
        yield return new WaitForSeconds(7);
        turnover = true;
    }
    private void AddEvent(bool IsMajor)
    {
        CurrentEvents.Add(randomEvents.GetRandomEvent(IsMajor));
        UiEvents.addEvent(CurrentEvents[CurrentEvents.Count - 1].name, CurrentEvents[CurrentEvents.Count-1].turnsLeft);
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
    public void TurnEnd() // this runs when the turn ends
    {
        if (sceneEnd)
        {
            
            turnCounter += 1;
            List<int> eventRemove = new List<int>();
            int counter = 0;
            bool addMajor = false;
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
                        addMajor = true;
                        
                    }
                    trade += resourcesAffected[0];
                    culture += resourcesAffected[1];
                    industry += resourcesAffected[2];
                    bexlyMoney += extraResAffected[0];
                    polution += extraResAffected[1];
                    //CurrentEvents.Remove(currentEvent);
                    eventRemove.Add(counter);
                }
                counter++;
            }
            int counter2 = 0; // this is bad but it works
            foreach(int count in eventRemove)
            {

                Debug.Log(count- counter2);
                EndingEvents.Add(CurrentEvents[count - counter2]);
                CurrentEvents.RemoveAt(count - counter2);
                UiEvents.removeEvent(count - counter2);
                counter2++;
                
            }
            UiEvents.UpdateEventTurnCounter();
            
            money = 0;
            int MoneyThisTurn = 0;
            MoneyThisTurn = (int)trade + bexlyMoney;
            if (MoneyThisTurn > 8)
            {
                MoneyThisTurn = 8;
            }
            MoneyChange(MoneyThisTurn);
            RepChange(culture);
            EnergyChange(industry);
            bexlyMoney = 4;
            //Debug.Log("turn " + turnCounter);

            //AddEvent(true); // todo, adjust, it shouldn't add a new major event every time
            // Check If Disaster Event is needed.
            if (trade <= 0)
            {
                
            }
            else if (rep <= 0)
            {

            }
            else if (energy <= 0)
            {
                randomEvents.GetDisasterEvent(randomEvents.powerOut);
            }




            if (addMajor)
            {
                AddEvent(true);
            }
            if (CurrentEvents.Count < 3)
            {
                AddEvent(false);
            }
            
            if (turnCounter == 24)
            {
                // game end
            }
            StateCheck(CurrentEvents[0]);
            // scene.load;
            sceneEnd = true;
            ValueSlider.ResetTurn();
        }

    }


    void StateCheck(EventData majorEvent)
    {
        switch (majorEvent.name)
        {
            case "Pandemic":
                currentState.SetActive(false);
                currentState = pandemicState;
                currentState.SetActive(true);

                currentState2.SetActive(false);
                currentState2 = defaultState2;
                currentState2.SetActive(true);
                break;

            case "Foreign Invasion":
                currentState.SetActive(false);
                currentState = foreignState;
                currentState.SetActive(true);
                
                currentState2.SetActive(false);
                currentState2 = invasionState2;
                currentState2.SetActive(true);
                break;
            case "City wide Power Outage":
                currentState.SetActive(false);
                currentState = powerOutageState;
                currentState.SetActive(true);

                currentState2.SetActive(false);
                currentState2 = defaultState2;
                currentState2.SetActive(true);
                break;
            case "Reactor meltdown":
                currentState.SetActive(false);
                currentState = defaultState;
                currentState.SetActive(true);

                currentState2.SetActive(false);
                currentState2 = defaultState2;
                currentState2.SetActive(true);
                break;
            case "Tornado":
                currentState.SetActive(false);
                currentState = tornadoState;
                currentState.SetActive(true);

                currentState2.SetActive(false);
                currentState2 = tornadoState2;
                currentState2.SetActive(true);
                break;

            case "Residential neighbourhood displaced":

                currentState.SetActive(false);
                currentState = defaultState;
                currentState.SetActive(true);

                currentState2.SetActive(false);
                currentState2 = commCenter2; // add post state
                currentState2.SetActive(true);
                break;

            default:
                currentState.SetActive(false);
                currentState = defaultState;
                currentState.SetActive(true);

                currentState2.SetActive(false);
                currentState2 = defaultState2;
                currentState2.SetActive(true);
                break;

        }
    }
    
}
