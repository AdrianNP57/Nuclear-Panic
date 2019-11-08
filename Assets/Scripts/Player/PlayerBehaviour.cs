using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator playerAnimator;

    public bool mIsFixedJump;
    public bool mIncreasedRampSpeed;

    private Rigidbody2D mRigidBody2D;
    private List<Collision2D> mAllCollisions; 

    // V2 jump
    public float mJumpLength;
    public float mJumpHeight;

    //level generation
    public List<GameObject> mLevels;
    public List<GameObject> mBufferLevels;

    private bool onGround = true;
    private bool onExtraTimeToJump = false;
    private bool jumpEnabled = true;
    private bool inJump = false;
    public bool isDead { get; private set; }
    public float extraTimeToJump;

    public float initialSpeedRun;
    public float maxSpeedRun;
    public float speedRunAccelaration;

    public GameObject difficultyPanel;

    [HideInInspector]
    public float currentSpeedRun;
    [HideInInspector]
    public Vector3 initialPosition;

    private AudioEffectPlayer fxPlayer;
    private MusicManager musicManager;

    private bool allowInteraction;

    private bool playLanding;

    public SpriteRenderer[] playerSprites;
    public float lowRedValue;
    public float highRedValue;
    public float blinkInterval;
    public bool receiveingDamage = false;
    private bool isBlinkingHigh = false;

    public Score score;
    public HighScoreBehaviour highScoreBehaviour;

    public SpriteRenderer headRenderer;
    public Sprite damageSprite;
    public Sprite okaySprite;
    public Sprite deadSprite;


    // Start is called before the first frame update
    void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mAllCollisions = new List<Collision2D>();
        mBufferLevels = new List<GameObject>();
        initialPosition = transform.position;
        fxPlayer = Camera.main.GetComponent<AudioEffectPlayer>();
        musicManager = Camera.main.GetComponent<MusicManager>();

        Init();
    }

    public void Init()
    {
        currentSpeedRun = initialSpeedRun;
        transform.position = initialPosition;

        onGround = true;
        onExtraTimeToJump = false;
        jumpEnabled = true;
        inJump = false;
        isDead = false;
        receiveingDamage = false;
        fxPlayer.SetEnabled(true);
        //musicManager.InitMusic();

        difficultyPanel.SetActive(true);
        StartCoroutine(PreventPrematureInteraction());

        playerAnimator.Play("Run");
        mAllCollisions.Clear();
    }

    public void Die()
    {
        if (isDead)
        {
            return;
        }

        isDead = true;
        headRenderer.sprite = deadSprite;
        playerAnimator.Play("Die");
        mRigidBody2D.velocity = Vector2.zero;
        fxPlayer.SetEnabled(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            return;
        }

        if(allowInteraction)
        {
            /*if (!chooseDifficulty)
            {
                CheckLanding();
                CheckFallingIntoVoid();

                // TODO move to different method
                if (mRigidBody2D.velocity.y > 0.01f && !inJump)
                {
                    if (!onGround)
                    {
                        Vector2 vel = mRigidBody2D.velocity;
                        vel.y = 0;
                        mRigidBody2D.velocity = vel;
                    }
                    else if (mIncreasedRampSpeed)
                    {
                        mRigidBody2D.velocity = new Vector2(currentSpeedRun * 1.175f, mRigidBody2D.velocity.y);
                    }

                }
            }*/

            //CheckUIEvents();
        }
    }

    /*private void CheckFallingIntoVoid()
    {
        if (gameObject.transform.position.y < -10.0f)
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lethalRadiation = true; //Collision with gamma
        }
    }*/

    private IEnumerator PreventPrematureInteraction()
    {
        allowInteraction = false;
        yield return new WaitForSeconds(0.5f);
        allowInteraction = true;
    }

    private IEnumerator PreventLandingSoundForShortAirTime()
    {
        playLanding = false;
        yield return new WaitForSeconds(0.1f);

        playLanding = true;
    }
}
