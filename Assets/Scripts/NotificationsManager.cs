using System;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
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
        SetNotifications();
    }

    private void SpawnNotification(Medicine actualMed)
    {
        NotificationController notificationController = Instantiate(notificationPref, spawner).GetComponent<NotificationController>();
        notificationController.notificationsManager = GetComponent<NotificationsManager>();
        notificationController.medicine = actualMed;
    }

    private async void SetNotifications()
    {
        List<Medicine> medicines =  await SupabaseController.GetUserMedicines();
        foreach(Medicine medicine in medicines)
        {
            foreach(ScheduleObj schedule in medicine.schedules)
            {
                Notifications.Instance.CreateNotification(medicine.startDay, schedule.time, medicine);
                Debug.Log("noti creada");
            }
        }
    }
}
