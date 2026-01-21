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

        [Header("Movement")]
        public float speed = 5f;

        [Header("Optional Stop")]
        public Transform stopPoint;
        public float stopDuration = 2f;

        [HideInInspector] public float progress;
        [HideInInspector] public bool isStopped;
        [HideInInspector] public float stopTimer;
        [HideInInspector] public bool hasStoppedThisLoop;
        [HideInInspector] public float stopProgress;
    }

    public List<CarData> cars = new List<CarData>();

    void Start()
    {
        foreach (CarData carData in cars)
        {
            if (carData.car == null || carData.startPoint == null || carData.endPoint == null)
                continue;

            carData.car.transform.position = carData.startPoint.position;
            carData.progress = 0f;
            carData.isStopped = false;
            carData.hasStoppedThisLoop = false;

            // Pre-calc stop progress (0–1)
            if (carData.stopPoint != null)
            {
                float totalDistance = Vector3.Distance(
                    carData.startPoint.position,
                    carData.endPoint.position
                );

                float stopDistance = Vector3.Distance(
                    carData.startPoint.position,
                    carData.stopPoint.position
                );

                carData.stopProgress = stopDistance / totalDistance;
            }
        }
    }

    void Update()
    {
        foreach (CarData carData in cars)
        {
            if (carData.car == null || carData.startPoint == null || carData.endPoint == null)
                continue;

            // Handle stopping
            if (carData.isStopped)
            {
                carData.stopTimer += Time.deltaTime;

                if (carData.stopTimer >= carData.stopDuration)
                {
                    carData.isStopped = false;
                    carData.stopTimer = 0f;
                }

                continue;
            }

            float totalDistance = Vector3.Distance(
                carData.startPoint.position,
                carData.endPoint.position
            );

            carData.progress += (carData.speed / totalDistance) * Time.deltaTime;

            // Trigger stop reliably
            if (carData.stopPoint != null &&
                !carData.hasStoppedThisLoop &&
                carData.progress >= carData.stopProgress)
            {
                carData.isStopped = true;
                carData.hasStoppedThisLoop = true;
                continue;
            }

            carData.car.transform.position = Vector3.Lerp(
                carData.startPoint.position,
                carData.endPoint.position,
                carData.progress
            );

            // Loop
            if (carData.progress >= 1f)
            {
                carData.progress = 0f;
                carData.hasStoppedThisLoop = false;
                carData.car.transform.position = carData.startPoint.position;
            }
        }
    }
}
