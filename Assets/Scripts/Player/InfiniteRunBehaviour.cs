using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRunBehaviour : MonoBehaviour
{
    public float initialSpeedRun;
    public float maxSpeedRun;
    public float speedRunAccelaration;

    private Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Init();
    }

    void Init()
    {
        PlayerData.instance.currentSpeed = initialSpeedRun;
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(PlayerData.instance.currentSpeed, rigidbody2D.velocity.y);

        // TODO fix this
        if (/*!chooseDifficulty && score.CurrentScore() > 100*/ false)
        {
            PlayerData.instance.currentSpeed += speedRunAccelaration * Time.deltaTime;
            PlayerData.instance.currentSpeed = PlayerData.instance.currentSpeed < maxSpeedRun ? PlayerData.instance.currentSpeed : maxSpeedRun;
        }
    }
}
