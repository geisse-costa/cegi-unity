using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jump;

    private Rigidbody2D rb;
    [SerializeField] private Animator anim;
    //Aula 5
    [SerializeField] private GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

        if (horizontalInput != 0) transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
            anim.SetBool("isGrounded", false);
        }

        anim.SetFloat("Vertical Speed", rb.velocity.y);

        if (rb.velocity.y == 0)
        {
            anim.SetBool("isGrounded", true);

            anim.SetFloat("Movement", Mathf.Abs(rb.velocity.x));
        }

        //Aula 5
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab, transform);
        }
    }
}
