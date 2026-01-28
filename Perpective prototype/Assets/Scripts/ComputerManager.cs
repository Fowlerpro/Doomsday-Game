using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ComputerManager : MonoBehaviour
{
    public GameObject Screen;
    public RawImage Loading;
    public float fadeDuration = 0.5f;
    public float delay = 2.0f;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {


            Screen.SetActive(!Screen.activeSelf);
            StartCoroutine(FadeAfterDelay());
        }
    }
    IEnumerator FadeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(FadeOutRoutine());
    }
    IEnumerator FadeOutRoutine()
    {

        ColorTransparency(1f);

        yield return StartCoroutine(FadeScreen(1f, 0f, fadeDuration));

    }
    IEnumerator FadeScreen(float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, time / duration);
            ColorTransparency(alpha);
            yield return null;
        }
        ColorTransparency(to);
    }

    public void ColorTransparency(float Alpha)
    {
        Color lfade = Loading.color;
        lfade.a = Alpha;
        Loading.color = lfade;
    }
}
