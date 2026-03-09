
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject majorEvent;
    public GameObject minorEvent1;
    public GameObject minorEvent2;
    public GameObject[] ObjectList = new GameObject[3];
    private TurnProgression turnProgress;
    void Start()
    {
        turnProgress = this.GetComponent<TurnProgression>();
        ObjectList[0] = majorEvent;
        ObjectList[1] = minorEvent1;
        ObjectList[2] = minorEvent2;
        majorEvent.SetActive(false);
        minorEvent1.SetActive(false);
        minorEvent2.SetActive(false);
    }
    //public event EventGrab
    // Update is called once per frame
    public void AddPayment(int EventNum)
    {
        if (turnProgress.turnover)
        {
            string EventName = ObjectList[EventNum].GetComponentInChildren<TextMeshProUGUI>().text;
            int actualNum = turnProgress.FindEvent(EventName);

            if (turnProgress.CurrentEvents[actualNum].toPay + turnProgress.CurrentEvents[actualNum].paid < turnProgress.CurrentEvents[actualNum].cost)
            {
                if (turnProgress.MoneyChange(-1))
                {
                    turnProgress.CurrentEvents[actualNum].toPay += 1;
                }

            }
            UpdateSlider(EventNum, actualNum);
        }
    }

    public void SubtractPayment(int EventNum)
    {
        if (turnProgress.turnover)
        {
            string EventName = ObjectList[EventNum].GetComponentInChildren<TextMeshProUGUI>().text;
            int actualNum = turnProgress.FindEvent(EventName);

            if (turnProgress.CurrentEvents[actualNum].toPay > 0)
            {
                if (turnProgress.MoneyChange(1))
                {
                    turnProgress.CurrentEvents[actualNum].toPay -= 1;
                }
                //turnProgress.money += 1;
            }
            UpdateSlider(EventNum, actualNum);
        }
    }


    public void UpdateSlider(int eventNum, int actualNum)
    {
        ObjectList[eventNum].GetComponentInChildren<Slider>().maxValue = turnProgress.CurrentEvents[actualNum].cost;
        ObjectList[eventNum].GetComponentInChildren<Slider>().value = turnProgress.CurrentEvents[actualNum].paid + turnProgress.CurrentEvents[actualNum].toPay;
    }
    void Update()
    {
        
    }
    public void UpdateEventTurnCounter()
    {
        foreach(GameObject eventPart in ObjectList)
        {
            if (eventPart.activeSelf)
            {
                string eventText = eventPart.GetComponentsInChildren<TextMeshProUGUI>(true)[1].text;
                int turnsLeft = int.Parse(eventText.Substring(13));
                turnsLeft--;
                //Debug.Log(turnsLeft);
                eventPart.GetComponentsInChildren<TextMeshProUGUI>(true)[1].text = "Turns Left: \n" + turnsLeft;
            }
        }
    }
    
    public void addEvent(string eventName, int turnsTotal)
    {
        //Debug.Log(eventName);
        int eventNumber = -1;
        for (int i = 0; i < ObjectList.Length; i++)
        {
            if(eventNumber == -1)
            {
                if (!ObjectList[i].activeSelf)
                {
                    ObjectList[i].SetActive(true);
                    eventNumber = i;
                }
            }
            
            
        }
        //Debug.Log(eventNumber);
        //Debug.Log(ObjectList.Length);
        //Debug.Log(eventName)

        //GameObject textmesh = ObjectList[eventNumber].GetComponentInChildren<TextMesh>(true);
        ObjectList[eventNumber].GetComponentsInChildren<TextMeshProUGUI>(true)[0].text = eventName;
        ObjectList[eventNumber].GetComponentsInChildren<TextMeshProUGUI>(true)[1].text = "Turns Left: \n"+turnsTotal;
        int actualNum = turnProgress.FindEvent(eventName);
        UpdateSlider(eventNumber, actualNum);
    }
    public void removeEvent(int eventnumber)
    {
        ObjectList[eventnumber].SetActive(false);
    }
    
}
