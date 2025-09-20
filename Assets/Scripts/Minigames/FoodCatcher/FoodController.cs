using UnityEngine;

public class FoodController : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    public float speed;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int num = Random.Range(0, sprites.Length);
        spriteRenderer.sprite = sprites[num];
    }

    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
