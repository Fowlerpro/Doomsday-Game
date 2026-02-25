using Unity.VisualScripting;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
 public GameObject NewCanvas;
 public GameObject OldCanvas;
 public GameObject OldCanvas2;
 public GameObject NewCanvasText;
 public GameObject OldCanvasText;
 public GameObject OldCanvasText2;
    public void OnButtonClick()
    {
        NewCanvas.SetActive(true);
        NewCanvasText.SetActive(true);
        OldCanvas.SetActive(false);
        OldCanvas2.SetActive(false);
        OldCanvasText.SetActive(false);
        OldCanvasText2.SetActive(false);
    }
}
