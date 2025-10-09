using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [Header("UI")]
    public GameObject finalPanel;
    public TMP_Text answersTxt;
    public TMP_Text finalMsgTxt;
    public TMP_Text questionTxt;
    public TMP_Text[] optionTxts;
    public Question[] questions;
    public Slider timeSlider;
    [Header("Config")]
    public bool inGame;
    public TextAsset quizJson;
    public int questionIndex;
    public int answerId;
    public int correctAnswers;
    public float questionTime;
    public string[] finalMsgs;
    public PlayerStats playerStats;
    private float timer;
    void Start()
    {
        LoadQuestions();
    }

    // Update is called once per frame
    void Update()
    {
        if (inGame)
        {
            QuestionTimer();
        }
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

    public void StartQuiz()
    {
        NextQuestion();
        inGame = true;
    }
    private void NextQuestion()
    {
        timer = 0;
        timeSlider.value = timeSlider.maxValue;
        if (questionIndex < questions.Length)
        {
            timeSlider.maxValue = questionTime;
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
        playerStats.dailyMG += 1;
        PlayerPrefs.SetInt("coins", playerStats.totalCoins);
        PlayerPrefs.Save();
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

    private void QuestionTimer()
    {
        if (timeSlider.value <= 0)
        {
            NextQuestion();
        }

        timer += Time.deltaTime;
        float progress = timer / questionTime;
        timeSlider.value = Mathf.Lerp(timeSlider.maxValue, 0, progress);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Minigames");
    }
}
