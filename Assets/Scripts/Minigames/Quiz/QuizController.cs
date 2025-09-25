using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizController : MonoBehaviour
{
    [Header("UI")]
    public GameObject finalPanel;
    public TMP_Text answersTxt;
    public TMP_Text finalMsgTxt;
    public TMP_Text questionTxt;
    public TMP_Text[] optionTxts;
    public Question[] questions;
    [Header("Config")]
    public TextAsset quizJson;
    public int questionIndex;
    public int answerId;
    public int correctAnswers;
    public string[] finalMsgs;
    public PlayerStats playerStats;
    void Start()
    {
        LoadQuestions();
        NextQuestion();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckAnswer(int userAnswer)
    {
        if (answerId == userAnswer)
        {
            Debug.Log("Correcto");
            correctAnswers++;
        }
        else
        {
            Debug.Log("Incorrecto");
        }

        NextQuestion();
    }
    private void NextQuestion()
    {
        if (questionIndex < questions.Length)
        {
            questionTxt.text = questions[questionIndex].question;
            answerId = questions[questionIndex].answer;
            for (int i = 0; i < questions[questionIndex].options.Length; i++)
            {
                optionTxts[i].text = questions[questionIndex].options[i];
            }
            questionIndex++;
        }
        else
        {
            ShowResults();
        }
    }

    private void ShowResults()
    {
        finalPanel.SetActive(true);
        answersTxt.text = $"{correctAnswers}/{questions.Length}";
        if (correctAnswers <= questions.Length / 2)
        {
            finalMsgTxt.text = finalMsgs[0];
        }
        else if (correctAnswers > questions.Length / 2)
        {
            finalMsgTxt.text = finalMsgs[1];
        }
        playerStats.totalCoins += correctAnswers * 10;
    }
    private void LoadQuestions()
    {
        if (quizJson != null)
        {
            Question[] loadedQuestions = JsonHelper.FromJson<Question>(quizJson.text);
            questions = loadedQuestions;
            Debug.Log("Preguntas cargadas: " + questions.Length);
        }
        else
        {
            Debug.LogError("No se encontró el archivo quiz.json en Resources.");
        }
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Minigames");
    }
}
