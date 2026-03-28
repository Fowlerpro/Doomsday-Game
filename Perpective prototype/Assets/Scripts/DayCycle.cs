using System.Collections;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [Header("Daytime")]
    public GameObject[] dayObjects;

    [Header("Nighttime")]
    public GameObject[] nightObjects;

    [Header("Delay")]
    public float delayBeforeSwitch = 10f;

    private Coroutine switchRoutine;

    public void OnDayNightButtonClick()
    {
        if (switchRoutine != null)
        {
            StopCoroutine(switchRoutine);
        }

        switchRoutine = StartCoroutine(DayNightDelay());
    }

    IEnumerator DayNightDelay()
    {
        yield return new WaitForSeconds(delayBeforeSwitch);

        int roll = Random.Range(1, 3);


        DisableAll();

        if (roll == 1)
        {
            ActivateObjects(dayObjects);
        }
        else
        {
            ActivateObjects(nightObjects);
        }
    }

    void DisableAll()
    {
        foreach (GameObject obj in dayObjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in nightObjects)
        {
            obj.SetActive(false);
        }
    }

    void ActivateObjects(GameObject[] objectsToActivate)
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }
    }
}