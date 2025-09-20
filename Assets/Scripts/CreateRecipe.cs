using TMPro;
using UnityEngine;
using System.IO;

public class CreateRecipe : MonoBehaviour
{
    [Header("InoputFields")]
    public TMP_InputField medicineName;
    public TMP_InputField hours;
    public TMP_InputField days;
    
    [Header("Config")]
    public GameObject notificationPref;
    public GameObject notiSpawner;
    public string[] hoursXDay;
    public Recipe recipe;

    //int.Parse(hours.text)
    public void AddMedicine()
    {
        Medicine medicine = new()
        {
            name = medicineName.text,
            hoursXDay = hoursXDay,
        };
        recipe.medicines.Add(medicine);
        SpawnNotification(medicine.name);
        medicineName.text = "";
        hours.text = "";
        days.text = "";
    }

    public void SaveRecipe()
    {
        string json = JsonUtility.ToJson(recipe);
        File.WriteAllText(Application.persistentDataPath + "/recipe.json", json);
        Debug.Log(Application.persistentDataPath + "/recipe.json");
    }

    private void SpawnNotification(string medicineName)
    {
        GameObject otherNot = GameObject.FindWithTag("Notification");
        Destroy(otherNot);
        
        GameObject notification = Instantiate(notificationPref, notiSpawner.transform);
        notification.GetComponentInChildren<TMP_Text>().text = $"{medicineName}" + " añadid@";
        Destroy(notification, 2);
    }
}
