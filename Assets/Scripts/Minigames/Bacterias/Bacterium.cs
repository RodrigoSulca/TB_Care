using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class Bacterium : MonoBehaviour
{
    public float lifeTime;
    public float dmg;
    public int killPoints;
    private SpriteRenderer bacteriumImg;
    private BacteriumGController bacteriumGController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bacteriumGController = GameObject.FindGameObjectWithTag("GameController").GetComponent<BacteriumGController>();
        bacteriumImg = GetComponent<SpriteRenderer>();
        bacteriumImg.DOFade(1,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = bacteriumImg.color.a;
        if (Mathf.Approximately(alpha, 1.0f))
        {
            bacteriumGController.TakeDamage(dmg);
            Destroy(gameObject);
        }
    }

    public void TakeDamage()
    {
        DOTween.Kill(bacteriumImg);
        bacteriumGController.points += killPoints;
        Destroy(gameObject);
    }
}
