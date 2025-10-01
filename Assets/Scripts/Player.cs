using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 0f;

    public int applesCount;

    public Vector2 direction;
    // Start is called once be = Vector2.right;fore the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        applesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        PlayerMovement();
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
