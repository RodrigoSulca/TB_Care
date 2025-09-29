using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using NUnit.Framework;

public class HomeManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject dailyQPanel;
    public Slider questSlider;
    public Button questButton;
    public TMP_Text pointsTxt;
    [Header("Config")]
    public PlayerStats playerStats;
    private static bool loadStats;

    void Start()
    {
        PlayerPrefs.SetInt("coins", playerStats.totalCoins);
        PlayerPrefs.Save();

        questSlider.value = playerStats.dailyMG;
        if (questSlider.value >= questSlider.maxValue)
        {
            questButton.interactable = true;
        }
    }

    void Update()
    {
        pointsTxt.text = playerStats.totalCoins.ToString();
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
        playerStats.totalCoins += cantPoints;
        questButton.interactable = false;
    }


    private void LoadStats()
    {
        string path = Application.persistentDataPath + "/stats.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            JsonUtility.FromJsonOverwrite(json, playerStats);
        }
        else
        {
            print("No hay datos guardados");
        }
    }

    void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(playerStats);
        File.WriteAllText(Application.persistentDataPath + "/stats.json", json);
        Debug.Log(Application.persistentDataPath + "/stats.json");
    }
}
