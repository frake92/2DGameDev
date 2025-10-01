
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 0f;

    public int applesCount;

    public Vector2 direction;

    private float jumpForce = 0.5f;
    private Rigidbody2D rb;
    private bool isGronded;
    // Start is called once be = Vector2.right;fore the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        applesCount = 0;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        PlayerMovement();
        isGronded = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
    }
    public void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            speed = 2f;
            direction = Vector2.right;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            speed = 2f;
            direction = Vector2.left;
        }
        else if (Input.GetKey(KeyCode.W) && isGronded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        else
            speed = 0f;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null
        && collision.gameObject.CompareTag("apple"))
        {
            applesCount++;
            Destroy(collision.gameObject);
        }
    }

    /*
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null
        && collision.gameObject.CompareTag("apple"))
        {
            applesCount++;
        }
    }
    */

}
