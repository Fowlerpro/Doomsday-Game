using UnityEngine;
using UnityEngine.Video;

public class EasterEgg : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject screenObject;

    public string secretCode1 = "";
    public string secretCode2 = "";
    public string secretCode3 = "";
    public string secretCode4 = "";
    public string secretCode5 = "";

    public VideoClip videoClip1;
    public VideoClip videoClip2;
    public VideoClip videoClip3;
    public VideoClip videoClip4;
    public VideoClip videoClip5;

    private string currentInput = "";

    void Start()
    {
        screenObject.SetActive(false);
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                AddDigit(c.ToString());
            }
        }
    }

    public void AddDigit(string digit)
    {
        currentInput += digit;

        int maxLength = GetMaxCodeLength();
        if (currentInput.Length > maxLength)
        {
            currentInput = currentInput.Substring(currentInput.Length - maxLength);
        }

        if (!string.IsNullOrEmpty(secretCode1) && currentInput.EndsWith(secretCode1))
        {
            PlayVideo(videoClip1);
            currentInput = "";
        }
        else if (!string.IsNullOrEmpty(secretCode2) && currentInput.EndsWith(secretCode2))
        {
            PlayVideo(videoClip2);
            currentInput = "";
        }
        else if (!string.IsNullOrEmpty(secretCode3) && currentInput.EndsWith(secretCode3))
        {
            PlayVideo(videoClip3);
            currentInput = "";
        }
        else if (!string.IsNullOrEmpty(secretCode4) && currentInput.EndsWith(secretCode4))
        {
            PlayVideo(videoClip4);
            currentInput = "";
        }
        else if (!string.IsNullOrEmpty(secretCode5) && currentInput.EndsWith(secretCode5))
        {
            PlayVideo(videoClip5);
            currentInput = "";
        }
    }

    int GetMaxCodeLength()
    {
        int maxLength = 0;

        if (!string.IsNullOrEmpty(secretCode1) && secretCode1.Length > maxLength)
            maxLength = secretCode1.Length;

        if (!string.IsNullOrEmpty(secretCode2) && secretCode2.Length > maxLength)
            maxLength = secretCode2.Length;

        if (!string.IsNullOrEmpty(secretCode3) && secretCode3.Length > maxLength)
            maxLength = secretCode3.Length;

        if (!string.IsNullOrEmpty(secretCode4) && secretCode4.Length > maxLength)
            maxLength = secretCode4.Length;

        if (!string.IsNullOrEmpty(secretCode5) && secretCode5.Length > maxLength)
            maxLength = secretCode5.Length;

        return maxLength;
    }

    void PlayVideo(VideoClip clip)
    {
        if (!videoPlayer.isPlaying && clip != null)
        {
            screenObject.SetActive(true);

            videoPlayer.Stop();
            videoPlayer.clip = clip;
            videoPlayer.time = 0;
            videoPlayer.Play();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        vp.Stop();
        screenObject.SetActive(false);
    }
}