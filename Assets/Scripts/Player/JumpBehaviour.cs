﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehaviour : MonoBehaviour
{
    public float jumpLength;
    public float jumpHeight;

    private Rigidbody2D rigidbody2D;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();

        EventManager.StartListening("InputJump", Jump);
    }

    private void Update()
    {
        float airTime = jumpLength / PlayerData.instance.currentSpeed;

        rigidbody2D.gravityScale = (float)(jumpHeight / Math.Pow(airTime / 2.0f, 2.0f)); ;
    }

    void Jump()
    {
        float verticalVelcocity = (float)Math.Sqrt(2.0f * rigidbody2D.gravityScale * jumpHeight);

        // TODO fix
        if (/*(onGround || onExtraTimeToJump) && jumpEnabled*/ playerController.grounded)
        {
            EventManager.TriggerEvent("Jump");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalVelcocity);
        }
    }

    /*private IEnumerator PreventMultiJump()
    {
        jumpEnabled = false;
        yield return new WaitForSeconds(extraTimeToJump + 0.05f);
        jumpEnabled = true;
    }

    private IEnumerator AllowExtraTimeJump()
    {
        onExtraTimeToJump = true;
        yield return new WaitForSeconds(extraTimeToJump);
        onExtraTimeToJump = false;
    }*/
}
