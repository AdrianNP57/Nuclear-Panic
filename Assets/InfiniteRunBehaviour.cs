using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunBehaviour : MonoBehaviour
{
    public float initialSpeedRun;
    public float maxSpeedRun;
    public float speedRunAccelaration;

    public Animator playerAnimator;

    private float currentSpeedRun;
    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Init();
    }

    void Init()
    {
        currentSpeedRun = initialSpeedRun;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(currentSpeedRun, rigidbody2D.velocity.y);
        playerAnimator.speed = currentSpeedRun / 3;

        // TODO fix this
        if (/*!chooseDifficulty && score.CurrentScore() > 100*/ false)
        {
            currentSpeedRun += speedRunAccelaration * Time.deltaTime;
            currentSpeedRun = currentSpeedRun < maxSpeedRun ? currentSpeedRun : maxSpeedRun;
        }
    }
}
