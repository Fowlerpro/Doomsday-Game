using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class ComputerManager : MonoBehaviour
{
    public GameObject Screen;
    public GameObject Cursor;
    public RawImage Loading;
    public float fadeDuration = 0.5f;
    float delay;
    public float anchorDelay = 2.0f;
    public VideoClip loadingClip;
    public VideoPlayer player;
    bool inputLocked = false;
    private void Start()
    {
        player.loopPointReached += OnVideoFinished;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {

            if (!inputLocked)
            {
                inputLocked = true;
                delay = anchorDelay;
                Screen.SetActive(false);
                Cursor.SetActive(true);
                ColorTransparency(1f);
                PlayVideo(loadingClip);
                StartCoroutine(FadeAfterDelay());
            }
            else
            {
                StopLock();
            }
        }
    }
    void StopLock()
    {
        player.Stop();
        Screen.SetActive(true);
        Cursor.SetActive(false);
        inputLocked = false;
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
    void OnVideoFinished(VideoPlayer vp)
    {
        vp.Stop();
    }
    void PlayVideo(VideoClip clip)
    {
        if (!player.isPlaying)
        {

            player.Stop();
            player.clip = clip;
            player.time = 0;
            player.Play();
        }
    }
}
