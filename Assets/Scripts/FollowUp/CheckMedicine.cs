using TMPro;
using UnityEngine;

public class CheckMedicine : MonoBehaviour
{
    public Medicine medicine;
    [Header("UI")]
    public GameObject questionPanel;
    public TMP_Text questionTxt;
    public GameObject messagePanel;
    public TMP_Text messageTxt;

    [Header("Config")]
    public int coins;
    public string[] encourageMsgs;
    public string[] motivationalMsgs;
    public PlayerStats playerStats;
    [HideInInspector] public CalendarController calendarController;
    void Start()
    {
        questionTxt.text = $"Tomaste {medicine.name} a las {medicine.hoursXDay[0]}?";
    }
    public void TakeMedicine(bool take)
    {
        questionPanel.SetActive(false);
        messagePanel.SetActive(true);
        if (take)
        {
            RandomMsg(encourageMsgs);
            playerStats.totalPoints += coins;
        }
        else
        {
            RandomMsg(motivationalMsgs);
        }
    }

    public void Close()
    {
        Destroy(gameObject);
        calendarController.boxActive = false;
    }

    private void RandomMsg(string[] messages)
    {
        int num = Random.Range(0, messages.Length);
        messageTxt.text = messages[num];
    }
}
