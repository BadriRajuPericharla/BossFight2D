using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth=100f;
    public float CurrentHealth;
    [SerializeField]private GameObject FillArea;
    [SerializeField]private GameObject GameOver;
    [SerializeField]private Animator PlayerAnimator;
    [SerializeField]private Animator enemyAnimator;
    [SerializeField]private EnemyController enemyController;

    void Start()
    {
        CurrentHealth=MaxHealth;
    }
   

    public void TakeDamage(int Damage)
    {
        CurrentHealth-=Damage;
        CurrentHealth=Mathf.Clamp(CurrentHealth,0,MaxHealth);
        Debug.Log("Damage");
        if (CurrentHealth <= 0)
        {
            FillArea.SetActive(false);
            StartCoroutine(Die());
        }
    }
    IEnumerator Die()
    {
        enemyController.enabled=false;
        PlayerAnimator.SetBool("IsDie",true);
        enemyAnimator.SetBool("IsWin",true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        GameOver.SetActive(true);
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
