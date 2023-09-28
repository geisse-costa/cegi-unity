using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(transform.parent.localScale.x * speed + transform.parent.GetComponent<Rigidbody2D>().velocity.x, 0);
        rb.velocity = direction;
        transform.SetParent(null); //Tira qualquer parentesco que o objeto tenha 
    }

    private void OnTriggerEnter2D(Collider2D collision) //Tira a colisão entre duas balas
    {
        if (collision.tag != "Player" && collision.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
