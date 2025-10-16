using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;
public class Bacterium : MonoBehaviour
{
    public float lifeTime;
    public float dmg;
    public int killPoints;
    public TMP_Text pointsTxt;
    private SpriteRenderer bacteriumImg;
    private BacteriumGController bacteriumGController;
    private Animator pointsAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bacteriumGController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BacteriumGController>();
        bacteriumImg = GetComponent<SpriteRenderer>();
        pointsAnimator = pointsTxt.GetComponent<Animator>();
        pointsTxt.text = $"+{killPoints}";
        bacteriumImg.DOFade(1,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = bacteriumImg.color.a;
        if (Mathf.Approximately(alpha, 1.0f))
        {
            bacteriumGController.TakeDamage(dmg);
            DestroyBacterium();
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        DestroyBacterium();
        bacteriumGController.points += killPoints;
        pointsAnimator.SetTrigger("Defeat");
        Destroy(gameObject, 1);
    }
    
    private void DestroyBacterium()
    {
        DOTween.Kill(bacteriumImg);
        bacteriumImg.enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}
