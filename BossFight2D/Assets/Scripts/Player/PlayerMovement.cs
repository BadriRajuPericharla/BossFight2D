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
    [SerializeField]private GameObject HitPoint;
    [SerializeField]private Animator animator;
    Rigidbody2D Rb;
    SpriteRenderer Sr;
    public float MoveInput;
    public bool jumpPressed;
    public bool knifeAttack;
   
    
    void Start()
    {
        Rb=GetComponent<Rigidbody2D>();
        Sr=GetComponent<SpriteRenderer>();
        // Rb.freezeRotation=true;
    }

    
    void Update()
    {
        
       float keyboardInput = Input.GetAxis("Horizontal");
        if (keyboardInput != 0)
        {
            MoveInput = keyboardInput;
        }
       Rb.velocity=new Vector2(MoveInput*Speed,Rb.velocity.y);
        if ((Input.GetKeyDown(KeyCode.UpArrow)||jumpPressed)&& JumpCount<2)
        {
            Rb.velocity=new Vector2(Rb.velocity.x,JumpForce);
           
            animator.SetBool("IsJump",true);
            JumpCount+=1;
            jumpPressed=false;
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
            knifeAttack=true;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            knifeAttack=false;
        }

        if (knifeAttack)
        {
            animator.SetBool("IsAttack",true);
            HitPoint.SetActive(true);
        }
        if(!knifeAttack)
        {
            animator.SetBool("IsAttack",false);
            HitPoint.SetActive(false);
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
    }
    public void leftmove()
    {
        MoveInput=-1;
    }
    public void rightmove()
    {
        MoveInput=1;
    }
    public void stopmove()
    {
        MoveInput=0;
    }
    public void jump()
    {
        jumpPressed=true;
    }
    public void attckKnife()
    {
        knifeAttack=true;
    }
    public void stopKnife()
    {
        knifeAttack=false;
    }

}
