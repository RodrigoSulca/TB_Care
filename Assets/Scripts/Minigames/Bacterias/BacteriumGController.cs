using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BacteriumGController : MonoBehaviour
{
    
    [Header("Bullets")]
    public GameObject bulletPref;
    public float bulletSpeed;
    public float shootCD;
    private bool canShoot = true;
    [Header("Timer")]
    public Slider timerSlider;
    private float currentTime;
    [Header("Config")]
    public Transform originPos;
    public Image dmgLoansImg;
    public TMP_Text pointsTxt;
    public float duration;
    public GameObject finishPanel;
    public int points;
    public bool inGame;
    public bool winGame;

    void Start()
    {
        Time.timeScale = 0;
    }
    void Update()
    {
        if (inGame)
        {
            pointsTxt.text = points.ToString();
            Timer();

            if (Input.GetMouseButtonDown(0) && canShoot)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    public void TakeDamage(float dmg)
    {
        dmgLoansImg.fillAmount += dmg;
        if (dmgLoansImg.fillAmount == 1)
        {
            FinishGame(false);
        }
    }
    
    public void StartGame()
    {
        inGame = true;
        Time.timeScale = 1;
    }

    public void FinishGame(bool win)
    {
        Time.timeScale = 0;
        winGame = win;
        finishPanel.SetActive(true);
    }

    private IEnumerator Shoot()
    {
        canShoot = false;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;
        Vector2 direction = (mousePos - originPos.position).normalized;

        GameObject bullet = Instantiate(bulletPref, originPos.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        yield return new WaitForSeconds(shootCD);
        canShoot = true;
    }
    
    private void Timer()
    {
        if (timerSlider.value <= 0)
        {
            FinishGame(true);
        }

        currentTime += Time.deltaTime;
        float progress = currentTime / duration;
        timerSlider.value = Mathf.Lerp(timerSlider.maxValue, 0, progress);
    }
}
