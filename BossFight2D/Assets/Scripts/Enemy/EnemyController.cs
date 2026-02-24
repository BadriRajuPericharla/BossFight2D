using UnityEngine;

public class EnemyController:MonoBehaviour
{
    public float speed = 3f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player != null)
        {
            float direction = Mathf.Sign(player.position.x - transform.position.x);
            transform.position += Vector3.right * direction * speed * Time.deltaTime;
        }
    }
}