using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderController : MonoBehaviour
{
    // All sprites
    public SpriteRenderer[] allSprites;

    // Faces
    public SpriteRenderer headRenderer;
    public Sprite okaySprite;
    public Sprite damageSprite;
    public Sprite deadSprite;

    // Animations
    public Animator animator;
    private InfiniteRunBehaviour infiniteRun;

    // Glasses
    public SpriteRenderer glassesRenderer;
    public Sprite glassesOn;
    public Sprite glassesOff;

    // Damage
    public float lowRedValue;
    public float highRedValue;
    public float blinkInterval;
    private bool isBlinkingHigh;
    private bool receiveingDamage;

    void Awake()
    {
        infiniteRun = GetComponent<InfiniteRunBehaviour>();

        animator.Play("Run");

        EventManager.StartListening("Jump", OnJump);
        EventManager.StartListening("Land", OnLand);

        EventManager.StartListening("GlassesOn", OnGlassesOn);
        EventManager.StartListening("GlassesOff", OnGlassesOff);

        EventManager.StartListening("DamageStart", OnDamageStart);
        EventManager.StartListening("DamageEnd", OnDamageEnd);

        StartCoroutine(RedBlink());
        Init();
    }

    private void Init()
    {
        isBlinkingHigh = receiveingDamage = false;
    }

    private void Update()
    {
        animator.speed = infiniteRun.currentSpeed / 3;
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

    private void OnDamageStart()
    {
        receiveingDamage = true;
        headRenderer.sprite = damageSprite;
    }

    private void OnDamageEnd()
    {
        receiveingDamage = false;
        headRenderer.sprite = okaySprite;
    }

    private IEnumerator RedBlink()
    {
        float antiRedValue = isBlinkingHigh ? highRedValue : lowRedValue;
        isBlinkingHigh = !isBlinkingHigh;

        foreach (SpriteRenderer sprite in allSprites)
        {
            if (receiveingDamage)
            {
                sprite.color = new Color(1, 1 - antiRedValue, 1 - antiRedValue);
            }
            else
            {
                sprite.color = Color.white;
            }
        }

        yield return new WaitForSeconds(blinkInterval);
        StartCoroutine(RedBlink());
    }
}
