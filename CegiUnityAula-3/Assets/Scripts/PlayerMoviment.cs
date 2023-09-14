using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    public float speed;
    public float jump;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (Input.GetButtonDown ("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if(rb.velocity.x > 0)
        {
            gameObject.transform.localScale = new Vector3(1,1,1);
        }
        else if(rb.velocity.x < 0)
        {
            gameObject.transform.localScale = new Vector3(-1,1,1);
        }
    }
}
