using Unity.VisualScripting;
using UnityEngine;

public class ActivateButtonEvent : MonoBehaviour
{
    public GameObject NewCanvas;
    public GameObject OldCanvas;
    public GameObject OldCanvas2;
    public GameObject OldCanvas3;
    public GameObject SelectorNew;
    public GameObject SelectorOld;
    public GameObject SelectorOld2;
    public GameObject SelectorOld3;
    public void OnButtonClick()
    {
        NewCanvas.SetActive(true);
        OldCanvas.SetActive(false);
        OldCanvas2.SetActive(false);
        OldCanvas3.SetActive(false);
        SelectorNew.SetActive(true);
        SelectorOld.SetActive(false);
        SelectorOld2.SetActive(false);
        SelectorOld3.SetActive(false);
    }
}
