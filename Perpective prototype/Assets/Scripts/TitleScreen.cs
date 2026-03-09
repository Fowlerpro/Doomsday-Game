using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    public GameObject InfoScreen;

    public void GoToNextScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ToggleOverlayOn()
    {
        if (InfoScreen != null)
        {
            InfoScreen.SetActive(!InfoScreen.activeSelf);
        }
    }
    public void ToggleOverlayOff()
    {
        if (InfoScreen != null)
        {
            InfoScreen.SetActive(!InfoScreen.activeSelf);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }
}