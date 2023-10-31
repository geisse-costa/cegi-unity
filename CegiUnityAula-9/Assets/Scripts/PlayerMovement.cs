using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private int contador;
    public float speed;
    public float jumpForce;

    private bool onAir;
    private Rigidbody2D rb;

    [SerializeField] private Animator anim;
    [SerializeField] private Bullet bulletPrefab;

    [SerializeField] private AudioClip fireAudio;
    [SerializeField] private AudioClip stepAudio;
    [SerializeField] private AudioSource audioStepSource;
    [SerializeField] private AudioSource audioShootSource;

    [SerializeField] private TextMeshProUGUI textUp;
    [SerializeField] private CinemachineVirtualCamera cameraJogador;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //cameraJogador.Priority = 9;
        //cameraJogador.GetCinemachineComponent<CinemachineTransposer>().m_XDamping = 10;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 velocity = Movement();

        if (Input.GetButtonDown("Fire1") && !onAir && Mathf.Abs(velocity.x) <= 0.1f && !EventSystem.current.IsPointerOverGameObject())
        {
            Shoot(velocity);
        }
    }

    private void Shoot(Vector3 velocity)
    {
        contador++;

        if (velocity.x != 0) velocity.x = 0;
        anim.SetTrigger("Shoot");
        Instantiate(bulletPrefab, transform);

        textUp.text = "Quantidade de tiros: " + contador;

        audioShootSource.PlayOneShot(fireAudio);
    }

    private Vector3 Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horizontalInput * speed, rb.velocity.y);

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

        if (horizontalInput != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(horizontalInput), 1, 1);

            if (!onAir)
            {
                if (!audioStepSource.isPlaying)
                {
                    audioStepSource.clip = stepAudio;
                    audioStepSource.Play();
                }
            }
            else
            {
                if (audioStepSource.isPlaying) audioStepSource.Stop();
            }
        }

        if (Input.GetButtonDown("Jump") && !onAir)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            anim.SetTrigger("Jump");
        }

        return velocity;
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
