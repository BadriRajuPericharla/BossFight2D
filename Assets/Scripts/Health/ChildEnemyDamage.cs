using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChildEnemyDamage : MonoBehaviour
{
    [SerializeField]private ChildEnemyHealth enemyHealth;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ChildEnemy")
        {
            enemyHealth.TakeDamage(30);
        }
    }
    
    
    
}
