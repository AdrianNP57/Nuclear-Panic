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

    public ScoreBehaviour score;

    [HideInInspector]
    public float currentSpeed;

    private Vector2 initialPosition;
    private Rigidbody2D rigidbody;
    private PlayerController controller;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = gameObject.transform.position;
        rigidbody = GetComponent<Rigidbody2D>();
        controller = GetComponent<PlayerController>();

        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    void Init()
    {
        gameObject.transform.position = initialPosition;
        currentSpeed = initialSpeedRun;
    }

    // Update is called once per frame
    void Update()
    {
        float actualSpeed = controller.onRamp ? currentSpeed * onRampMultiplier : currentSpeed;
        rigidbody.velocity = new Vector2(controller.isAlive? actualSpeed : 0, rigidbody.velocity.y);

        if (score.CurrentScore() > 100 && controller.isAlive)
        {
            currentSpeed += speedRunAccelaration * Time.deltaTime;
            currentSpeed = currentSpeed < maxSpeedRun ? currentSpeed : maxSpeedRun;
        }

        if(gameObject.transform.position.y < -8 && controller.isAlive)
        {
            EventManager.TriggerEvent("PlayerDied");
        }

        DebugPanelBehaviour.Log("Speed", currentSpeed.ToString());
    }
}
