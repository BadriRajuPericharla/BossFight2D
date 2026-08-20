using Unity.VisualScripting;
using UnityEngine;

public class ChildEnemyController : MonoBehaviour
{
    public float Speed = 4f;
    [SerializeField] private Transform Player;
    [SerializeField]private Animator animator;
    [SerializeField]private float StopDistance=4f;
    private bool shouldMove;
    private Vector2 targetPosition;
    Rigidbody2D Rb;


    void Start()
    {
        Rb=GetComponent<Rigidbody2D>();
    }
    void Update() 
    {
        if(Player==null)return;
        targetPosition = new Vector2(Player.position.x, Rb.position.y);
       
        float distance = Vector2.Distance(transform.position, targetPosition);
        Vector2 direction = (Vector2)Player.position - Rb.position;
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(-0.6f,0.6f,0.6f);
        }
            
        else if(direction.x < 0)
        {
            transform.localScale = new Vector3(0.6f,0.6f,0.6f);
        }
            

        if(distance > StopDistance)
        {
            shouldMove=true;
            animator.SetBool("IsRun", true);
            animator.SetBool("IsAttack",false);
        }
        else
        {
            shouldMove=false;
            animator.SetBool("IsRun", false);
            animator.SetBool("IsAttack",true);
        }
    }
    void FixedUpdate()
    {
        if (!shouldMove)
            return;

        Vector2 newPosition = Vector2.MoveTowards(Rb.position,targetPosition,Speed * Time.fixedDeltaTime);

        Rb.MovePosition(newPosition);
    }

}