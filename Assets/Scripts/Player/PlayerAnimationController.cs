using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator.Play("Run");

        EventManager.StartListening("Jump", OnJump);
        EventManager.StartListening("Land", OnLand);
    }

    private void Update()
    {
        animator.speed = PlayerData.instance.currentSpeed / 3;
    }

    private void OnJump()
    {
        animator.Play("Jump");
    }

    private void OnLand()
    {
        animator.Play("Run");
    }
}
