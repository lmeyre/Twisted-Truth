using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaEndStrike1 : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {

    // }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Attacking", false);
        animator.GetComponent<BoxCollider2D>().enabled = true;
        animator.transform.position =  animator.GetComponent<Katana>().attackKatanaPos.position;
        SoundManager.instance.PlaySound(SoundManager.instance.playerAttack1);
        // Debug.Log("augmenting");
        // float reverse = 1f;
        // if (animator.GetComponent<SpriteRenderer>().flipX)
        //     reverse = -1f;
        // animator.transform.position = new Vector2(animator.transform.position.x + Globals.katanaOffset * reverse, animator.transform.position.y);
    }

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
