using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestItem : MonoBehaviour
{
    public Quest quest;
    [Header("UI")]
    public TMP_Text questDesc;
    public TMP_Text reward;
    public Slider progressSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        questDesc.text = quest.description;
        reward.text = quest.reward.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
