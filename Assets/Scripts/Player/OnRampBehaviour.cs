using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO fix this
public class OnRampBehaviour : MonoBehaviour
{
    private PlayerController controller;
    private JumpBehaviour jumpBehaviour;
    private Rigidbody2D rigidbody;

    private bool previouslyOnRamp;

    // Start is called before the first frame update
    void Awake()
    {
        controller = GetComponent<PlayerController>();
        jumpBehaviour = GetComponent<JumpBehaviour>();
        rigidbody = GetComponent<Rigidbody2D>();

        Init();
    }

    private void Init()
    {
        previouslyOnRamp = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (!jumpBehaviour.onJump)
        {
            if((previouslyOnRamp && !controller.onRamp) || controller.onRampEdge)

            rigidbody.velocity *= new Vector2(1, -1);
        }

        previouslyOnRamp = controller.onRamp;*/

        if(!jumpBehaviour.onJump && rigidbody.velocity.y > 0)
        {
            rigidbody.velocity *= new Vector2(1, 0);
        }
    }
}
