using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnBoarding : MonoBehaviour
{
    [SerializeField] private Sprite[] carouselSprites;
    [SerializeField] private Image carouselImg;

    [SerializeField] private TMP_Text carouselTxt;
    [SerializeField] private string[] carouselTexts;

    [SerializeField] private Image carouselCircles;

    private int carouselIndex;
    private float circleFill = 0.2f;

    public void NextBoard()
    {
        if (carouselIndex < carouselTexts.Length-1) 
        {
            carouselIndex++;
            circleFill *= 3;
        }
        else
        {
            carouselIndex = 0;
            circleFill = 0.2f;
        }

        carouselImg.sprite = carouselSprites[carouselIndex];
        carouselTxt.text = carouselTexts[carouselIndex];
        carouselCircles.fillAmount = circleFill;
    }
}
