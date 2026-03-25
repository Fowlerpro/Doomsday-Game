using TMPro;
using UnityEngine;

public class EndDayAnim : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private Animator PrinterAnim;
    private int month = 1;

    void Start()
    {
        PrinterAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        Text.text = "Month " + month.ToString();
    }
    public void OnButtonClick()
    {
        PrinterAnim.SetTrigger("EPress");
        month ++;
    }

}
