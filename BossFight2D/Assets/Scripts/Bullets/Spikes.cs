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
            Animator Player=collision.GetComponent<Animator>();
            PlayerMovement Playermov=collision.GetComponent<PlayerMovement>();
            StartCoroutine(Dizzy(Player,Playermov));
            gameObject.SetActive(false);
        }
        if (collision.gameObject.tag == "End")
        {
            gameObject.SetActive(false);
        }
    }
    IEnumerator Dizzy(Animator Anim,PlayerMovement move)
    {
        Anim.SetBool("IsDizzy",true);
        move.enabled=false;
        yield return new WaitForSeconds(4);
        Anim.SetBool("IsDizzy",false);
        move.enabled=true;
        
    }
}
