using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
   [SerializeField]private int ParticleCount=12;
   [SerializeField]private float Speed=5f;
   [SerializeField]private GameObject PaticlePrefab;
   [SerializeField]private int poolSize=20;
   private List<GameObject>Pool=new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject spike=Instantiate(PaticlePrefab);
            spike.SetActive(false);
            Pool.Add(spike);
        }
    }
    GameObject GetSpike()
    {
        for(int i = 0; i < Pool.Count; i++)
        {
            if (!Pool[i].activeInHierarchy)
            {
                Pool[i].SetActive(true);
                return Pool[i];
            }
        }
        GameObject spike=Instantiate(PaticlePrefab);
        Pool.Add(spike);
        return spike;
    }
    public void SpawnSpikes()
    {
        float angleStep=360f/ParticleCount;
        float angle=0f;
        for(int i = 0; i < ParticleCount; i++)
        {
            float x=Mathf.Cos(angle*Mathf.Deg2Rad);
            float y = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 direction = new Vector2(x, y);

            GameObject spike = GetSpike();
            spike.transform.position = transform.position;

            Rigidbody2D rb = spike.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.velocity = direction * Speed;

            angle += angleStep;
        }
    }
}
