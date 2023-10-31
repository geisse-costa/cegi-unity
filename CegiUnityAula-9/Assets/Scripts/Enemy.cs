using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private float speed;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float minDistance;

    private int direction = 1;
    private float time;
    private bool attacked;
    private PlayerMovement player;

    private void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (attacked)
        {
            if (Time.time - time < attackCooldown) return;

            time = Time.time;
            attacked = false;
        }

        if (distance <= minDistance)
        {
            if (Vector3.right * direction != player.transform.right)
            {
                print("Animação Atacar");
                attacked = true;
                //Animação Ataque
                //player.GetAttacked();
                return;
            }
        }

        transform.Translate(Vector3.right * direction *  Time.deltaTime * speed);
        //Animação movimento
    }

    private void ChangeDirection()
    {
        direction *= -1;
        transform.localScale = new Vector3(direction, 1, 1);
    }

    private void GetAttacked()
    {
        health -= 10;
        if (health < 0)
        {
            Destroy(gameObject);
        }
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
}
