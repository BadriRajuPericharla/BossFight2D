using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]float Speed=10f;
    [SerializeField]float JumpForce=5f;
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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Rb.velocity=new Vector2(Rb.velocity.x,JumpForce);
        }
    }
    
}
