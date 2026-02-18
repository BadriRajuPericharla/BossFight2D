using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]float speed =3f;
    private Transform Player;
    void Start()
    {
        Player=GameObject.FindGameObjectWithTag("Player").transform;
    }

   
    void Update()
    {
        if(Player !=null){
        if (Player.position.x > transform.position.x)
        {
            transform.position += Vector3.right*speed*Time.deltaTime;
        }
        if (Player.position.x < transform.position.x)
        {
            transform.position += Vector3.left*speed*Time.deltaTime;
        }
        }
        
    }
}
