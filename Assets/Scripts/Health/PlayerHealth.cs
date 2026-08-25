using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth=200f;
    public float CurrentHealth;
    public GameObject FillArea;
    [SerializeField]private Animator PlayerAnimator;
    [SerializeField]private Animator enemyAnimator;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private AudioManager audioManager;
    [SerializeField]private UI uI;
    public Slider healthSlider;
    [SerializeField]private ModesManager modesManager;
    private PlayerDizzy playerDizzy;
    bool IsDead=false;

    void Start()
    {
        CurrentHealth=MaxHealth;
        playerDizzy=GetComponent<PlayerDizzy>();
    }
   

    public void TakeDamage(int Damage)
    {
        if(IsDead) return;
        if(PlayerShield.instance.shieldActivated) return;
        else
        {
            CurrentHealth-=Damage;
        
            CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
            healthSlider.value=CurrentHealth;
            if (CurrentHealth <= 0)
            {
                IsDead=true;
                FillArea.SetActive(false);
                StartCoroutine(Die());
            }
        }
        
    }
    IEnumerator Die()
    {
        playerMovement.enabled=false;
        audioManager.PlayerDeadSound();
        playerDizzy.enabled=false;
        enemyController.enabled=false;
        PlayerAnimator.SetBool("IsDie",true);
        enemyAnimator.SetBool("IsWin",true);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        modesManager.PlayerDied();
    }

}

