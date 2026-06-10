using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;
public class DialogController : MonoBehaviour
{
    public bool hasRecipe;
    [SerializeField] private TextAsset csvFile;
    [SerializeField] private Button nextBtn;
    [SerializeField] private TMP_Text dialogTxt;
    [SerializeField] private GameObject[] credentialPanels;

    [Header("Credentials")]
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField ageField;
    private int dialogIndex = 0;
    private int credentialsIndex = 0;

    // Estructura para guardar cada fila
    private class DialogData
    {
        public string dialog;
        public bool data;

        public DialogData(string d, bool da)
        {
            dialog = d;
            data = da;
        }
    }

    private List<DialogData> dialogs = new List<DialogData>();

    void Start()
    {
        LoadCSV();
        nextBtn.onClick.AddListener(NextDialog);
        //ageField.onValueChanged.AddListener(CheckCredentials);
        NextDialog();
    }

    void LoadCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("No CSV asignado");
            return;
        }

        string[] lines = csvFile.text.Split('\n');

        // Empezamos desde 1 para saltar el header
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i])) continue;

            string[] values = lines[i].Split(';');

            if (values.Length >= 3)
            {
                string dialog = values[0];
                bool data = bool.Parse(values[2].Trim());

                dialogs.Add(new DialogData(dialog, data));
            }
        }
    }

    void NextDialog()
    {
        if (dialogIndex >= dialogs.Count)
        {
            EndDialogs();
            return;
        }

        DialogData current = dialogs[dialogIndex];
        if(credentialsIndex > 0) credentialPanels[credentialsIndex-1].SetActive(false);
        if (current.data)
        {
            credentialPanels[credentialsIndex].SetActive(true);
            nextBtn.interactable = false;
            credentialsIndex++;
        }
        else
        {
            nextBtn.interactable = true;
        }
        dialogTxt.text = current.dialog;
        
        dialogIndex++;
    }

    public void CheckCredentials()
    {
        if(nameField.text.Trim() != "" && ageField.text.Trim() != "")
        {
            nextBtn.interactable = true;
        }
        else
        {
            nextBtn.interactable = false;
        }
    }

    void EndDialogs()
    {
        gameObject.SetActive(false);
    }

    public void SkipDialog()
    {
        dialogIndex++;
    }

    public void HasRecipe(bool res)
    {
        hasRecipe = res;
    }
}
