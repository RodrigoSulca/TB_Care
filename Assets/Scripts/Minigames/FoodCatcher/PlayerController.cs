using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool right;
    public bool left;
    public bool win = true;
    public int foodPoints;
    public int totalPoints;

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            MoveRight();
        }
        else if (left)
        {
            MoveLeft();
        }
    }

    public void SetRight()
    {
        right = !right;
    }


    public void SetLeft()
    {
        left = !left;
    }
    private void MoveRight()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.right);
        transform.localScale = new Vector3(-1, 1, 1);
    }

    private void MoveLeft()
    {
        transform.Translate(speed * Time.deltaTime * Vector2.left);
        transform.localScale = new Vector3(1, 1, 1);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GoodFood"))
        {
            Debug.Log("Good");
            totalPoints += foodPoints;
        }
        else if (collision.gameObject.CompareTag("BadFood"))
        {
            win = false;
        }
    }
}
