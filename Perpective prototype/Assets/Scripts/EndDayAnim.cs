using UnityEngine;

public class EndDayAnim : MonoBehaviour
{
    private Animator PrinterAnim;

    void Start()
    {
        PrinterAnim = GetComponent<Animator>();
    }
    public void OnButtonClick()
    {
        PrinterAnim.SetTrigger("EPress");
    }
}
