using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    [Header("Weather Objects")]
    public GameObject[] thunderstormObjects;
    public GameObject[] rainObjects;

    private int currentWeatherNumber;

    public void MotherNatureClick()
    {
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
}