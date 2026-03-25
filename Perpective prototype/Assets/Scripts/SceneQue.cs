using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneQue : MonoBehaviour
{
    bool sceneQueued = false;

    public void OnButtonClick()
    {
        if (!sceneQueued)
        {
            sceneQueued = true;
            Invoke(nameof(LoadScene), 10f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
