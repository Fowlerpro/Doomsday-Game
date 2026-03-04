using UnityEngine;
using UnityEngine.Video;

public class EasterEgg : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject screenObject;

    public string secretCode1 = "676921";
    public string secretCode2 = "91021";

    public VideoClip videoClip1;
    public VideoClip videoClip2;

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
                currentInput += c;

                int maxLength = Mathf.Max(secretCode1.Length, secretCode2.Length);
                if (currentInput.Length > maxLength)
                {
                    currentInput = currentInput.Substring(
                        currentInput.Length - maxLength
                    );
                }

                if (currentInput.EndsWith(secretCode1))
                {
                    PlayVideo(videoClip1);
                    currentInput = "";
                }
                else if (currentInput.EndsWith(secretCode2))
                {
                    PlayVideo(videoClip2);
                    currentInput = "";
                }
            }
        }
    }

    void PlayVideo(VideoClip clip)
    {
        if (!videoPlayer.isPlaying)
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
