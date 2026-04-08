using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]private AudioManager audioManager;
    public void Attack()
    {
        audioManager.EnemySwordAttack();
    }
}
