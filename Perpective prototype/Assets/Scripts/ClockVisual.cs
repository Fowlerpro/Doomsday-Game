using UnityEngine;

public class ClockVisual : MonoBehaviour
{
    public float rotationDuration = 4f;

    void Update()
    {
        if (rotationDuration <= 0f) return;

        float degreesPerSecond = -360f / rotationDuration;
        transform.Rotate(0f, degreesPerSecond * Time.deltaTime, 0f);
    }
}
