using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoolManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> levels;
    public PlayerBehaviour playerBehaviour;
    public GameObject plainLevel;

    private List<GameObject> plainLevels;
    private int plainLevelsGenerated;
    private int standardLevelsGenerated;
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

        Init();
    }

    private void Init()
    {
        plainLevels = new List<GameObject>();
        plainLevelsGenerated = 0;
        standardLevelsGenerated = 0;
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
        Debug.Log(Camera.main.transform.position.x - CurrentEndOfWorld());

        if ((Camera.main.transform.position.x - CurrentEndOfWorld()) > -20)
        {
            if(playerBehaviour.chooseDifficulty)
            {
                NotPlayingLevelGeneration();
            }
            else
            {
                StandardLevelGeneration();
            }
        }
    }

    private void NotPlayingLevelGeneration()
    {
        GameObject newLevel = Instantiate(plainLevel, new Vector3(CurrentEndOfWorld(), 0, 0), Quaternion.identity);
        plainLevels.Add(newLevel);
        plainLevelsGenerated++;


        if(plainLevels.Count > 5)
        {
            Destroy(plainLevels[0]);
            plainLevels.RemoveAt(0);
        }
    }

    private void StandardLevelGeneration()
    {
        // Level 13 (easy tutorial level, always goes first)
        int newLevelIndex = standardLevelsGenerated > 0? (int)(UnityEngine.Random.value * levels.Count) : 0;
        newLevelIndex = newLevelIndex == previousLevelIndex ? newLevelIndex + 1 : newLevelIndex;
        newLevelIndex %= levels.Count;

        GameObject newLevel = levels[newLevelIndex];
        newLevel.transform.localPosition = new Vector3(CurrentEndOfWorld(), 0, 0);

        previousLevelIndex = newLevelIndex;
        standardLevelsGenerated++;
    }

    private int CurrentEndOfWorld()
    {
        return 10 + 10 * plainLevelsGenerated + 50 * standardLevelsGenerated;
    }
}
