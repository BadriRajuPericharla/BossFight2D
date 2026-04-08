using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    public Slider slider;
    [SerializeField]private PlayerHealth playerHealth;
    [SerializeField]private SpriteRenderer playerSpriteRenderer;
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerHealth.TakeDamage(10);
            slider.value-=10f;
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
