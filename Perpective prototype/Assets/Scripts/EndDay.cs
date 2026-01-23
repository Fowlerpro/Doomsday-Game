using UnityEngine;

public class EndDay : MonoBehaviour
{
    private Animator StampAnim;

    void Start()
    {
        StampAnim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StampAnim.SetTrigger("EPress");
        }
    }
}
