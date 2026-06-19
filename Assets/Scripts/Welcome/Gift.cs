using UnityEngine;

public class Gift : MonoBehaviour
{
    [SerializeField] private int cantClicks;
    [SerializeField] private GameObject petImg;
    [SerializeField] private DialogController dialogController;
    private int actualClicks;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        dialogController.nextBtn.interactable = false;
    }

    public void ClickGift()
    {
        actualClicks++;
        if (actualClicks == cantClicks) {

            petImg.SetActive(true);
            gameObject.SetActive(false);

            dialogController.NextDialog();
            dialogController.nextBtn.interactable = true;
        }
    }
}
