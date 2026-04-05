using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeController : MonoBehaviour
{
    [SerializeField] private GameObject actualPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangePanel(GameObject nextPanel)
    {
        actualPanel.SetActive(false);
        nextPanel.SetActive(true);
        actualPanel = nextPanel;
    }
}
