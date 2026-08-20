using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
   

    [SerializeField]private PlayerHealth playerHealth;
    [SerializeField]private SpriteRenderer playerSpriteRenderer;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !PlayerShield.instance.shieldActivated)
        {
            
            playerHealth.TakeDamage(10);
            playerSpriteRenderer.color=Color.red;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerSpriteRenderer.color=Color.white;
        }
    }


}
