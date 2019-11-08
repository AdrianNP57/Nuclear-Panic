using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float raySpacing;
    public float rayLength;
    public float rayOffsetY;

    public LayerMask rayMask;

    [HideInInspector]
    public bool grounded;
    [HideInInspector]
    public bool onRamp;
    [HideInInspector]
    public bool onRampEdge;

    [HideInInspector]
    public bool isAlive;

    // Start is called before the first frame update
    void Awake()
    {
        EventManager.StartListening("PlayerDied", OnPlayerDied);
        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    private void Init()
    {
        grounded = onRamp = onRampEdge = false;
        isAlive = true;
    }

    private void Update()
    {
        GroundCheck();
        RampCheck();
    }

    private void LateUpdate()
    {
        if(grounded) {
            DebugPanelBehaviour.Log("State", "Grounded");
        }
        else
        {
            DebugPanelBehaviour.Log("State", "On Air");
        }
    }

    private void GroundCheck()
    {
        Vector2 leftOrigin = transform.position + raySpacing / 2 * Vector3.left + rayOffsetY * Vector3.up;
        Vector2 rightOrigin = transform.position + raySpacing / 2 * Vector3.right + rayOffsetY * Vector3.up;

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, rayLength, rayMask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, rayLength, rayMask);

        Debug.DrawRay(leftOrigin, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rightOrigin, Vector2.down * rayLength, Color.red);

        bool prevouslyGrounded = grounded;
        grounded = leftHit.collider != null || rightHit.collider != null;

        if (!prevouslyGrounded && grounded && isAlive)
        {
            EventManager.TriggerEvent("Land");
        }
    }

    private void RampCheck()
    {
        Vector2 leftOrigin = transform.position + raySpacing / 2 * Vector3.left + rayOffsetY * Vector3.up;
        Vector2 rightOrigin = transform.position + raySpacing / 2 * Vector3.right + rayOffsetY * Vector3.up;

        RaycastHit2D leftHit = Physics2D.Raycast(leftOrigin, Vector2.down, 2 * rayLength, rayMask);
        RaycastHit2D rightHit = Physics2D.Raycast(rightOrigin, Vector2.down, 2 * rayLength, rayMask);

        Debug.DrawRay(leftOrigin, Vector2.down * 2 * rayLength, Color.red);
        Debug.DrawRay(rightOrigin, Vector2.down *  2 * rayLength, Color.red);

        onRamp = IsRayOnRamp(rightHit) && grounded;
        onRampEdge = IsRayOnRamp(leftHit) && !IsRayOnRamp(rightHit) && grounded;

        DebugPanelBehaviour.Log("On ramp", onRamp.ToString());
    }

    private bool IsRayOnRamp(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            float groundAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (Mathf.Abs(groundAngle - 45) < 10)
            {
                return true;
            }
        }

        return false;
    }

    private void OnPlayerDied()
    {
        isAlive = false;
    }
}
