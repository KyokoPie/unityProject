using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Rush : StateMachineBehaviour
{
    public float speed = 5f;
    public float attackRange = 5f;
    GameObject rushAttack_L;
    GameObject rushAttack_R;

    GameObject targetPos;
    Transform player;
    Rigidbody2D rb;
    Boss_Controller bossController;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<Boss_Controller>();
        rushAttack_L = rb.GetComponent<Boss_Controller>().RushDistance_L;
        rushAttack_R = rb.GetComponent<Boss_Controller>().RushDistance_R;

        if (player.position.x > rb.position.x)
        {
            targetPos = rushAttack_R;
        }else if(player.position.x < rb.position.x)
        {
            targetPos = rushAttack_L;
        }
        bossController.LookAtPlayer();
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (targetPos == rushAttack_L)
        {
            Vector2 target = new Vector2(targetPos.transform.position.x - 1, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (rb.position.x <= targetPos.transform.position.x)
            {
                animator.SetBool("isRun", false);
            }
            
        }
        if (targetPos == rushAttack_R)
        {
            Vector2 target = new Vector2(targetPos.transform.position.x + 1, rb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            if (rb.position.x >= targetPos.transform.position.x)
            {
                animator.SetBool("isRun", false);
            }
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.SetBool("isRun", false);
    }
}
