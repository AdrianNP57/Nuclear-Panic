using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO test on ramps
public class InfiniteRunBehaviour : MonoBehaviour
{
    public float initialSpeedRun;
    public float maxSpeedRun;
    public float speedRunAccelaration;
    public float onRampMultiplier;

    [HideInInspector]
    public float currentSpeed;
    private bool isAlive;

    private Rigidbody2D rigidbody;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();

        EventManager.StartListening("PlayerDied", OnPlayerDied);

        Init();
    }

    void Init()
    {
        currentSpeed = initialSpeedRun;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        float actualSpeed = controller.onRamp ? currentSpeed * onRampMultiplier : currentSpeed;
        rigidbody.velocity = new Vector2(isAlive? actualSpeed : 0, rigidbody.velocity.y);

        // TODO fix this
        if (/*!chooseDifficulty && score.CurrentScore() > 100*/ false)
        {
            currentSpeed += speedRunAccelaration * Time.deltaTime;
            currentSpeed = currentSpeed < maxSpeedRun ? currentSpeed : maxSpeedRun;
        }

        DebugPanelBehaviour.Log("Speed", currentSpeed.ToString());
    }

    private void OnPlayerDied()
    {
        isAlive = false;
    }
}
