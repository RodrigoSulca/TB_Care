using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigamesManager : MonoBehaviour
{
    public PlayerStats playerStats;
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Time.timeScale = 1;
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
