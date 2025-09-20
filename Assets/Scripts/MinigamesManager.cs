using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamesManager : MonoBehaviour
{

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
