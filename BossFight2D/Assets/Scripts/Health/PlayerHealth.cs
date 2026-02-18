using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float MaxHealth=100f;
    public float CurrentHealth;
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
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
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
