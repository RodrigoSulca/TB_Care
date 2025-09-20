using System;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public GameObject notificationPref;
    public Transform spawner;
    public Recipe recipe;
    public bool notificationActive;
    public List<Medicine> pendingMed;

    void Start()
    {
        pendingMed = new List<Medicine>(recipe.medicines);
    }

    void Update()
    {
        TimeSpan actualTime = DateTime.Now.TimeOfDay;
        actualTime = new TimeSpan(actualTime.Hours, actualTime.Minutes, 0);

        foreach (Medicine medicine in pendingMed)
        {
            if (TimeSpan.TryParse(medicine.hoursXDay[0], out TimeSpan medicineTime))
            {
                if (medicineTime <= actualTime && !notificationActive)
                {
                    SpawnNotification(medicine);
                    pendingMed.Remove(medicine);
                    notificationActive = true;
                    break;
                }
            }
        }
    }

    private void SpawnNotification(Medicine actualMed)
    {
        NotificationController notificationController = Instantiate(notificationPref, spawner).GetComponent<NotificationController>();
        notificationController.notificationsManager = GetComponent<NotificationsManager>();
        notificationController.medicine = actualMed;
    }
}
