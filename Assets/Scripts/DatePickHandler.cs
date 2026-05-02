using System;
using System.Globalization;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CreateMedicine))]
public class DatePickHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text dateTxt;
    [SerializeField] private TMP_Text timeTxt;
    private CreateMedicine createMedicine;


    void Start()
    {
        createMedicine = GetComponent<CreateMedicine>();
    }

    public void OpenDatePicker()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR

            using AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject plugin = new("com.example.datepicker.DatePickerPlugin");
            plugin.Call("showDatePicker", activity);

        #else
            createMedicine.startDate = DateTime.Today;
        #endif

    }

    public void OpenTimePicker()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
            using AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
            AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

            AndroidJavaObject plugin = new("com.example.datepicker.DatePickerPlugin");
            plugin.Call("showTimePicker", activity);
        #else

            DateTime simulatedTime = DateTime.Now;
            string timeOnly = simulatedTime.ToString("HH:mm");
            createMedicine.medicineTime = timeOnly;
        #endif
    }

    public void OnDateSelected(string date)
    {
        dateTxt.text = date;
        DateTime parsedDate = DateTime.ParseExact(
            date,
            "d/M/yyyy",
            CultureInfo.InvariantCulture
        );
        createMedicine.startDate = parsedDate;
    }

    public void OnTimeSelected(string time)
    {
        timeTxt.text = time;
        DateTime parsedTime = DateTime.ParseExact(
            time,
            "H:m", // formato flexible (ej: 8:5)
            CultureInfo.InvariantCulture
        );
        createMedicine.medicineTime = time;
    }
}
