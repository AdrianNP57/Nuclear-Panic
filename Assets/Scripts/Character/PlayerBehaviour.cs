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

    // V1 jump
    public float mForceJump;

    // V2 jump
    public float mJumpLength;
    public float mJumpHeight;

    //level generation
    public List<GameObject> mLevels;
    public List<GameObject> mBufferLevels;

    private bool onGround = true;
    private bool jumpEnabled = true;
    private bool inJump = false;

    public float initialSpeedRun;
    public float maxSpeedRun;
    public float speedRunAccelaration;
    [HideInInspector]
    public float currentSpeedRun;

    private AudioEffectPlayer fxPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mAllCollisions = new List<Collision2D>();
        mBufferLevels = new List<GameObject>();
        currentSpeedRun = initialSpeedRun;

        fxPlayer = Camera.main.GetComponent<AudioEffectPlayer>();

        playerAnimator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        InfiniteRun();
        CheckLanding();
        Jump();
        CheckFallingIntoVoid();
        CheckUIEvents();

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
        Debug.Log("X-velocity: " + mRigidBody2D.velocity.x);
    }

    private void InfiniteRun()
    {
        mRigidBody2D.velocity = new Vector2(currentSpeedRun, mRigidBody2D.velocity.y);
        playerAnimator.speed = currentSpeedRun / 3;

        currentSpeedRun += speedRunAccelaration * Time.deltaTime;
        currentSpeedRun = currentSpeedRun < maxSpeedRun ? currentSpeedRun : maxSpeedRun;
    }

    private void CheckLanding()
    {
        bool previouslyOnGround = onGround;
        onGround = mAllCollisions.Exists(col => col.collider.CompareTag("Ground"));

        if (onGround && !previouslyOnGround)
        {
            fxPlayer.Play(fxPlayer.land);
            inJump = false;
        }
    }

    private void Jump()
    {
        float airTime = mJumpLength / currentSpeedRun;
        float gravity = (float)(mJumpHeight / Math.Pow(airTime / 2.0f, 2.0f));
        float verticalVelcocity = (float)Math.Sqrt(2.0f * gravity * mJumpHeight);

        mRigidBody2D.gravityScale = gravity / -Physics.gravity.y;

        if (Input.GetButton("Jump"))
        {
            if (onGround && jumpEnabled)
            {
                fxPlayer.Play(fxPlayer.jump);
                StartCoroutine(PreventMultiJump());

                if (!mIsFixedJump)
                {
                    //V1 : Imply physics; Depends of 
                    mRigidBody2D.AddForce(new Vector2(0, mForceJump), ForceMode2D.Impulse);
                }
                else
                {
                    //V2 : Fix lenght of jump
                    mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, verticalVelcocity);
                }
                inJump = true;
            }
        }
    }

    private void CheckFallingIntoVoid()
    {
        if (gameObject.transform.position.y < -10.0f)
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lethalRadiation = true; //Collision with gamma
        }
    }

    private void CheckUIEvents()
    {
        //Press Ecs to go to main menu
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenuScene");
        }
    }

    private IEnumerator PreventMultiJump()
    {
        jumpEnabled = false;
        yield return new WaitForSeconds(0.1f);
        jumpEnabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        mAllCollisions.Add(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Collision2D colToRemove = mAllCollisions.Find(c => c.gameObject.Equals(collision.gameObject));
        mAllCollisions.Remove(colToRemove);
    }
}
