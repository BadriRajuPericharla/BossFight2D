using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float Speed=20f;
    Rigidbody2D rb;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*Speed;
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            if(collision.gameObject.GetComponent<EnemyHealth>().enemyShieldActive) return;
            collision.gameObject.GetComponent<EnemyHealth>().HitEffect();
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(20);
            collision.gameObject.GetComponent<EnemyHealth>().fireBallDamageCounter+=20;
        }
        if (collision.gameObject.tag == "ChildEnemy")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<ChildEnemyHealth>().HitEffect();
            collision.gameObject.GetComponent<ChildEnemyHealth>().TakeDamage(20);
        }
        if (collision.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
    }



}
