using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float Speed=10f;
    [SerializeField]float JumpForce=5f;
    [SerializeField]int JumpCount=0;
    Rigidbody2D Rb;
    SpriteRenderer Sr;
    Animator animator;
   
    
    void Start()
    {
        Rb=GetComponent<Rigidbody2D>();
        Sr=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        Rb.freezeRotation=true;
    }

    
    void Update()
    {
       float MoveInput=Input.GetAxis("Horizontal");
       Rb.velocity=new Vector2(MoveInput*Speed,Rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.UpArrow)&& JumpCount<=3)
        {
            Rb.velocity=new Vector2(Rb.velocity.x,JumpForce);
           
            animator.SetBool("IsJump",true);
            JumpCount+=1;
        }
        if (MoveInput!=0)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        if (MoveInput > 0)
        {
            Sr.flipX=false;
            
        }
        if (MoveInput < 0)
        {
            Sr.flipX=true;
            
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount=0;
            animator.SetBool("IsJump",false);
         
           
        }
        if (collision.gameObject.tag == "Enemy")
        {
            GetComponent<PlayerHealth>().TakeDamage(10);
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30);
        }
    }

}
