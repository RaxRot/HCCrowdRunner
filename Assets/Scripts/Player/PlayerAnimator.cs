using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform runnersParent;
    
    public void Run()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Animator>();
            
            animator.Play(TagManager.PLAYER_RUN_ANIM_NAME);
        }
    }

    public void Idle()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Transform runner = runnersParent.GetChild(i);
            Animator animator = runner.GetComponent<Animator>();
            
            animator.Play(TagManager.PLAYER_IDLE_ANIM_NAME);
        }
    }
}
