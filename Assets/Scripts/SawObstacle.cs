using UnityEngine;
using System.Collections;

public class SawObstacle : MonoBehaviour
{

    Vector2 direction;
    Collider2D obstacleCollider;
    public float speed = 2f;
    public int damage = 1;

    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        direction = Vector2.right;
        obstacleCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider != null)
        {
            if (collision.gameObject.CompareTag("AICollider"))
            {
                direction = -direction;
            }

            else if (collision.gameObject.CompareTag("Player"))
            {
                player.TakeDamage(damage);
                StartCoroutine(DamageIndicator());

            }
        }
    }

    IEnumerator DamageIndicator()
    {
        player.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 255);
        yield return new WaitForSeconds(1f);
        player.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }


}
