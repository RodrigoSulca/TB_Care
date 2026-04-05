using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(WelcomeController))]
public class CreateMedicine : MonoBehaviour
{
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField notesField;
    [SerializeField] private TMP_Dropdown frequencyDrop;
    [SerializeField] private GameObject[] creationPanels;
    public string startDate;
    public string medicineTime;
    private WelcomeController welcomeController;

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
        await SupabaseController.CreateMedicine(nameField.text, notesField.text);
    }
}
