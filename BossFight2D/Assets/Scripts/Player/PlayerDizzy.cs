using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDizzy : MonoBehaviour
{
    Animator Anim;
    PlayerMovement move;
    public EnemyController enemyController;
    public Animator EnemyAnimator;
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
        enemyController.enabled=true;
        EnemyAnimator.SetBool("SpecialAttack",false);
        yield return new WaitForSeconds(3);
        move.enabled=true;
        Anim.SetBool("IsDizzy",false);
        
        
    }
}
