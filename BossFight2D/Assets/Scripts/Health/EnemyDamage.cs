using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]private EnemyHealth enemyHealth;
    [SerializeField]private GameObject Player;
    public Slider slider;
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemyHealth.TakeDamage(30);
            slider.value-=30f;
           
        }
    }
}
