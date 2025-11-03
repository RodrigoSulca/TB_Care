using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Unity.Notifications.Android;
public class NotificationsManager : MonoBehaviour
{
    public GameObject notificationPref;
    public Transform spawner;
    public Recipe recipe;
    public bool notificationActive;
    public bool setnotifications;
    public List<Medicine> pendingMed;

    void Start()
    {
        SetDailyMedicines();
        LoadRecipe();
        pendingMed = new List<Medicine>(recipe.dayMedicines);
    }

    void Update()
    {
        TimeSpan actualTime = DateTime.Now.TimeOfDay;
        actualTime = new TimeSpan(actualTime.Hours, actualTime.Minutes, 0);

        foreach (Medicine medicine in pendingMed)
        {
            if (TimeSpan.TryParse(medicine.hoursXDay[0], out TimeSpan medicineTime))
            {
                if (medicineTime == actualTime && !notificationActive)
                {
                    SpawnNotification(medicine);
                    pendingMed.Remove(medicine);
                    notificationActive = true;
                    break;
                }
                else if(medicineTime > actualTime && !setnotifications)
                {
                    Notifications.Instance.CreateNotification(medicineTime, medicine);
                }
            }
        }
        setnotifications = true;
    }

    private void SetDailyMedicines()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        string lastDate = PlayerPrefs.GetString("LastMessageDate", "");

        if (lastDate != today)
        {
            recipe.dayMedicines = new List<Medicine>(recipe.medicines);

            PlayerPrefs.SetString("LastMessageDate", today);
            PlayerPrefs.Save();
            SaveRecipe();
        }
    }

    private void SpawnNotification(Medicine actualMed)
    {
        NotificationController notificationController = Instantiate(notificationPref, spawner).GetComponent<NotificationController>();
        notificationController.notificationsManager = GetComponent<NotificationsManager>();
        notificationController.medicine = actualMed;
    }

    private void LoadRecipe()
    {
        string path = Application.persistentDataPath + "/recipe.json";
        string json = File.ReadAllText(path);
        JsonUtility.FromJsonOverwrite(json, recipe);
    }

    private void SaveRecipe()
    {
        string json = JsonUtility.ToJson(recipe);
        File.WriteAllText(Application.persistentDataPath + "/recipe.json", json);
        Debug.Log(Application.persistentDataPath + "/recipe.json");
    }
}
