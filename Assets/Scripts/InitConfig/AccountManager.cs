using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [Header("UI Register/Login")]
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public TMP_InputField emailField;
    public TMP_InputField passwordField;
    public Button registerButton;
    [Header("UI Panels")]
    public GameObject registerPanel;
    public GameObject recipePanel;

    void Update()
    {
        if(nameField.text.Trim() == ""|| ageField.text.Trim() == ""|| emailField.text.Trim() == ""|| passwordField.text.Trim() == "")
        {
            registerButton.interactable = false;
        }
        else
        {
            registerButton.interactable = true;
        }
    }
    public async void RegisterUser()
    {
        bool response = await SupabaseController.CreateUser(nameField.text, int.Parse(ageField.text), emailField.text);
        if (response)
        {
            registerPanel.SetActive(false);
            recipePanel.SetActive(true);
        }
        else
        {
            
        }
    }
}
