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
    public GameObject LoadingCanvas;
    public GameObject ButtonCanvas;
    public float fadeDuration = 0.5f;
    float delay;
    float buttonTimer = 3f;
    public float anchorDelay = 2.0f;
    public VideoClip loadingClip;
    public VideoPlayer player;
    bool inputLocked = false;
    private void Start()
    {
        player.loopPointReached += OnVideoFinished;
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            StopLock();
        }
    }
    public void ComputerTurnOn ()
    {
        if (!inputLocked)
        {
            LoadingCanvas.SetActive(true);
            inputLocked = true;
            delay = anchorDelay;
            Screen.SetActive(false);
            Cursor.SetActive(true);
            ButtonCanvas.SetActive(true);
            ColorTransparency(1f);
            PlayVideo(loadingClip);
            StartCoroutine(FadeAfterDelay());
            StartCoroutine(SpawnButton());
        }
    }

     void StopLock()
    {
        player.Stop();
        Screen.SetActive(true);
        Cursor.SetActive(false);
        ButtonCanvas.SetActive(false);
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
    IEnumerator SpawnButton()
    {
        yield return new WaitForSeconds(buttonTimer);
        LoadingCanvas.SetActive(false);

            
        
        
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
