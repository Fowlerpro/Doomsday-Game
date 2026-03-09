using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SliderScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject trade;
    public GameObject industry;
    public GameObject reputation;
    public GameObject[] ObjectList = new GameObject[3];
    private TurnProgression turnProgress;
    int[] slidernums = new int[3];
    void Start()
    {
        turnProgress = this.GetComponent<TurnProgression>();
        ObjectList[0] = trade;
        ObjectList[1] = industry;
        ObjectList[2] = reputation;
        for (int i = 0; i < slidernums.Length; i++)
        {
            slidernums[i] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPayment(int EventNum)
    {
        //string EventName = ObjectList[EventNum].GetComponentInChildren<TextMeshProUGUI>().text;
        if (turnProgress.turnover)
        {
            if (slidernums[EventNum] < 4)
            {
                if (turnProgress.MoneyChange(-1))
                {

                    slidernums[EventNum]++;
                    UpdateSlider(EventNum);
                    UpdateValues(1, EventNum);
                }

            }
        }
    }

    public void SubtractPayment(int EventNum)
    {
        //string EventName = ObjectList[EventNum].GetComponentInChildren<TextMeshProUGUI>().text;
        if (turnProgress.turnover)
        {
            if (slidernums[EventNum] > 0)
            {
                if (turnProgress.MoneyChange(1))
                {
                    slidernums[EventNum]--;
                    //turnProgress.money++;
                    UpdateSlider(EventNum);
                    UpdateValues(-1, EventNum);
                }


            }
        }
    }
    public void UpdateValues(int change, int eventNum)
    {
        switch (eventNum)
        {
            case 0:
                turnProgress.trade += change;
                break;
            case 1:
                turnProgress.industry += change;
                break;
            case 2:
                turnProgress.rep += change;
                break;

        }
    }
    public void UpdateSlider(int eventNum)
    {
        ObjectList[eventNum].GetComponent<Slider>().value = slidernums[eventNum];
    }
}
