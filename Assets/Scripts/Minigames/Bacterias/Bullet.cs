using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime;
    private Bacterium bacterium;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bacterium"))
        {
            bacterium = collision.GetComponent<Bacterium>();
            bacterium.TakeDamage();
            Destroy(gameObject);
        }
    }
}
