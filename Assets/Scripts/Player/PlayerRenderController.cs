using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderController : MonoBehaviour
{
    // Animations
    public Animator animator;

    // Glasses sprite
    public SpriteRenderer glassesRenderer;
    public Sprite glassesOn;
    public Sprite glassesOff;

    void Start()
    {
        animator.Play("Run");

        EventManager.StartListening("Jump", OnJump);
        EventManager.StartListening("Land", OnLand);

        EventManager.StartListening("GlassesOn", OnGlassesOn);
        EventManager.StartListening("GlassesOff", OnGlassesOff);
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

    private void OnGlassesOn()
    {
        glassesRenderer.sprite = glassesOn;
    }

    private void OnGlassesOff()
    {
        glassesRenderer.sprite = glassesOff;
    }
}
