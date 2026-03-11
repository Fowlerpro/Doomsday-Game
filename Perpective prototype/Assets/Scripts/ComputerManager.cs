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
    float buttonTimer = 2f;
    public float anchorDelay = 2.0f;
    public VideoClip loadingClip;
    public VideoPlayer player;
    bool inputLocked = false;
    bool spaceBarPressed = false;
    bool phonePressed = false;
    private void Start()
    {
        player.loopPointReached += OnVideoFinished;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!inputLocked)
            {
                inputLocked = true;
                spaceBarPressed = true;
            }
            else
            { 
                StopLock();
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!inputLocked)
            {
                inputLocked = true;
                phonePressed = true;
            }
            else
            {
                StopLock();
            }
        }
        if (Input.GetKeyDown(KeyCode.W))
        {

            if (!inputLocked && !spaceBarPressed)
            {
                LoadingCanvas.SetActive(true);
                inputLocked = true;
                delay = anchorDelay;
                Screen.SetActive(false);
                Cursor.SetActive(true);
                ColorTransparency(1f);
                PlayVideo(loadingClip);
                StartCoroutine(FadeAfterDelay());
                StartCoroutine(SpawnButton());
                
            }
            else if (!spaceBarPressed && !phonePressed)
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
        ButtonCanvas.SetActive(false);
        inputLocked = false;
        spaceBarPressed = false;
        phonePressed = false;

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
        ButtonCanvas.SetActive(true);
            
        
        
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
