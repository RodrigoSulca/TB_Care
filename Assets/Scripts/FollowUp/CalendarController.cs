using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalendarController : MonoBehaviour
{
    public Recipe recipe;
    public List<Medicine> pendingMed;
    public GameObject medicineBox;
    public Transform canvas;
    public bool boxActive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pendingMed = new List<Medicine>(recipe.medicines);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSpan actualTime = DateTime.Now.TimeOfDay;
        actualTime = new TimeSpan(actualTime.Hours, actualTime.Minutes, 0);

        foreach (Medicine medicine in pendingMed)
        {
            if (TimeSpan.TryParse(medicine.hoursXDay[0], out TimeSpan medicineTime))
            {
                if (medicineTime <= actualTime && !boxActive)
                {
                    SpawnMedicineBox(medicine);
                    pendingMed.Remove(medicine);
                    boxActive = true;
                    break;
                }
            }
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void SpawnMedicineBox(Medicine actualMed)
    {
        CheckMedicine checkMedicine = Instantiate(medicineBox, canvas).GetComponent<CheckMedicine>();
        checkMedicine.medicine = actualMed;
        checkMedicine.calendarController = GetComponent<CalendarController>();
    }
}
