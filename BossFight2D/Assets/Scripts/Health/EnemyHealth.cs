using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth=1000f;
    [SerializeField]private GameObject FillArea;
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
            Die();
        }

    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
