using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]private float Speed=20f;
    private EnemyDamage enemyDamage;
    Rigidbody2D rb;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=transform.right*Speed;
        enemyDamage=GameObject.FindGameObjectWithTag("HitPoint").GetComponent<EnemyDamage>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(20);
            
            enemyDamage.slider.value-=20f;
            

        }
        if (collision.gameObject.tag == "End")
        {
            Destroy(gameObject);
        }
    }



}
