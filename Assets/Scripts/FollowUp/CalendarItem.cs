using TMPro;
using UnityEngine;

public class CalendarItem : MonoBehaviour
{
    [Header("UI")]
    public TMP_Text medicineName;
    public TMP_Text medicineHour;

    [Header("Config")]
    public Medicine medicine;
    public int day;
    public bool consumed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        medicineName.text = medicine.name;
        if(day < medicine.days)
        {
            medicineHour.text = medicine.hoursXDay[0];
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
