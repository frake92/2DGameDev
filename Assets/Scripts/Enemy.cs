using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float health = 10f;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 3f;

    private float distanceToPlayer = Mathf.Infinity;

    private Vector2 direction = Vector2.right;
    public Player player;

    private bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // try to auto-find the player if not assigned
        if (player == null)
        {
            var pgo = GameObject.FindWithTag("Player");
            if (pgo != null) player = pgo.GetComponent<Player>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // basic patrol movement
        EnemyMovement();

        if (player != null)
        {
            distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= attackRange)
            {
                Attack();
            }
        }
    }

    public void EnemyMovement()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // optional: flip sprite depending on movement direction
        if (transform.localScale.x < 0 && direction.x > 0)
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        else if (transform.localScale.x > 0 && direction.x < 0)
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    }

    // flip direction when hitting an object tagged as patrol endpoint
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null && collision.gameObject.CompareTag("AICollider"))
        {
            direction = -direction;
        }
    }

    void Attack()
    {
        if (!canAttack || player == null) return;

        canAttack = false;
        // use Player's TakeDamage method
        player.TakeDamage(damage);
        StartCoroutine(WaitForAttack());
    }

    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
