using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoolManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> levels;
    private GameObject player;

    private int levelsGenerated;
    private int previousLevelIndex;

    void Awake()
    {
        levels = new List<GameObject>();

        foreach (Transform levelTransform in transform)
        {
            GameObject level = levelTransform.gameObject;
            levels.Add(level);
        }

        transform.DetachChildren();
        player = GameObject.FindGameObjectWithTag("Player");

        levelsGenerated = 0;
        previousLevelIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x - 50 * levelsGenerated) > 20)
        {
            int newLevelIndex = (int) (UnityEngine.Random.value * levels.Count);
            newLevelIndex = newLevelIndex == previousLevelIndex ? newLevelIndex + 1 : newLevelIndex;
            newLevelIndex %= levels.Count;

            GameObject newLevel = levels[newLevelIndex];
            newLevel.transform.localPosition = new Vector3(50 + levelsGenerated * 50, 0, 0);

            // TODO disable previous levels

            previousLevelIndex = newLevelIndex;
            levelsGenerated++;
        }
    }
}
