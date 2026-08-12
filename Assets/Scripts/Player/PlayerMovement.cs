using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float Speed=10f;
    [SerializeField]float JumpForce=8f;
    [SerializeField]int JumpCount=0;
    [SerializeField]private GameObject FirePoint;
    [SerializeField]private Animator animator;
    [SerializeField]private AudioManager audioManager;
    public bool canAttack=false;
    Rigidbody2D Rb;
    public float MoveInput;
    public bool knifeAttack;
   
    
    void Awake()
    {
        Rb=GetComponent<Rigidbody2D>();
        
       
    }
    public void Attack()
    {
        audioManager.SwordAttack();
    }
    
    void Update()
    {
        
       float keyboardInput = Input.GetAxis("Horizontal");
        if (keyboardInput != 0)
        {
            MoveInput = keyboardInput;
        }
        Rb.velocity=new Vector2(MoveInput*Speed,Rb.velocity.y);
        if ((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.Space)) && JumpCount<2)
        {
            audioManager.JumpSound();
            Rb.velocity=new Vector2(Rb.velocity.x,JumpForce);
            animator.SetBool("IsJump",true);
            JumpCount+=1;
            
        }
        if (Mathf.Abs(MoveInput) > 0.1f)
        {
            animator.SetBool("IsRun", true);
        }
        else
        {
            animator.SetBool("IsRun", false);
        }

        if (canAttack)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                knifeAttack=true;
                
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                knifeAttack=false;
            }
        }
        

        if (knifeAttack)
        {
            animator.SetBool("IsAttack",true);
        }
        if (!knifeAttack)
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
        if (collision.gameObject.tag == "Ground" && Rb.velocity.y<=0)
        {
            JumpCount=0;
            animator.SetBool("IsJump",false);  
        }
    }
    public void JumpFromMobile()
    {
        if (JumpCount < 2)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, JumpForce);
            animator.SetBool("IsJump", true);
            JumpCount++;
        }
    }

}
