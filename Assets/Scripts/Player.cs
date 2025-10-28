
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Player : MonoBehaviour
{
    public float speed = 0f;

    public int applesCount;
    // player's health
    public int hp = 10;

    public Vector2 direction;

    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float jumpVelocity = 12f; 
    [SerializeField] private float fallMultiplier = 2.5f; 
    [SerializeField] private float lowJumpMultiplier = 2f;
    private Rigidbody2D rb;
    private bool isGronded;
    public Enemy enemy;
    private Animator animator;

    void Start()
    {
        applesCount = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction.x * speed, rb.linearVelocity.y);
        }
        else
        {
            transform.position += (Vector3)direction * speed * Time.deltaTime;
        }

        isGronded = Physics2D.Raycast(transform.position, Vector2.down, 0.15f);

        if (rb != null)
        {
            if (rb.linearVelocity.y < 0)
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (rb.linearVelocity.y > 0 && !Input.GetKey(KeyCode.W))
            {
                rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }
        }
    }

    public void PlayerMovement()
    {
        float h = 0f;
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetFloat("Speed", 1);
            h = 1f;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            animator.SetFloat("Speed", 1);
            h = -1f;
        }

        if (h != 0f)
        {
            speed = 2f;
            direction = new Vector2(h, 0f);
        }
        else
        {
            speed = 0f;
            direction = Vector2.zero;
            animator.SetFloat("Speed", 0);
        }

        
        if (Input.GetKeyDown(KeyCode.W) && isGronded)
        {
            if (rb != null)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            }
            else
            {
                transform.position += Vector3.up * (jumpVelocity * 0.1f);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            Attack();
        }
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

    public void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log($"Player took {amount} damage. HP now: {hp}");
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
        // placeholder: disable the player for now
        gameObject.SetActive(false);
    }

    private void Attack()
    {
        if (enemy != null)
            enemy.TakeDamage(5);
    }


}
