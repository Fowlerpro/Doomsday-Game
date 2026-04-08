using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScreenTransition : MonoBehaviour
{
    public RawImage transitionScreen;
    public GameObject transitionCanvas;
    public float fadeDuration = 20f;
    public void Transition()
    {
        StartCoroutine(CanvasTimer());
        StartCoroutine(FadeOutRoutine());
    }
    IEnumerator FadeOutRoutine()
    {

        ColorTransparency(0f);

        yield return StartCoroutine(FadeScreen(0f, 1f, fadeDuration));

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
        Color lfade = transitionScreen.color;
        lfade.a = Alpha;
        transitionScreen.color = lfade;
    }
    private IEnumerator CanvasTimer()
    {

        transitionCanvas.SetActive(true);

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("TitleScreen");
        
    }
}
