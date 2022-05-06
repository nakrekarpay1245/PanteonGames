using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;

    public static PlayerAnimation playerAnimation;
    private void Awake()
    {
        if(playerAnimation == null)
        {
            playerAnimation = this;
        }
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        //Debug.Log("Move : " + PlayerMovement.playerMovement.IsPlayerCanMove());
        //Debug.Log("Fall : " + PlayerMovement.playerMovement.IsPlayerFall());
        animator.SetBool("isMove", PlayerMovement.playerMovement.IsPlayerCanMove());
        animator.SetBool("isFall", PlayerMovement.playerMovement.IsPlayerFall());
    }
}
