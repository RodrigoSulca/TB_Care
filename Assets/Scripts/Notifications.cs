using System;
using System.Collections;
using Unity.Notifications.Android;
using UnityEngine;

public class Notifications : MonoBehaviour
{
    private const string channelId = "notificationChannel";

    #region Singleton
    public static Notifications Instance { get; private set; }

    // 🔹 Configuración inicial del Singleton
    private void Awake()
    {
        // Si ya existe una instancia y no es esta, destruye el duplicado
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asigna la instancia y evita que se destruya al cambiar de escena
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    void Start()
    {
        StartCoroutine(AskPermission());
    }
    public void CreateNotification(TimeSpan notiTime, Medicine medicine)
    {
        AndroidNotificationChannel androidNotificationChannel = new()
        {
            Id = channelId,
            Name = "NotificationChannel",
            Description = "Recipe notifications",
            Importance = Importance.Default
        };

        AndroidNotificationCenter.RegisterNotificationChannel(androidNotificationChannel);

        DateTime timeToNotify = DateTime.Now.Date + notiTime;
        AndroidNotification androidNotification = new()
        {
            Title = "Hora de tu medicina 💊",
            Text = $"Es hora de tomar: {medicine.name}",
            FireTime = timeToNotify
        };

        AndroidNotificationCenter.SendNotification(androidNotification, channelId);
    }

    private IEnumerator AskPermission()
    {
        PermissionRequest request = new();
        while (request.Status == PermissionStatus.RequestPending)
        {
        yield return null;
        }
    }
}
