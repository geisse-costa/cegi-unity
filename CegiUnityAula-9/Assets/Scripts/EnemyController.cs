using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float attackDistance;

    private int direction = 1;
    private float time;
    private bool attacked;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>(); //GameObject.Find("NomeObjeto").GetComponent<Objeto>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacked)
        {
            if (Time.time - time < attackCooldown) return;

            time = Time.time;
            attacked = false;
        }

        if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
        {
            attacked = true;
            //Animação ataque
            //Player.GetAttacked();
            print("Animação Atacar");
            return;
        }
        //Invoke("funcaop", 5);
        //StartCoroutime(waitTime());
        transform.Translate(Vector3.right * direction * speed * Time.deltaTime);
    }

    private IEnumerator waitTime()
    {
        yield return new WaitForSeconds(time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limite"))
        {
            ChangeDirection();
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            GetAttacked();
        }
    }

    private void GetAttacked()
    {
        health -= 10;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeDirection()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction, transform.localScale.y, transform.localScale.z);
    }
}
