
using UnityEngine;


public class ChildEnemyDamage : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ChildEnemy")
        {
            collision.gameObject.GetComponent<ChildEnemyHealth>().TakeDamage(30);
        }
    }
    
    
    
}
