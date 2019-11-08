using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO jump is getting triggerd more than once per actual jump
// TODO implement coyote time
public class JumpBehaviour : MonoBehaviour
{
    public float jumpLength;
    public float jumpHeight;

    private Rigidbody2D rigidbody2D;
    private PlayerController playerController;
    private InfiniteRunBehaviour infiniteRun;

    [HideInInspector]
    public bool onJump;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        infiniteRun = GetComponent<InfiniteRunBehaviour>();

        EventManager.StartListening("DifficultyChosen", OnDifficultyChosen);
        EventManager.StartListening("Land", OnLand);
        EventManager.StartListening("PlayerDied", OnPlayerDied);
        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    private void Init()
    {
        onJump = false;
    }

    private void Update()
    {
        float airTime = jumpLength / infiniteRun.currentSpeed;

        rigidbody2D.gravityScale = (float)(jumpHeight / Math.Pow(airTime / 2.0f, 2.0f));

        DebugPanelBehaviour.Log("On jump", onJump.ToString());
    }

    void Jump()
    {
        float verticalVelocity = (float)Math.Sqrt(2.0f * rigidbody2D.gravityScale * jumpHeight);

        if (playerController.grounded)
        {
            EventManager.TriggerEvent("Jump");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalVelocity);
            onJump = true;
        }
    }

    private void OnDifficultyChosen()
    {
        EventManager.StartListening("InputJump", Jump);
    }

    private void OnLand()
    {
        onJump = false;
    }

    private void OnPlayerDied()
    {
        EventManager.StopListening("InputJump", Jump);
    }
}
