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
    [SerializeField]private GameObject FirePoint;
    Rigidbody2D Rb;
    SpriteRenderer Sr;
    Animator animator;
   
    
    void Start()
    {
        Rb=GetComponent<Rigidbody2D>();
        Sr=GetComponent<SpriteRenderer>();
        animator=GetComponent<Animator>();
        // Rb.freezeRotation=true;
    }

    
    void Update()
    {
       float MoveInput=Input.GetAxis("Horizontal");
       Rb.velocity=new Vector2(MoveInput*Speed,Rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.UpArrow)&& JumpCount<2)
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            animator.SetBool("IsAttack",true);
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("IsAttack",false);
        }

        if (MoveInput > 0)
        {
            transform.localScale= new Vector3(2.5f,2.5f,2.5f);
            FirePoint.transform.localRotation=Quaternion.Euler(0,0,0);
        }
        else if (MoveInput < 0)
        {
            transform.localScale= new Vector3(-2.5f,2.5f,2.5f);
            FirePoint.transform.localRotation=Quaternion.Euler(0,180,0);
        }
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount=0;
            animator.SetBool("IsJump",false);  
        }
        
        if (collision.gameObject.tag == "Enemy"&&Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("collided");
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(30);
        }
    }

}
