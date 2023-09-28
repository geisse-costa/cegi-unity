using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    
    private int lastVerticalSpeedSign;
    private bool onAir;
    private Rigidbody2D rb;

    [SerializeField] private Animator anim;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private AudioClip fireAudio;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stepAudio;

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

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);

            if(!audioSource.loop)
            {
                audioSource.loop = true;
                audioSource.clip = stepAudio;
                audioSource.Play();
            }

        } else{
            audioSource.loop = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        Vector3 velocity = rb.velocity;
        if (Mathf.Abs(velocity.y) <= 0.05f)
        {
            anim.SetFloat("Movement", Mathf.Abs(velocity.x));
        }
        else
        {
            if (!onAir) onAir = true;
            anim.SetFloat("Vertical Speed", velocity.y);
        }

        if (Input.GetButtonDown("Fire1") && !onAir && Mathf.Abs(velocity.x) <= 0.1f)
        {
            if (velocity.x != 0) velocity.x = 0;
            anim.SetTrigger("Shoot");
            Instantiate(bulletPrefab, transform);

            audioSource.PlayOneShot(fireAudio); //Audio da bala
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded", true);
            anim.SetFloat("Vertical Speed", 0);
            onAir = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            anim.SetBool("isGrounded", false);
        }
    }
}
