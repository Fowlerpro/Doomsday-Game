using System.Collections;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("Weather Objects")]
    public GameObject[] thunderstormObjects;
    public GameObject[] rainObjects;

    [Header("Settings")]
    public float delayBeforeWeather = 10f;

    private int currentWeatherNumber;
    private Coroutine weatherRoutine;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ForceThunderstorm();
        }
    }

    public void MotherNatureClick()
    {
        if (weatherRoutine != null)
        {
            StopCoroutine(weatherRoutine);
        }

        weatherRoutine = StartCoroutine(WeatherDelay());
    }

    IEnumerator WeatherDelay()
    {
        yield return new WaitForSeconds(delayBeforeWeather);

        currentWeatherNumber = Random.Range(1, 11);

        Debug.Log("Weather rolled: " + currentWeatherNumber);

        DisableAllWeather();

        if (currentWeatherNumber == 1)
        {
            ActivateObjects(thunderstormObjects);
        }
        else if (currentWeatherNumber >= 2 && currentWeatherNumber <= 4)
        {
            ActivateObjects(rainObjects);
        }
    }

    void DisableAllWeather()
    {
        foreach (GameObject obj in thunderstormObjects)
        {
            obj.SetActive(false);
        }

        foreach (GameObject obj in rainObjects)
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

    void ForceThunderstorm()
    {
        if (weatherRoutine != null)
        {
            StopCoroutine(weatherRoutine);
        }

        currentWeatherNumber = 1;

        DisableAllWeather();
        ActivateObjects(thunderstormObjects);

        Debug.Log("Forced Thunderstorm");
    }
}