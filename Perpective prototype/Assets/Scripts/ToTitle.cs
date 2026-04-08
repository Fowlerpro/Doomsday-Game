using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToTitle : MonoBehaviour
{
    public void OnButtonClick()
    {
        
        SceneManager.LoadScene("TitleScreen");
    }
}
