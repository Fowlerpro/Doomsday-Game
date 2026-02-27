using Unity.VisualScripting;
using UnityEngine;

public class ActivateButtonEvent : MonoBehaviour
{
 public GameObject NewCanvas;
 public GameObject OldCanvas;
 public GameObject OldCanvas2;
    public void OnButtonClick()
    {
        NewCanvas.SetActive(true);
        OldCanvas.SetActive(false);
        OldCanvas2.SetActive(false);
    }
}
