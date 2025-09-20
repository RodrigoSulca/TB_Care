using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dailyQPanel;
    public Slider questSlider;
    public Button questButton;
    public TMP_Text pointsTxt;
    [Header("Config")]
    public PlayerStats playerStats;

    void Start()
    {
        questSlider.value = playerStats.dailyMG;
        if (questSlider.value >= questSlider.maxValue)
        {
            questButton.interactable = true;
        }
    }

    void Update()
    {
        pointsTxt.text = playerStats.totalPoints.ToString();
    }
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowDailyQuests()
    {
        dailyQPanel.SetActive(!dailyQPanel.activeSelf);
    }

    public void AddPoints(int cantPoints)
    {
        playerStats.totalPoints += cantPoints;
        questButton.interactable = false;
    }
}
