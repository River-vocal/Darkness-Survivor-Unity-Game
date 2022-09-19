using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    Transform playerTransform;
    Transform bossTransform;
    Rigidbody2D rb2;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float attackRange = 2.5f;
    public bool bossIsFlipped = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        bossTransform = animator.GetComponent<Transform>();
        rb2 = animator.GetComponent<Rigidbody2D>();
    }

    private void lookAtPlayer()
    {
        Vector3 flipped = bossTransform.localScale;
        flipped.z *= -1;
        if (bossTransform.position.x > playerTransform.position.x && bossIsFlipped)
        {
            bossTransform.localScale = flipped;
            bossTransform.Rotate(0, 180, 0);
            bossIsFlipped = false;
        }
        else if (bossTransform.position.x < playerTransform.position.x && !bossIsFlipped)
        {
            bossTransform.localScale = flipped;
            bossTransform.Rotate(0, 180, 0);
            bossIsFlipped = true;
        }
    }
    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        lookAtPlayer();
        if (Vector2.Distance(playerTransform.position, rb2.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
            return;
        }
        Vector2 targetPosition = new Vector2(playerTransform.position.x, rb2.position.y);
        Vector2 newPosition = Vector2.MoveTowards(rb2.position, targetPosition, speed * Time.deltaTime);
        rb2.MovePosition(newPosition);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
