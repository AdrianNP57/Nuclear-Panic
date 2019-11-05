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

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Init();
    }

    void Init()
    {
        currentSpeed = initialSpeedRun;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(currentSpeed, rigidbody2D.velocity.y);

        // TODO fix this
        if (/*!chooseDifficulty && score.CurrentScore() > 100*/ false)
        {
            currentSpeed += speedRunAccelaration * Time.deltaTime;
            currentSpeed = currentSpeed < maxSpeedRun ? currentSpeed : maxSpeedRun;
        }
    }
}
