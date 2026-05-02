using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(WelcomeController))]
public class CreateMedicine : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField notesField;
    [SerializeField] private TMP_Dropdown frequencyDrop;
    [SerializeField] private GameObject[] creationPanels;
    public DateTime startDate = DateTime.Today;
    public string medicineTime = DateTime.Now.ToString("HH:mm");
    private WelcomeController welcomeController;
    private List<ScheduleObj> schedules = new();

    void Start()
    {
        welcomeController = GetComponent<WelcomeController>();
    }

    public void SetFrequency()
    {
        welcomeController.ChangePanel(creationPanels[frequencyDrop.value]);
    }

    public async void Create()
    {
        var scheduleObj = new ScheduleObj
        {
            time = medicineTime,
            weekdays = new int[] {1,2,3,4,5,6,7}
        };
        schedules.Add(scheduleObj);
        await SupabaseController.CreateMedicine(nameField.text, notesField.text, startDate, schedules );
        SceneManager.LoadScene("Home");
    }
}
