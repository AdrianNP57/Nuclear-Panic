using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollowingPlayer : MonoBehaviour {
    public bool isConstantSpeed;
    public GameObject mPlayer;
    public float mXOffset;
    public float mYOffset;

    void Start() {
        //gameObject.transform.Translate(new Vector3(mPlayer.transform.position.x - unitsXOffset, mYOffset, 0));
        float unitsXOffset = Camera.main.ScreenToWorldPoint(new Vector3(mXOffset, 0, 0)).x;
        gameObject.transform.position = new Vector3(mPlayer.transform.position.x - unitsXOffset, mYOffset, -10);
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

                float cameraY = mPlayer.transform.position.y + mYOffset >= 0 ? mPlayer.transform.position.y + mYOffset : 0;

                gameObject.transform.position = new Vector3(gameObject.transform.position.x, cameraY, gameObject.transform.position.z);
                gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(playerBehaviour.currentSpeedRun, 0);

                if (gameObject.GetComponent<Camera>().WorldToScreenPoint(mPlayer.transform.position).x <= 0)
                {
                    GameObject.Find("RadiationBar").GetComponent<RadiationBar>().lethalRadiation = true; //Collision with gamma
                    //Debug.Log("GAME OVER");
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }
	}
}
