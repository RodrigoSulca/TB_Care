using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PetManager : MonoBehaviour
{
    public TMP_Text coinsTxt;
    public PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coinsTxt.text = playerStats.totalCoins.ToString();
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
