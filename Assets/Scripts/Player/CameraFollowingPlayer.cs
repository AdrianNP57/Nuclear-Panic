using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowingPlayer : MonoBehaviour
{
    public GameObject player;

    public float mXOffset;
    public float mYOffset;

    private InfiniteRunBehaviour infiniteRun;
    private Vector3 initialPosition;

    void Awake() {
        float unitsXOffset = Camera.main.ScreenToWorldPoint(new Vector3(mXOffset, 0, 0)).x;

        infiniteRun = player.GetComponent<InfiniteRunBehaviour>();
        initialPosition = new Vector3(player.transform.position.x - unitsXOffset, mYOffset, -10);

        Init();
    }

    public void Init()
    {
        gameObject.transform.position = initialPosition;
    }

    void Update() {
        if (player != null) {
            float cameraY = player.transform.position.y + mYOffset >= 0 ? player.transform.position.y + mYOffset : 0;

            gameObject.transform.position = new Vector3(gameObject.transform.position.x, cameraY, gameObject.transform.position.z);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(infiniteRun.currentSpeed, 0);

            if (gameObject.GetComponent<Camera>().WorldToScreenPoint(player.transform.position).x <= 80)
            {
                GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lethalRadiation = true; //Collision with gamma
            }
        }
	}
}
