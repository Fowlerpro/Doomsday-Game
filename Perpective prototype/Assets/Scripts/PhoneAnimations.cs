using UnityEngine;

public class PhoneAnimations : MonoBehaviour
{
    public void PressButton(Animator animator)
    {
        animator.SetTrigger("Press");
    }
}