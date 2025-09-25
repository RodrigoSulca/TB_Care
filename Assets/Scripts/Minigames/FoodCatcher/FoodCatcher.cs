using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FoodCatcher : MonoBehaviour
{
    public bool startGame;

    [Header("Timer")]
    public Image timerImg;
    public float gameTime;

    [Header("Panels")]
    public GameObject tutorialPanel;
    public GameObject gamePanel;
    public GameObject resultsPanel;

    [Header("UI")]
    public TMP_Text resultTxt;
    public TMP_Text pointsTxt;
    public TMP_Text gamePointsTxt;
    public TMP_Text winPointsTxt;
    public GameObject recomendationTxt;

    [Header("Config")]
    public int winPoints;
    public FoodSpawner foodSpawner;
    public PlayerController playerController;
    public PlayerStats playerStats;

    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {
        pointsTxt.text = playerController.totalPoints.ToString();
        if (!playerController.win)
        {
            ShowResults();
            recomendationTxt.SetActive(true);
        }
    }

    public void StartGame()
    {
        tutorialPanel.SetActive(false);
        gamePanel.SetActive(true);
        foodSpawner.enabled = true;
        startGame = true;
        StartCoroutine(GameTimer());
    }

    private IEnumerator GameTimer()
    {
        float elapsed = 0f;
        float startFill = 1f;
        float endFill = 0f;

        while (elapsed < gameTime)
        {
            if (startGame)
            {
                elapsed += Time.deltaTime;
                float t = Mathf.Clamp01(elapsed / gameTime);
                timerImg.fillAmount = Mathf.Lerp(startFill, endFill, t);
                yield return null;
            }
        }

        ShowResults();
    }

    public void ShowResults()
    {
        ClearFood();
        gamePanel.SetActive(false);
        foodSpawner.StopAllCoroutines();
        foodSpawner.enabled = false;
        resultsPanel.SetActive(true);

        if (playerController.win)
        {
            resultTxt.text = "Felicidades!";
            winPointsTxt.text = $"Puntos por victoria: {winPoints.ToString()}";
            playerStats.totalCoins += winPoints;
        }
        else
        {
            resultTxt.text = "Vuelve a intentarlo";
        }
        gamePointsTxt.text = $"Puntos conseguidos: {playerController.totalPoints.ToString()}";
        playerStats.totalCoins += playerController.totalPoints;
        playerStats.dailyMG += 1;
        playerController.win = true;
        Debug.Log(playerStats.totalCoins);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Minigames");
    }

    private void ClearFood()
    {
        FoodController[] foods = FindObjectsByType<FoodController>(FindObjectsSortMode.None);
        foreach (FoodController food in foods)
        {
            Destroy(food);
        }
    }
}
