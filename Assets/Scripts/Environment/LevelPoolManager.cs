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

        Init();
    }

    private void Init()
    {
        levelsGenerated = 0;
        previousLevelIndex = -1;
    }

    public void ReInit()
    {
        foreach(GameObject level in levels)
        {
            level.transform.localPosition = new Vector3(0, -1000, 0);
        }

        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.x - 50 * levelsGenerated) > 20)
        {
            // Level 13 (easy tutorial level, always goes first)
            int newLevelIndex = levelsGenerated > 0? (int) (UnityEngine.Random.value * levels.Count) : 0;
            newLevelIndex = newLevelIndex == previousLevelIndex ? newLevelIndex + 1 : newLevelIndex;
            newLevelIndex %= levels.Count;

            GameObject newLevel = levels[newLevelIndex];
            newLevel.transform.localPosition = new Vector3(50 + levelsGenerated * 50, 0, 0);

            previousLevelIndex = newLevelIndex;
            levelsGenerated++;
        }
    }
}
