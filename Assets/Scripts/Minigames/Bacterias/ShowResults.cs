using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowResults : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text[] resultTxts;
    public TMP_Text extraPointsTxt;
    public GameObject winPoints;
    [Header("Config")]
    public BacteriumGController bacteriumGController;
    public PlayerStats playerStats;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        SetupResults();
    }

    public void ExitGame(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
    
    private void SetupResults()
    {
        int totalPoints = 0;
        extraPointsTxt.text = $"Puntos extra: {bacteriumGController.points}";
        totalPoints += bacteriumGController.points;
        if (bacteriumGController.winGame)
        {
            resultTxts[0].text = "Felicidades!";
            resultTxts[1].text = "Lograste eliminar a las bacterias";
            totalPoints += 100;
        }
        else
        {
            winPoints.SetActive(false);
            resultTxts[0].text = "Buen intento!";
            resultTxts[1].text = "Deberas ser mas rapido la proxima vez";
        }
        playerStats.totalCoins += totalPoints;
    }
}
