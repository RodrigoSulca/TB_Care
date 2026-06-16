using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DialogController : MonoBehaviour
{
    [Header("UI Config")]
    public bool hasRecipe;
    public Button nextBtn;
    [SerializeField] private TextAsset csvFile;
    [SerializeField] private TMP_Text dialogTxt;
    [SerializeField] private GameObject[] credentialPanels;
    [SerializeField] private Animator animator;

    [Header("Credentials")]
    [SerializeField] private TMP_InputField nameField;
    [SerializeField] private TMP_InputField ageField;
    [SerializeField] private Button[] recipeBtns;
    [SerializeField] private Sprite selectedSprite;
    [SerializeField] private Sprite originSprite;

    [HideInInspector] public int credentialsIndex = 0;
    private int dialogIndex = 0;
    private string playerName;
    private bool skiped;

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
        if (hasRecipe && !skiped)
        {
            dialogIndex += 2;
            skiped = true;
        }

        if (dialogIndex == 8 || dialogIndex >= dialogs.Count)
        {
            SceneManager.LoadScene("Home");
            return;
        }

        DialogData current = dialogs[dialogIndex];

        if (credentialsIndex > 0)
            credentialPanels[credentialsIndex - 1].SetActive(false);

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
        Debug.Log(credentialsIndex);
        if(credentialsIndex == 5)
        {
            nextBtn.interactable = true;
        }

        string dialog = current.dialog.Replace("*nombre*", playerName);
        dialogTxt.text = dialog;
        animator.SetTrigger("Talk");
        

        dialogIndex++;
    }

    public void CheckCredentials()
    {
        if(nameField.text.Trim() != "" && ageField.text.Trim() != "")
        {
            nextBtn.interactable = true;
            playerName = nameField.text;
        }
        else
        {
            nextBtn.interactable = false;
        }
    }

    public void HasRecipe(bool res)
    {
        hasRecipe = res;
        foreach(Button button in recipeBtns)
        {
            Image image = button.GetComponent<Image>();
            if (EventSystem.current.currentSelectedGameObject == button.gameObject)
            {
                image.sprite = selectedSprite;
            }
            else
            {
                image.sprite = originSprite;
            }
        }
    }
}
