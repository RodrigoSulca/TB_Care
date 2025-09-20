using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotificationController : MonoBehaviour
{
    public float duration;
    public Medicine medicine;
    public TMP_Text notificationTxt;
    public Slider notificationSlider;
    [HideInInspector] public NotificationsManager notificationsManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        notificationTxt.text = $"Es hora de tomar {medicine.name}!";
        StartCoroutine(NotificationTimer());
    }

    public IEnumerator NotificationTimer()
    {
        float currentTime = 0;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentTime / duration);
            notificationSlider.value = Mathf.Lerp(notificationSlider.maxValue, notificationSlider.minValue, t);
            yield return null;
        }
        notificationsManager.notificationActive = false;
        Destroy(gameObject);
    }

    public void ClickNotification(string sceneName)
    {
        notificationsManager.notificationActive = false;
        SceneManager.LoadScene(sceneName);
    }
}
