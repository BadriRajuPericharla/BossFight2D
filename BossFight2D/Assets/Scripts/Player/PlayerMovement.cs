using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float Speed=10f;
    [SerializeField]float JumpForce=5f;
    [SerializeField]int JumpCount=0;
    Rigidbody2D Rb;
   
    
    void Start()
    {
        Rb=GetComponent<Rigidbody2D>();
        Rb.freezeRotation=true;
    }

    
    void Update()
    {
       float MoveInput=Input.GetAxis("Horizontal");
       Rb.velocity=new Vector2(MoveInput*Speed,Rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.UpArrow)&& JumpCount<=3)
        {
            Rb.velocity=new Vector2(Rb.velocity.x,JumpForce);
            JumpCount+=1;
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount=0;
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<PlayerHealth>().TakeDamage(10);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30);
        }
    }

}
