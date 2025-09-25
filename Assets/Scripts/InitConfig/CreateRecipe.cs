using TMPro;
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateRecipe : MonoBehaviour
{
    [Header("InoputFields")]
    public TMP_InputField medicineName;
    public TMP_InputField timesXDay;
    public TMP_InputField days;
    public Button createRecipe;

    [Header("Config")]
    public GameObject hourPref;
    public Transform hoursBox;
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
    }

    public void AddMedicine()
    {
        SetMedicineHours();
        Medicine medicine = new()
        {
            name = medicineName.text,
            hoursXDay = new List<string>(hoursXDay),
            days = int.Parse(days.text)
        };
        newMedicines.Add(medicine);
        medicineName.text = "";
        timesXDay.text = "";
        days.text = "";
        hoursXDay.Clear();
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
        for (int i = 0; i < int.Parse(timesXDay.text); i++)
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
}
