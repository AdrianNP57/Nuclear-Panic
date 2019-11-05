using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO test on ramps
public class InfiniteRunBehaviour : MonoBehaviour
{
    public float initialSpeedRun;
    public float maxSpeedRun;
    public float speedRunAccelaration;

    [HideInInspector]
    public float currentSpeed;
    private bool isAlive;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
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
        rigidbody2D.velocity = new Vector2(isAlive? currentSpeed : 0, rigidbody2D.velocity.y);

        // TODO fix this
        if (/*!chooseDifficulty && score.CurrentScore() > 100*/ isAlive)
        {
            currentSpeed += speedRunAccelaration * Time.deltaTime;
            currentSpeed = currentSpeed < maxSpeedRun ? currentSpeed : maxSpeedRun;
        }
    }

    private void OnPlayerDied()
    {
        isAlive = false;
    }
}
