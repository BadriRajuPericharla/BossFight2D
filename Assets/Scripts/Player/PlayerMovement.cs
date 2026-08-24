using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float Speed=10f;
    [SerializeField]float JumpForce=15f;
    [SerializeField]private float Gravity=20f;
    [SerializeField]private float fallGravity=35f;
    [SerializeField]int JumpCount=0;
    [SerializeField]private GameObject FirePoint;
    [SerializeField]private Animator animator;
    [SerializeField]private AudioManager audioManager;
    public bool canAttack=false;
    public bool isjumping=false;
    Rigidbody2D Rb;
    public float MoveInput;
    public bool knifeAttack=false;
    private float verticalVelocity;
    
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
        if ((Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.Space)) && JumpCount<2)
        {
            audioManager.JumpSound();
            isjumping=true;
            verticalVelocity = JumpForce;

            animator.SetBool("IsJump", true);

            JumpCount++;
            
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
    void FixedUpdate()
    {

        float horizontalMovement = MoveInput * Speed;
        if (verticalVelocity > 0)
        {
            verticalVelocity -= Gravity * Time.fixedDeltaTime;
        }
        else
        {
            verticalVelocity-=fallGravity*Time.fixedDeltaTime;
        }

        Vector2 movement = new Vector2(horizontalMovement,verticalVelocity);

  
        Rb.MovePosition(Rb.position + movement * Time.fixedDeltaTime);

  
        animator.SetBool("IsRun", Mathf.Abs(MoveInput) > 0.1f);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isjumping=false;
            JumpCount=0;
            animator.SetBool("IsJump",false);  
        }
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "ChildEnemy")
        {
            isjumping=false;
            animator.SetBool("IsJump",false);
        }
    }
    public void JumpFromMobile()
    {
        if (JumpCount < 2)
        {
            verticalVelocity = JumpForce;
            isjumping=true;
            animator.SetBool("IsJump", true);

            JumpCount++;
        }
    }

}
