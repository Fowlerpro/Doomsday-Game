using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

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
        //This is all I left in update btw, rightclick input and that it resets the view
        //All the stuff u need to see is below gang
        if (Input.GetMouseButtonDown(1))
        {
            ResetView();
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
    //Alr Jayce ik u seeing this pookie, bellow is the office movement stuff
    //I basically copy and pasted what I had in update and put them here
    //I left out the actual get button press input and added a public void with a name

    //Then all I did was drag the OfficeMovmentManager object into each button onclick slot
    //which I then added a function with the same name as bellow like "ToggleWindowView", etc.

    //If u got any questions just ask me n I can show u myself
    public void ToggleWindowView()
    {
        if (!pcViewActive && !phoneViewActive)
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
    }
    public void TogglePCView()
    {
        if (!windowViewActive && !phoneViewActive)
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
    }
    public void TogglePhoneView()
    {
        if (!windowViewActive && !pcViewActive)
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

    public void ResetView()
    {
        //I have this bool set up cause the window blur was appearing
        //whenever I clicked right mouse button from wtv view
        bool wasWindowView = windowViewActive;

        windowViewActive = false;
        pcViewActive = false;
        phoneViewActive = false;
        toggled = false;

        //This turns off the phone and monitor views
        if (pcToggle != null)
            pcToggle.SetActive(false);

        if (phoneToggle != null)
            phoneToggle.SetActive(false);

        //This resets all those objects in the manager, like
        //your city skyline and power plant smoke and even the window view
        foreach (GameObject obj in disableOnToggle)
        {
            if (obj != null)
                obj.SetActive(true);
        }

        foreach (GameObject obj in enableOnToggle)
        {
            if (obj != null)
                obj.SetActive(false);
        }


        //This is for that bool I mentioned, the second blur only
        //appears when leaving the window view

        //The initial blur that appears when going to the window view is in
        //the actual public logic up above btw
        if (wasWindowView && !blurRunning)
        {
            StartCoroutine(FadeOutRoutine());
        }
    }
}
