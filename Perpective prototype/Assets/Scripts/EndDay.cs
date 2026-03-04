using UnityEngine;

public class EndDay : MonoBehaviour
{
    private Animator PrinterAnim;

    void Start()
    {
        PrinterAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            PrinterAnim.SetTrigger("EPress");
        }
    }
}
