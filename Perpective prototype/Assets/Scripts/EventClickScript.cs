using UnityEngine;
using System.Collections;
public class EventClickScript : MonoBehaviour 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void buttonSwitchOff(GameObject gameobject)
    {
        gameobject.SetActive(!gameobject.activeSelf);
    }
}
