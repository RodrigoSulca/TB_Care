using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateRecipe : MonoBehaviour
{
    [Header("InputFields")]
    [SerializeField] private TMP_InputField nameIF;
    [SerializeField] private TMP_InputField frequencyIF;
    [SerializeField] private TMP_InputField daysIF;
    public Button setHours;
    public Button createRecipe;

    [Header("Config")]
    public GameObject hourPref;
    public Transform hoursBox;
    public GameObject medicinePref;
    public Transform medicinesBox;
    public List<string> hoursXDay;
    public List<Medicine> newMedicines;
    public Recipe recipe;

    void Update()
    {
        if (newMedicines.Count > 0)
        {
            createRecipe.interactable = true;
        }
        else
        {
            createRecipe.interactable = false;
        }

        if (frequencyIF.text != "")
        {
            setHours.interactable = true;
        }
        else
        {
            setHours.interactable = false;
        }
    }

    public void AddMedicine()
    {
        SetMedicineHours();
        Medicine medicine = new()
        {
            name = nameIF.text,
            hoursXDay = new List<string>(hoursXDay),
            days = int.Parse(daysIF.text)
        };
        newMedicines.Add(medicine);
        nameIF.text = "";
        frequencyIF.text = "";
        daysIF.text = "";
        hoursXDay.Clear();
        MedicinePrefab(medicine);
    }

    public void SaveRecipe()
    {
        string json = JsonUtility.ToJson(recipe);
        File.WriteAllText(Application.persistentDataPath + "/recipe.json", json);
        Debug.Log(Application.persistentDataPath + "/recipe.json");
    }

    public void SpawnHours()
    {
        hoursXDay.Clear();
        for (int i = 0; i < int.Parse(frequencyIF.text); i++)
        {
            GameObject hour = Instantiate(hourPref, hoursBox);
            hour.GetComponentInChildren<TMP_Text>().text = $"{i + 1}.";
        }
    }

    public void UpdateRecipe(string sceneName)
    {
        foreach (Medicine medicine in newMedicines)
        {
            recipe.medicines.Add(medicine);
        }
        SaveRecipe();
        SceneManager.LoadScene(sceneName);
    }

    private void SetMedicineHours()
    {
        GameObject[] hours = GameObject.FindGameObjectsWithTag("Hour");
        foreach (GameObject hour in hours)
        {
            hoursXDay.Add(hour.GetComponentInChildren<TMP_InputField>().text);
            Destroy(hour);
        }
    }

    private void MedicinePrefab(Medicine medicine)
    {
        MedicinePreview preview = Instantiate(medicinePref, medicinesBox).GetComponent<MedicinePreview>();
        preview.medicineName.text = medicine.name;
        preview.medicineDays.text = $"{medicine.days.ToString()} dias";
        preview.medicine = medicine;
    }
}
