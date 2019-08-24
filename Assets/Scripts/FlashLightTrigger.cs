using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightTrigger : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        while (animator.GetComponent<PolygonCollider2D>() != null)
            Destroy(animator.GetComponent<PolygonCollider2D>());
        animator.gameObject.AddComponent(typeof(PolygonCollider2D));

        FlashLight flashLight = animator.GetComponent<FlashLight>();
        flashLight.mask.sprite = flashLight.focusedLight;
        flashLight.mask.alphaCutoff = 0.000f;
        flashLight.StartCoroutine("ConcentrateLight");
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
