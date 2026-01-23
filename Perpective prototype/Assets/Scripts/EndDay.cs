using UnityEngine;

public class EndDay : MonoBehaviour
{
    private Animator StampAnim; // Reference to the Animator component

    void Start()
    {
        // Get the Animator component attached to this GameObject
        StampAnim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the Space key is pressed down in this frame
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Set the Animator Trigger to activate the transition
            StampAnim.SetTrigger("EPress");
        }
    }
}
