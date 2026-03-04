using UnityEngine;
using System.Collections;

public class OfficeMovement : MonoBehaviour
{
    public GameObject[] disableOnToggle;
    public GameObject[] enableOnToggle;

    public GameObject blurObject;
    public float fadeDuration = 2f;

    public GameObject pcToggle;
    public GameObject phoneToggle;

    private bool toggled;
    private bool blurRunning;

    private bool windowViewActive;
    private bool pcViewActive;
    private bool phoneViewActive;

    private Material blurMat;

    void Start()
    {
        if (blurObject != null)
        {
            blurObject.SetActive(false);

            Renderer rend = blurObject.GetComponent<Renderer>();
            if (rend != null)
            {
                blurMat = rend.material;
                SetAlpha01(0f);
            }
        }

        if (phoneToggle != null)
            phoneToggle.SetActive(false);

        if (pcToggle != null)
            pcToggle.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !pcViewActive && !phoneViewActive)
        {
            toggled = !toggled;
            windowViewActive = toggled;

            foreach (GameObject obj in disableOnToggle)
            {
                if (obj != null)
                    obj.SetActive(!toggled);
            }

            foreach (GameObject obj in enableOnToggle)
            {
                if (obj != null)
                    obj.SetActive(toggled);
            }

            if (!blurRunning)
                StartCoroutine(FadeOutRoutine());
        }

        if (Input.GetKeyDown(KeyCode.W) && !windowViewActive && !phoneViewActive)
        {
            pcViewActive = !pcViewActive;

            if (pcToggle != null)
                pcToggle.SetActive(pcViewActive);

            windowViewActive = false;
            toggled = false;

            if (phoneToggle != null)
                phoneToggle.SetActive(false);

            phoneViewActive = false;
        }


        if (Input.GetKeyDown(KeyCode.Q) && !windowViewActive && !pcViewActive)
        {
            phoneViewActive = !phoneViewActive;

            if (phoneToggle != null)
                phoneToggle.SetActive(phoneViewActive);

            windowViewActive = false;
            toggled = false;

            if (pcToggle != null)
                pcToggle.SetActive(false);

            pcViewActive = false;
        }
    }


    IEnumerator FadeOutRoutine()
    {
        blurRunning = true;

        blurObject.SetActive(true);
        SetAlpha01(1f);

        yield return StartCoroutine(FadeAlpha01(1f, 0f, fadeDuration));

        blurObject.SetActive(false);
        blurRunning = false;
    }

    IEnumerator FadeAlpha01(float from, float to, float duration)
    {
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, time / duration);
            SetAlpha01(alpha);
            yield return null;
        }

        SetAlpha01(to);
    }

    void SetAlpha01(float alpha01)
    {
        if (blurMat == null) return;

        Color c = blurMat.color;
        c.a = alpha01;
        blurMat.color = c;
    }
}
