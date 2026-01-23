using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueButton : MonoBehaviour
{
    bool sceneQueued = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !sceneQueued)
        {
            sceneQueued = true;
            Invoke(nameof(LoadScene), 2.5f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadScene("GameScene");
    }
}
