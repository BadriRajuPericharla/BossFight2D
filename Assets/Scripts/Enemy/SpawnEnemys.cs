using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    [SerializeField]private EnemyController enemyController;
    [SerializeField]private EnemyHealth enemyHealth;
    [SerializeField]private ChildEnemyHealth[] childEnemyHealth;
    public static SpawnEnemys instance;
    void Awake()
    {
        if (instance == null)
        {
            instance=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [SerializeField]private GameObject[] childEnemyPrefab;
    public int enemyCount=2;
    public void SpawnChildEnemy()
    {
        foreach(GameObject enemy in childEnemyPrefab)
        {
            
            Vector3 pos=enemy.transform.position;
            pos.x=enemyController.gameObject.transform.position.x;
            enemy.transform.position=pos;
            enemy.SetActive(true);
        }
        
        enemyController.enabled=false;
    }
    public void ChildEnemyDied(GameObject deadEnemy)
    {
        deadEnemy.SetActive(false);
        deadEnemy.transform.rotation = enemyController.gameObject.transform.rotation;
        if (AreAllChildEnemiesDead())
        {
            enemyHealth.enemyShield.SetActive(false);
            enemyHealth.enemyShieldActive=false;
            enemyController.enabled = true;
            foreach(ChildEnemyHealth childEnemyHealth in childEnemyHealth)
            {
                childEnemyHealth.CurrentHealth=childEnemyHealth.MaxHealth;
                childEnemyHealth.slider.value=childEnemyHealth.CurrentHealth;
                childEnemyHealth.FillArea.SetActive(true);
            }
                
        }
    }

    private bool AreAllChildEnemiesDead()
    {
        foreach (GameObject enemy in childEnemyPrefab)
        {
            if (enemy.activeInHierarchy)
            {
                return false;
            }
        }
        
        return true;
    }

}
