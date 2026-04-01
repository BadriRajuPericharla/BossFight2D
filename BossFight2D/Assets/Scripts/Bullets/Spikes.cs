using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Spikes : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerDizzy>().StartDizzy();
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "End")
        {
            gameObject.SetActive(false);
        }
    }
    
}
