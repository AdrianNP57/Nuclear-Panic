using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPoolManager : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> levels;
    [HideInInspector]
    public List<GameObject> plainLevels;
    public PlayerBehaviour playerBehaviour;
    public GameObject plainLevelPrefab;

    private int plainLevelsGenerated;
    private int standardLevelsGenerated;
    private int previousLevelIndex;

    private List<GameObject> currentlyVisibleLevels;

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
        currentlyVisibleLevels = new List<GameObject>();
        plainLevelsGenerated = 0;
        standardLevelsGenerated = 0;
        previousLevelIndex = -1;

        foreach(GameObject level in levels)
        {
            level.SetActive(false);
        }
    }

    public void ReInit()
    {
        foreach(GameObject level in levels)
        {
            level.transform.localPosition = new Vector3(0, -1000, 0);
        }

        foreach(GameObject plainLevel in plainLevels)
        {
            Destroy(plainLevel);
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
        GameObject newLevel = Instantiate(plainLevelPrefab, new Vector3(CurrentEndOfWorld(), 0, 0), Quaternion.identity);
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
        // Tutorial level always first and never again
        int newLevelIndex = standardLevelsGenerated > 0? UnityEngine.Random.Range(1, levels.Count) : 0;
        newLevelIndex = newLevelIndex == previousLevelIndex ? newLevelIndex + 1 : newLevelIndex;
        newLevelIndex = newLevelIndex < levels.Count? newLevelIndex : 1;

        GameObject newLevel = levels[newLevelIndex];
        newLevel.transform.localPosition = new Vector3(CurrentEndOfWorld(), 0, 0);
        newLevel.SetActive(true);

        previousLevelIndex = newLevelIndex;
        standardLevelsGenerated++;

        currentlyVisibleLevels.Add(newLevel);
        if(currentlyVisibleLevels.Count > 2)
        {
            Debug.Log("Disabled level");
            currentlyVisibleLevels[0].SetActive(false);
            currentlyVisibleLevels.RemoveAt(0);
        }
    }

    public int CurrentEndOfWorld()
    {
        return 10 + 10 * plainLevelsGenerated + 50 * standardLevelsGenerated;
    }
}
