using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{ 
    public float speed;
    public float jump;

    private Rigidbody2D rb;

    [SerializeField] private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

        if (horizontalInput != 0) transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);

        anim.SetFloat("speedX", Mathf.Abs(rb.velocity.x));

        // if(rb.velocity.x > 0)
        // {
        //     gameObject.transform.localScale = new Vector3(1, 1, 1);
        // }
        // else if(rb.velocity.x < 0)
        // {
        //     gameObject.transform.localScale = new Vector3(-1, 1, 1);
        // }
    }
}
