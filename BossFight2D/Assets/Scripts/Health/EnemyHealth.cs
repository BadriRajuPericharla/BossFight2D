using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public static float MaxHealth=1000f;
    [SerializeField]private GameObject FillArea;
    [SerializeField]private Animator EnemyAnimator;
    [SerializeField]private Animator PlayerAnimator;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private GameObject LevelComplete;
    [SerializeField]private SpawnParticles spawnParticles;
    public float CurrentHealth;
    public int damageCounter;
    bool isSpecialAttacking=false;
    void Start()
    {
        CurrentHealth=MaxHealth;
    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth-=Damage;
        CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
        damageCounter+=Damage;
        if(damageCounter >= 250&&!isSpecialAttacking)
        {
            damageCounter-=250;
            StartCoroutine(SpecialAttack());
        }
        if (CurrentHealth <= 0)
        {
            FillArea.SetActive(false);
            StartCoroutine(Die());
        }

    }
    IEnumerator Die()
    {
        EnemyAnimator.SetBool("IsDie",true);
        PlayerAnimator.SetBool("IsWin",true);
        playerMovement.enabled=false;
        LevelComplete.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    IEnumerator SpecialAttack()
    {
        isSpecialAttacking=true;
        EnemyAnimator.SetBool("SpecialAttack",true);
        spawnParticles.SpawnSpikes();
        enemyController.enabled=false;
        yield return new WaitForSeconds(3f);
        EnemyAnimator.SetBool("SpecialAttack",false);
        isSpecialAttacking=false;
        enemyController.enabled=true;
    }
}
