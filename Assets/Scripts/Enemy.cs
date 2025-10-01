using UnityEngine;

public class Enemy : MonoBehaviour
{
    static int Damage = 10;
    private float health = 100;
    private Rigidbody2D rb;

    private float speed = 3f;

    private float distanceBetweenPlayer = 0;

    private Vector2 direction;
    public Player player;

    private bool canAttack = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Vector2.right;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceBetweenPlayer = transform.position - player.transform.position;
        if (distanceBetweenPlayer < 1f)
            Attack();
    }

    public void EnemyMovement()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Collider2D>() != null
        && collision.gameObject.CompareTag("AIColliders"))
        {
            direction = -direction;
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            player.hp -= Damage;

        }
        StartCoroutine(WaitForAttack());
        canAttack = true;
    }
    IEnumerator WaitForAttack()
    {
        yield return new WaitForSeconds(3f);
    }
    
}
