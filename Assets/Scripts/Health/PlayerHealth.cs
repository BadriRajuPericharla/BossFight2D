using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth=200f;
    public float CurrentHealth;
    [SerializeField]private GameObject FillArea;
    [SerializeField]private Animator PlayerAnimator;
    [SerializeField]private Animator enemyAnimator;
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private PlayerMovement playerMovement;
    [SerializeField]private AudioManager audioManager;
    [SerializeField]private UI uI;
    [SerializeField]private Slider slider;
    PlayerDizzy playerDizzy;
    bool IsDead=false;

    void Start()
    {
        CurrentHealth=MaxHealth;
        playerDizzy=GetComponent<PlayerDizzy>();
    }
   

    public void TakeDamage(int Damage)
    {
        if(IsDead) return;
        CurrentHealth-=Damage;
        
        CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
        slider.value=CurrentHealth;
        Debug.Log("Damage");
        if (CurrentHealth <= 0)
        {
            IsDead=true;
            FillArea.SetActive(false);
            StartCoroutine(Die());
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
        Destroy(gameObject);
        uI.ShowGameOver();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Health")
        {
            CurrentHealth=MaxHealth;
            collision.gameObject.SetActive(false);
        }
    }
}
