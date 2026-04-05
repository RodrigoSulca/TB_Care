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
        using AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject plugin = new("com.example.datepicker.DatePickerPlugin");
        plugin.Call("showDatePicker", activity);
    }

    public void OpenTimePicker()
    {
        using AndroidJavaClass unityPlayer = new("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        AndroidJavaObject plugin = new("com.example.datepicker.DatePickerPlugin");
        plugin.Call("showTimePicker", activity);
    }

    public void OnDateSelected(string date)
    {
        dateTxt.text = date;
        createMedicine.startDate = date;
    }

    public void OnTimeSelected(string time)
    {
        timeTxt.text = time;
        createMedicine.medicineTime = time;
    }
}
