﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    Animator playerAnimator;
    SpriteRenderer sprite;
    bool rightFacing = true;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        playerAnimator.SetBool("isWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        //walking left/right movement
        float horz = Input.GetAxis("Horizontal");
        if (horz != 0)
        {
            playerAnimator.SetBool("isWalking", true);
            if (rightFacing && horz < 0)
            {
                sprite.flipX = true;
                rightFacing = false;
            } else if (!rightFacing && horz > 0)
            {
                sprite.flipX = false;
                rightFacing = true;
            }
        } else
        {
            playerAnimator.SetBool("isWalking", false);
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
            && GetComponent<MovePlayer>().isGrounded)
        {
            playerAnimator.SetBool("isJumping", true);
        }

        if ((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))) { 
            playerAnimator.SetBool("isJumping", false);
        }
    }
}
