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
    [SerializeField]private GameObject LevelComplete;
    public float CurrentHealth;
    void Start()
    {
        CurrentHealth=MaxHealth;
    }
    public void TakeDamage(int Damage)
    {
        CurrentHealth-=Damage;
        CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
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
}
