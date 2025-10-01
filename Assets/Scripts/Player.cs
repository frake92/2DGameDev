using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Collider2D>() != null)
        {
            // Itt csinálj valamit ha ütközik
            Debug.Log("Ütközés történt!");
            
            // Példa: irányváltás
            direction = -direction;
        }
    }
}
