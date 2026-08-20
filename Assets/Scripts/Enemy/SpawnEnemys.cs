using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    [SerializeField]private EnemyController enemyController;
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

        if (AreAllChildEnemiesDead())
        {
            enemyController.enabled = true;
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
