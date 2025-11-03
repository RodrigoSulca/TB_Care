using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CalendarController : MonoBehaviour
{
    public Recipe recipe;
    public List<Medicine> pendingMed;
    public GameObject medicineBox;
    public Transform canvas;
    public Transform calendar;
    public GameObject calendarItem;
    public bool boxActive;
    public TMP_Text dayTxt;
    public string[] days = { "Lunes","Martes","Miercoles","Jueves","Viernes","Sabado","Domingo" };
    public int dayIndex;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMedicines(dayIndex);
        pendingMed = new List<Medicine>(recipe.dayMedicines);
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
                    recipe.dayMedicines.Remove(medicine);
                    boxActive = true;
                    break;
                }
            }
        }
    }

    public void NextDay(bool next)
    {
        if (next && dayIndex < days.Length-1)
        {
            dayIndex++;
            SetMedicines(dayIndex);
        }
        else if (!next && dayIndex > 0)
        {
            dayIndex--;
            SetMedicines(dayIndex);
        }
        dayTxt.text = days[dayIndex];
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

    private void SetMedicines(int day)
    {
        ClearCalendar();
        foreach (Medicine medicine in recipe.medicines)
        {
            CalendarItem item = Instantiate(calendarItem, calendar).GetComponent<CalendarItem>();
            item.medicine = medicine;
            item.day = day;
        }
    }
    
    private void ClearCalendar()
    {
        for (int i = calendar.childCount - 1; i >= 0; i--)
    {
        GameObject child = calendar.GetChild(i).gameObject;
        Destroy(child);
    }
    }
}
