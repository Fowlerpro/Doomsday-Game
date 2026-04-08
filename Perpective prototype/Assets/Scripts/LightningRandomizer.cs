using System.Collections;
using UnityEngine;

public class LightningRandomizer : MonoBehaviour
{
    public GameObject[] lightningObjects;

    public float minDelay = 1f;
    public float maxDelay = 4f;

    public float flashDuration = 1f;

    private Coroutine loopRoutine;

    void OnEnable()
    {
        loopRoutine = StartCoroutine(LightningLoop());
    }

    void OnDisable()
    {
        if (loopRoutine != null)
        {
            StopCoroutine(loopRoutine);
        }

        //Everything turns off
        foreach (GameObject obj in lightningObjects)
        {
            obj.SetActive(false);
        }
    }

    IEnumerator LightningLoop()
    {
        while (true)
        {
            //Wait random time before next lightning activation
            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);

            //Pick random lightning objects
            int index = Random.Range(0, lightningObjects.Length);
            GameObject selected = lightningObjects[index];

            //Turn it on
            selected.SetActive(true);

            //Keep it on for wtv seconds
            yield return new WaitForSeconds(flashDuration);

            //Turn it off
            selected.SetActive(false);
        }
    }
}