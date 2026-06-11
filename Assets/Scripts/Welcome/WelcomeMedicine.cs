using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WelcomeMedicine : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField notesField;
    [SerializeField] private TMP_Dropdown frequencyDrop;
    [SerializeField] private GameObject[] creationPanels;
    [SerializeField] private Button nextBtn;

    [Header("Config")]
    [SerializeField] private DialogController dialogController;
    public DateTime startDate = DateTime.Today;
    public DateTime finishDate = DateTime.Today;
    public string medicineTime = DateTime.Now.ToString("HH:mm");
    private List<ScheduleObj> schedules = new();

    private void OnEnable()
    {
        nextBtn.onClick.AddListener(SetFrequency);
    }

    public void SetFrequency()
    {
        creationPanels[frequencyDrop.value].SetActive(true);

        nextBtn.onClick.RemoveListener(SetFrequency);
        dialogController.nextBtn.interactable = true;
    }

    public async void Create()
    {
        var scheduleObj = new ScheduleObj
        {
            time = medicineTime,
            weekdays = new int[] { 1, 2, 3, 4, 5, 6, 7 }
        };
        schedules.Add(scheduleObj);
        await SupabaseController.CreateMedicine(nameField.text, notesField.text, startDate, schedules);
        SceneManager.LoadScene("Home");
    }

    public void CheckName()
    {
        if(nameField.text.Trim() != "")
        {
            dialogController.nextBtn.interactable=true;
        }
        else
        {
            dialogController.nextBtn.interactable = false;
        }
    }
}
