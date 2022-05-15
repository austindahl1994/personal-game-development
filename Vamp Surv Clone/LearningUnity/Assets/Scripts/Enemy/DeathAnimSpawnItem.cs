using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimSpawnItem : StateMachineBehaviour
{
    //This animation script is used to destroy the prefab from the animator
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Destroy(animator.gameObject, stateInfo.length);
    }

}