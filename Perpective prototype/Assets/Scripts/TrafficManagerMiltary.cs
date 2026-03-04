using UnityEngine;
using System.Collections.Generic;

public class TrafficManagerMilitary : MonoBehaviour
{
    [System.Serializable]
    public class TankData
    {
        public GameObject tank;
        public Transform startPoint;
        public Transform endPoint;

        [Header("Movement")]
        public float speed = 5f;

        [Header("Stop")]
        public Transform stopPoint;
        public float stopDuration = 2f;

        [HideInInspector] public float progress;
        [HideInInspector] public bool isStopped;
        [HideInInspector] public float stopTimer;
        [HideInInspector] public bool hasStoppedThisLoop;
        [HideInInspector] public float stopProgress;
    }

    public List<TankData> tanks = new List<TankData>();

    void Start()
    {
        foreach (TankData tankData in tanks)
        {
            if (tankData.tank == null || tankData.startPoint == null || tankData.endPoint == null)
                continue;

            tankData.tank.transform.position = tankData.startPoint.position;
            tankData.progress = 0f;
            tankData.isStopped = false;
            tankData.hasStoppedThisLoop = false;

            if (tankData.stopPoint != null)
            {
                float totalDistance = Vector3.Distance(
                    tankData.startPoint.position,
                    tankData.endPoint.position
                );

                float stopDistance = Vector3.Distance(
                    tankData.startPoint.position,
                    tankData.stopPoint.position
                );

                tankData.stopProgress = stopDistance / totalDistance;
            }
        }
    }

    void Update()
    {
        foreach (TankData tankData in tanks)
        {
            if (tankData.tank == null || tankData.startPoint == null || tankData.endPoint == null)
                continue;

            if (tankData.isStopped)
            {
                tankData.stopTimer += Time.deltaTime;

                if (tankData.stopTimer >= tankData.stopDuration)
                {
                    tankData.isStopped = false;
                    tankData.stopTimer = 0f;
                }

                continue;
            }

            float totalDistance = Vector3.Distance(
                tankData.startPoint.position,
                tankData.endPoint.position
            );

            tankData.progress += (tankData.speed / totalDistance) * Time.deltaTime;

            if (tankData.stopPoint != null &&
                !tankData.hasStoppedThisLoop &&
                tankData.progress >= tankData.stopProgress)
            {
                tankData.isStopped = true;
                tankData.hasStoppedThisLoop = true;
                continue;
            }

            tankData.tank.transform.position = Vector3.Lerp(
                tankData.startPoint.position,
                tankData.endPoint.position,
                tankData.progress
            );

            if (tankData.progress >= 1f)
            {
                tankData.progress = 0f;
                tankData.hasStoppedThisLoop = false;
                tankData.tank.transform.position = tankData.startPoint.position;
            }
        }
    }
}
