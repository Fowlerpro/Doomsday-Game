using UnityEngine;
using UnityEngine.Video;

public class EasterEgg : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject screenObject;
    public string secretCode = "676921";

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

                if (currentInput.Length > secretCode.Length)
                {
                    currentInput = currentInput.Substring(
                        currentInput.Length - secretCode.Length
                    );
                }

                if (currentInput == secretCode)
                {
                    PlayVideo();
                    currentInput = "";
                }
            }
        }
    }

    void PlayVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            screenObject.SetActive(true);

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
