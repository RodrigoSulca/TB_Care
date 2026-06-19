using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OnBoarding : MonoBehaviour
{
    [SerializeField] private Sprite[] carouselSprites;
    [SerializeField] private Image carouselImg;

    [SerializeField] private TMP_Text carouselTxt;
    [SerializeField] private string[] carouselTexts;

    [SerializeField] private Button[] carouselBtns;
    [SerializeField] private Image carouselCircles;

    private int carouselIndex;
    private float circleFill = 0.2f;

    public void NextBoard()
    {
        if (carouselIndex < carouselTexts.Length) 
        {
            carouselIndex++;
            carouselImg.sprite = carouselSprites[carouselIndex];
            carouselTxt.text = carouselTexts[carouselIndex];
            circleFill *= 3;
            carouselCircles.fillAmount = circleFill;
        }
        else
        {
            carouselBtns[1].interactable = false;
        }
    }

    public void PrevBoard() 
    {
        if (carouselIndex > 0)
        {
            carouselIndex--;
            carouselImg.sprite = carouselSprites[carouselIndex];
            carouselTxt.text = carouselTexts[carouselIndex];
            circleFill/= 3;
            carouselCircles.fillAmount = circleFill;
        }
        else
        {
            carouselBtns[0].interactable = false;
        }
    }
}
