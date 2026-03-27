using TMPro;
using UnityEngine;
using System.Collections;

public class EndDayAnim : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public TextMeshProUGUI Text2;
    private Animator PrinterAnim;
    private int month = 1;

    [Header("Canvas Control")]
    public GameObject buttonCanvas;
    public float disableTime = 15f;

    private bool isOnCooldown = false;

    void Start()
    {
        PrinterAnim = GetComponent<Animator>();
    }
    private void Update()
    {
        Text.text = "Month " + month.ToString();
        Text2.text = "Month " + month.ToString();
    }
    public void OnButtonClick()
    {
        if (isOnCooldown) return;

        PrinterAnim.SetTrigger("EPress");
        month ++;

        if (buttonCanvas != null)
        {
            StartCoroutine(DisableButtons());
        }
    }
    IEnumerator DisableButtons()
    {
        isOnCooldown = true;

        buttonCanvas.SetActive(false);

        yield return new WaitForSeconds(disableTime);

        buttonCanvas.SetActive(true);

        isOnCooldown = false;
    }

}
