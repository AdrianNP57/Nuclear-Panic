using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public Animator playerAnimator;

    public bool mIsFixedJump;
    public float mSpeedRun;

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

    private AudioEffectPlayer fxPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mAllCollisions = new List<Collision2D>();
        mBufferLevels = new List<GameObject>();

        fxPlayer = Camera.main.GetComponent<AudioEffectPlayer>();

        playerAnimator.Play("Run");
    }

    // Update is called once per frame
    void Update()
    {
        //Infinite run
        mRigidBody2D.velocity = new Vector2(mSpeedRun, mRigidBody2D.velocity.y);
        playerAnimator.speed = mSpeedRun / 3;

        CheckLanding();
        Jump();
        CheckFallingIntoVoid();
        CheckUIEvents();
    }

    private void CheckLanding()
    {
        bool previouslyOnGround = onGround;
        onGround = mAllCollisions.Exists(col => col.collider.CompareTag("Ground"));

        if (onGround && !previouslyOnGround)
        {
            fxPlayer.Play(fxPlayer.land);
        }
    }

    private void Jump()
    {
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
                    float airTime = mJumpLength / mSpeedRun;
                    float gravity = (float)(mJumpHeight / Math.Pow(airTime / 2.0f, 2.0f));
                    float verticalVelcocity = (float)Math.Sqrt(2.0f * gravity * mJumpHeight);

                    mRigidBody2D.gravityScale = gravity / -Physics.gravity.y;
                    mRigidBody2D.velocity = new Vector2(mRigidBody2D.velocity.x, verticalVelcocity);
                }
            }
        }
    }

    private void CheckFallingIntoVoid()
    {
        if (gameObject.transform.position.y < -10.0f)
        {
            GameObject.Find("RadiationBar").GetComponent<RadiationBar>().gamma = true; //Collision with gamma
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
