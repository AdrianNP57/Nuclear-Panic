using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    public AudioEffectPlayer fxPlayer;
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
    private int mNbLvl;
    public List<GameObject> mLevels;
    public List<GameObject> mBufferLevels;

    private bool onGround = true;
    private bool jumpEnabled = true;

    // Start is called before the first frame update
    void Awake()
    {
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mAllCollisions = new List<Collision2D>();
        mBufferLevels = new List<GameObject>();
        mNbLvl = 0;

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

        //Autogeneration Level 
        //TODO : Move it to a better place...
        if ((gameObject.transform.position.x - 50 * mNbLvl) > 20)
        {
            int indLvlToInstantiate = (int)(UnityEngine.Random.value * mLevels.Count);//chose randomly a level​
            GameObject newLvl = GameObject.Instantiate(mLevels[indLvlToInstantiate],
                                                       new Vector3(50 + mNbLvl * 50, 0, 0),
                                                       Quaternion.identity);

            //Delete previous levels 
            if (mBufferLevels.Count >= 3)
            {
                GameObject.Destroy(mBufferLevels[0]);
                mBufferLevels.RemoveAt(0);
            }

            mBufferLevels.Add(newLvl);

            ++mNbLvl; //increase the number of levels already 
        }


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
                    Debug.Log(airTime);
                    float gravity = (float)(mJumpHeight / Math.Pow(airTime / 2.0f, 2.0f));
                    Debug.Log(gravity);
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
