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
    public bool boxActive;
    public TMP_Text[] medicinesTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMedicines();
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

    private void SetMedicines()
    {
        for (int i = 0; i < recipe.medicines.Count; i++)
        {
            medicinesTxt[i].text = $"{recipe.medicines[i].name}\n {recipe.medicines[i].hoursXDay[0]}";
        }
    }
}
