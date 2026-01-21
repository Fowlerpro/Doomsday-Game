using UnityEngine;
using System.Collections.Generic;

public class TrafficManager : MonoBehaviour
{
    [System.Serializable]
    public class CarData
    {
        public GameObject car;
        public Transform startPoint;
        public Transform endPoint;
        public float speed = 5f;

        [HideInInspector]
        public float progress;
    }

    public List<CarData> cars = new List<CarData>();

    void Start()
    {
        foreach (CarData carData in cars)
        {
            if (carData.car != null && carData.startPoint != null)
            {
                carData.car.transform.position = carData.startPoint.position;
                carData.progress = 0f;
            }
        }
    }

    void Update()
    {
        foreach (CarData carData in cars)
        {
            if (carData.car == null || carData.startPoint == null || carData.endPoint == null)
                continue;

            carData.progress += (carData.speed / Vector3.Distance(
                carData.startPoint.position,
                carData.endPoint.position)) * Time.deltaTime;

            carData.car.transform.position = Vector3.Lerp(
                carData.startPoint.position,
                carData.endPoint.position,
                carData.progress
            );

            if (carData.progress >= 1f)
            {
                carData.progress = 0f;
                carData.car.transform.position = carData.startPoint.position;
            }
        }
    }
}
