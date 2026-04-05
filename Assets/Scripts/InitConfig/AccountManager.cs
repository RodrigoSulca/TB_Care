using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [Header("UI Register")]
    public TMP_InputField nameField;
    public TMP_InputField ageField;
    public TMP_InputField emailFieldR;
    public TMP_InputField passwordFieldR;
    public Button registerBtn;

    [Header("UI Login")]
    public TMP_InputField emailFieldL;
    public TMP_InputField passwordFieldL;
    public Button loginBtn;


    void Start()
    {
        registerBtn.onClick.AddListener(RegisterUser);
        loginBtn.onClick.AddListener(LoginUser);
    }
    void Update()
    {
        if(nameField.text.Trim() == ""|| ageField.text.Trim() == ""|| emailFieldR.text.Trim() == ""|| passwordFieldR.text.Trim() == "")
        {
            registerBtn.interactable = false;
        }
        else
        {
            registerBtn.interactable = true;
        }
    }
    public async void RegisterUser()
    {
        bool response = await SupabaseController.CreateUser(nameField.text, int.Parse(ageField.text), emailFieldR.text, passwordFieldR.text, DateTime.UtcNow);
        if (response)
        {
            SceneManager.LoadScene("AddMedicine");
        }
    }

    public async void LoginUser()
    {
        bool response = await SupabaseController.Login(emailFieldL.text, passwordFieldL.text);
        if (response)
        {
            SceneManager.LoadScene("AddMedicine");
        }
    }
}
