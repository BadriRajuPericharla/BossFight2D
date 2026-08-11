using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed = 4f;
    [SerializeField] private Transform Player;
    [SerializeField]private Animator animator;
    [SerializeField]private float StopDistance=3.8f;

    

    

    void Update() 
    {
        if(Player==null)return;
        Vector2 Position = new Vector2(Player.position.x, transform.position.y);
       
        float distance = Vector2.Distance(transform.position, Position);
        Vector2 direction = Player.position - transform.position;
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
            
        else if(direction.x < 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }
            

        if(distance > StopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Position, Speed * Time.deltaTime);
            animator.SetBool("IsRun", true);
            animator.SetBool("IsAttack",false);
        }
        else
        {
            animator.SetBool("IsRun", false);
            animator.SetBool("IsAttack",true);
            

        }
    }
    
}