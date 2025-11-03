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
    public TMP_Text pointsTxt;
    [Header("Config")]
    public PlayerStats playerStats;
    public Quest[] quests;
    public GameObject questPrefab;
    public Transform questsBox;
    public int cantQuests;
    private static bool loadStats;

    void Start()
    {
        PlayerPrefs.SetInt("coins", playerStats.totalCoins);
        PlayerPrefs.Save();
        SetDailyQuests();
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

    private void SetDailyQuests()
    {
        for(int i=0; i < cantQuests; i++)
        {
            QuestItem questItem = Instantiate(questPrefab, questsBox).GetComponent<QuestItem>();
            questItem.quest = quests[Random.Range(0, quests.Length)];
        }
    }

    void OnApplicationQuit()
    {
        string json = JsonUtility.ToJson(playerStats);
        File.WriteAllText(Application.persistentDataPath + "/stats.json", json);
        Debug.Log(Application.persistentDataPath + "/stats.json");
    }
}
