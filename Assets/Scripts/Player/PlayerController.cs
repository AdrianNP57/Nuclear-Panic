using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float raySpacing;
    public float rayOffsetY;
    public float rayLength;

    public LayerMask rayMask;

    [HideInInspector]
    public bool grounded;

    // Start is called before the first frame update
    void Awake()
    {
        grounded = false;
    }

    private void Update()
    {
        Vector2 leftOrigin = transform.position + raySpacing / 2 * Vector3.left + rayOffsetY * Vector3.up;
        Vector2 rightOrigin = transform.position + raySpacing / 2 * Vector3.right + rayOffsetY * Vector3.up;

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, rayLength, rayMask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, rayLength, rayMask);

        bool prevouslyGrounded = grounded;
        if(leftHit.collider != null || rightHit.collider != null)
        {
            grounded = true;
            Debug.Log("Grounded");
        }
        else
        {
            Debug.Log("Air");
            grounded = false;
        }

        if(!prevouslyGrounded && grounded)
        {
            EventManager.TriggerEvent("Land");
        }
    }
}
