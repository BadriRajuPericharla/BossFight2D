using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDizzy : MonoBehaviour
{
    Animator Anim;
    PlayerMovement move;
    void Start()
    {
        Anim=GetComponent<Animator>();
        move=GetComponent<PlayerMovement>();
    }
    public void StartDizzy()
    {
        StartCoroutine(Dizzy());
    }
    IEnumerator Dizzy()
    {
        Anim.SetBool("IsDizzy",true);
        move.enabled=false;
        yield return new WaitForSeconds(2);
        move.enabled=true;
        Anim.SetBool("IsDizzy",false);
        
        
    }
}
