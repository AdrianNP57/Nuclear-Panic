using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingPlayer : MonoBehaviour {
    public bool isConstantSpeed;
    public GameObject mPlayer;
    public float mYOffset;

    void Start() {
        gameObject.transform.Translate(new Vector3(0, mYOffset, 0));
    }

    void Update() {
        if (mPlayer != null) {
            if(!isConstantSpeed)
            {
                Vector3 playPos = mPlayer.transform.position;
                gameObject.transform.position = new Vector3(playPos.x,
                                                            (playPos.y > -0.5f) ? playPos.y + mYOffset : gameObject.transform.position.y,
                                                            gameObject.transform.position.z);
            }
            else
            {
                Vector3 currentCameraPos = gameObject.transform.position;
                var playerBehaviour = (mPlayer.GetComponent<PlayerBehaviour>() as PlayerBehaviour);

                gameObject.transform.position = new Vector3(currentCameraPos.x + playerBehaviour.mSpeedRun * Time.deltaTime,
                    currentCameraPos.y, currentCameraPos.z);

                if (gameObject.GetComponent<Camera>().WorldToScreenPoint(mPlayer.transform.position).x <= 0)
                {
                    Debug.Log("GAME OVER");
                    Time.timeScale = 0;
                }
            }
        }
	}
}
