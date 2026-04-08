using UnityEngine;
using System.Collections;

public class Projector : MonoBehaviour
{
    [Header("References")]
    public Animator quadAnimator;

    [Header("Animation Triggers")]
    public string pullDownTrigger = "PullDown";
    public string putUpTrigger = "PutUp";

    [Header("Objects To Toggle")]
    public GameObject[] objectsToActivate;

    [Header("Timing")]
    public float delayBeforeStart = 9f;
    public float delayAfterAnim = 1f;
    public float activeDuration = 5f;

    private bool isRunning = false;

    public void OnButtonClick()
    {
        if (!isRunning)
        {
            StartCoroutine(Sequence());
        }
    }

    IEnumerator Sequence()
    {
        isRunning = true;

        yield return new WaitForSeconds(delayBeforeStart);

        quadAnimator.SetTrigger(pullDownTrigger);

        yield return new WaitForSeconds(delayAfterAnim);

        SetObjects(true);

        yield return new WaitForSeconds(activeDuration);

        SetObjects(false);

        quadAnimator.SetTrigger(putUpTrigger);

        isRunning = false;
    }

    void SetObjects(bool state)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(state);
        }
    }
}