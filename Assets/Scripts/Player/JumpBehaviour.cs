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
    private InfiniteRunBehaviour inifinteRun;

    [HideInInspector]
    public bool onJump;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerController = GetComponent<PlayerController>();
        inifinteRun = GetComponent<InfiniteRunBehaviour>();

        EventManager.StartListening("DifficultyChosen", OnDifficultyChosen);
        EventManager.StartListening("Land", OnLand);
    }

    private void Update()
    {
        float airTime = jumpLength / inifinteRun.currentSpeed;

        rigidbody2D.gravityScale = (float)(jumpHeight / Math.Pow(airTime / 2.0f, 2.0f));

        DebugPanelBehaviour.Log("On jump", onJump.ToString());
    }

    void Jump()
    {
        float verticalVelcocity = (float)Math.Sqrt(2.0f * rigidbody2D.gravityScale * jumpHeight);

        // TODO fix
        if (/*(onGround || onExtraTimeToJump) && jumpEnabled*/ playerController.grounded)
        {
            EventManager.TriggerEvent("Jump");
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, verticalVelcocity);
            onJump = true;
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

    private void OnDifficultyChosen()
    {
        EventManager.StartListening("InputJump", Jump);
    }

    private void OnLand()
    {
        onJump = false;
    }
}
