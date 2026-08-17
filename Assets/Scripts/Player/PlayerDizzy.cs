using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDizzy : MonoBehaviour
{
    Animator Anim;
    PlayerMovement move;
    PlayerShoot playerShoot;
    public EnemyController enemyController;
    public Animator EnemyAnimator;
    [SerializeField]private AudioManager audioManager;
    [SerializeField]private ParticleSystem hitParticleSystem;

    void Start()
    {
        Anim=GetComponent<Animator>();
        move=GetComponent<PlayerMovement>();
        playerShoot=GetComponent<PlayerShoot>();
    }
    public void StartDizzy()
    {
        hitParticleSystem.gameObject.SetActive(true);
        hitParticleSystem.Play();
        StartCoroutine(Dizzy());
    }
    
    IEnumerator Dizzy()
    {
        Anim.SetBool("IsDizzy",true);
        audioManager.PlayerDizzy();
        move.enabled=false;
        playerShoot.enabled=false;
        enemyController.enabled=true;
        EnemyAnimator.SetBool("SpecialAttack",false);
        yield return new WaitForSeconds(3);
        move.enabled=true;
        playerShoot.enabled=true;
        Anim.SetBool("IsDizzy",false); 
    }
}
