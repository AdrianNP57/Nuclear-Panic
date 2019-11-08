using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowingPlayer : MonoBehaviour
{
    public GameObject player;

    public float mXOffset;
    public float mYOffset;

    private Vector3 initialPosition;

    private PlayerController playerController;
    private InfiniteRunBehaviour infiniteRun;

    void Awake() {
        float unitsXOffset = Camera.main.ScreenToWorldPoint(new Vector3(mXOffset, 0, 0)).x;
        initialPosition = new Vector3(player.transform.position.x - unitsXOffset, mYOffset, -10);

        playerController = player.GetComponent<PlayerController>();
        infiniteRun = player.GetComponent<InfiniteRunBehaviour>();

        EventManager.StartListening("GameRestart", Init);

        Init();
    }

    public void Init()
    {
        gameObject.transform.position = initialPosition;
    }

    void Update() {
        float cameraY = player.transform.position.y + mYOffset >= 0 ? player.transform.position.y + mYOffset : 0;

        gameObject.transform.position = new Vector3(gameObject.transform.position.x, cameraY, gameObject.transform.position.z);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(infiniteRun.currentSpeed, 0);

        if (DistanceToLethal() <= 0 && playerController.isAlive)
        {
            EventManager.TriggerEvent("PlayerDied");
        }

        DebugPanelBehaviour.Log("Distance to chasing alpha", DistanceToLethal().ToString());
	}

    private float DistanceToLethal()
    {
        return gameObject.GetComponent<Camera>().WorldToScreenPoint(player.transform.position).x - 80;
    }
}
