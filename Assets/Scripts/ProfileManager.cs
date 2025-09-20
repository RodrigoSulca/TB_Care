using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProfileManager : MonoBehaviour
{
    public PlayerStats playerStats;
    
    [Header("UI")]
    public TMP_Text pointsTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointsTxt.text = $"Puntos: {playerStats.totalPoints.ToString()}";
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("Home");
    }
}
