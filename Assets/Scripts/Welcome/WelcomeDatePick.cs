using System;
using System.Globalization;
using TMPro;
using UnityEngine;

public class WelcomeDatePick : MonoBehaviour
{
    [SerializeField] private TMP_Text firstDateTxt;
    [SerializeField] private TMP_Text timeTxt;
    [SerializeField] private TMP_Text lastDateTxt;
    [SerializeField] private WelcomeMedicine welcomeMedicine;
    private bool firstDate;

    private void Start()
    {
        firstDateTxt.text = DateTime.Today.ToString("d/M/yyyy");
        lastDateTxt.text = DateTime.Today.ToString("d/M/yyyy");
        timeTxt.text = DateTime.Now.ToString("HH:mm");
    }

    public void OpenDatePicker()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR

                    using AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                    AndroidJavaObject plugin = new("com.example.datepicker.DatePickerPlugin");
                    plugin.Call("showDatePickerW", activity);

        #else
                welcomeMedicine.startDate = DateTime.Today;
        #endif

    }

    public void OpenTimePicker()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
                    using AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

                    AndroidJavaObject plugin = new("com.example.datepicker.DatePickerPlugin");
                    plugin.Call("showTimePickerW", activity);
        #else

                DateTime simulatedTime = DateTime.Now;
                string timeOnly = simulatedTime.ToString("HH:mm");
                welcomeMedicine.medicineTime = timeOnly;
        #endif
    }

    public void OnDateSelected(string date)
    {
        DateTime parsedDate = DateTime.ParseExact(
            date,
            "d/M/yyyy",
            CultureInfo.InvariantCulture
        );

        if (firstDate)
        {
            firstDateTxt.text = date;
            welcomeMedicine.startDate = parsedDate;
        }
        else
        {
            lastDateTxt.text = date;
            welcomeMedicine.finishDate = parsedDate;
        }
    }

    public void OnTimeSelected(string time)
    {
        timeTxt.text = time;
        DateTime parsedTime = DateTime.ParseExact(
            time,
            "H:m", // formato flexible (ej: 8:5)
            CultureInfo.InvariantCulture
        );
        timeTxt.text = time;
        welcomeMedicine.medicineTime = time;
    }

    public void SetDateType(bool first)
    {
        firstDate = first;
    }
}
