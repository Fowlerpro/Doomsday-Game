using UnityEngine;
using System.Collections.Generic;

public class TrafficManagerAir : MonoBehaviour
{
    [System.Serializable]
    public class PlaneData
    {
        public GameObject plane;
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

    public List<PlaneData> planes = new List<PlaneData>();

    void Start()
    {
        foreach (PlaneData planeData in planes)
        {
            if (planeData.plane == null || planeData.startPoint == null || planeData.endPoint == null)
                continue;

            planeData.plane.transform.position = planeData.startPoint.position;
            planeData.progress = 0f;
            planeData.isStopped = false;
            planeData.hasStoppedThisLoop = false;

            if (planeData.stopPoint != null)
            {
                float totalDistance = Vector3.Distance(
                    planeData.startPoint.position,
                    planeData.endPoint.position
                );

                float stopDistance = Vector3.Distance(
                    planeData.startPoint.position,
                    planeData.stopPoint.position
                );

                planeData.stopProgress = stopDistance / totalDistance;
            }
        }
    }

    void Update()
    {
        foreach (PlaneData planeData in planes)
        {
            if (planeData.plane == null || planeData.startPoint == null || planeData.endPoint == null)
                continue;

            if (planeData.isStopped)
            {
                planeData.stopTimer += Time.deltaTime;

                if (planeData.stopTimer >= planeData.stopDuration)
                {
                    planeData.isStopped = false;
                    planeData.stopTimer = 0f;
                }

                continue;
            }

            float totalDistance = Vector3.Distance(
                planeData.startPoint.position,
                planeData.endPoint.position
            );

            planeData.progress += (planeData.speed / totalDistance) * Time.deltaTime;

            if (planeData.stopPoint != null &&
                !planeData.hasStoppedThisLoop &&
                planeData.progress >= planeData.stopProgress)
            {
                planeData.isStopped = true;
                planeData.hasStoppedThisLoop = true;
                continue;
            }

            planeData.plane.transform.position = Vector3.Lerp(
                planeData.startPoint.position,
                planeData.endPoint.position,
                planeData.progress
            );

            if (planeData.progress >= 1f)
            {
                planeData.progress = 0f;
                planeData.hasStoppedThisLoop = false;
                planeData.plane.transform.position = planeData.startPoint.position;
            }
        }
    }
}
