﻿using System.Collections;
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
        // foreach(PolygonCollider2D col in animator.GetComponents<PolygonCollider2D>())
        //     Destroy(col);
        // animator.gameObject.AddComponent(typeof(PolygonCollider2D));
        // animator.GetComponent<PolygonCollider2D>().isTrigger = true;
        
        FlashLight flashLight = animator.GetComponent<FlashLight>();
        flashLight.baseCol.enabled = false;
        flashLight.focusCol.enabled = true;

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
