using UnityEngine;
using UnityEngine.Video;

public class CutscenePlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    [Header("Cutscene Clips")]
    public VideoClip[] videoClips;

    private int currentIndex = 0;
    private bool isWaitingForInput = false;

    void Start()
    {
        if (videoClips.Length > 0)
        {
            PlayClip(currentIndex);
        }

        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void PlayClip(int index)
    {
        if (index >= videoClips.Length)
        {
            Debug.Log("All clips finished");
            return;
        }

        videoPlayer.clip = videoClips[index];
        videoPlayer.Play();
        isWaitingForInput = false;
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Clip finished");
        isWaitingForInput = true;
    }

    public void OnNextButtonPressed()
    {
        //If video is currently playing it will skip that mf
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        //Go to next clip
        currentIndex++;

        if (currentIndex < videoClips.Length)
        {
            PlayClip(currentIndex);
        }
        else
        {
            Debug.Log("Clips complete");
        }
    }
}