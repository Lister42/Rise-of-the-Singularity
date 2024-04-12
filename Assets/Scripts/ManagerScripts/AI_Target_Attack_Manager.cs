using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AI_Target_Attack_Manager : MonoBehaviour
{
    #region Various
    private string sceneName;
    private bool isScenarioOne = false;
    private bool isScenarioTwo = false;
    private bool isScenarioThree = false;
    private bool canSpawnEnemy = true;
    private int spawnMeleeCount;
    private int spawnRangedCount;
    private float scenario1_timeToStart = 120.0F;
    private float scenario2_timeToStart = 120.0F;
    private float scenario3_timeToStart = 120.0F;
    private float timeBetweenWaves = 180.0F;

    public Text timeNextWaveText;
    private float nextWaveTime = 0;

    public Transform _waypointsObjectTransform; 
    private List<Transform> waypointList;
    private int numWaypoints = 0;

    public Transform _spawnpointsObjectTransform;
    private List<Transform> spawnpointList;
    private int numSpawnpoints = 0;
    #endregion

    #region Prefabs
    public GameObject _enemyMeleePrefab;
    public GameObject _enemyRangedPrefab;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        spawnMeleeCount = 0;
        spawnRangedCount = 0;

        //Find which scenario we're working in and set up waypoints
        SetUpLists();
    }

    // Update is called once per frame
    void Update()
    {
        if (isScenarioOne && canSpawnEnemy && scenario1_timeToStart <= Time.time)
        {
            StartCoroutine(spawnEnemy());
        }
        else if (isScenarioTwo && canSpawnEnemy && scenario2_timeToStart <= Time.time)
        {
            StartCoroutine(spawnEnemy());
        }
        else if (isScenarioThree && canSpawnEnemy&& scenario3_timeToStart <= Time.time)
        {

        }

        UpdateTime();
    }

    private void SetUpLists()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
        {
            isScenarioOne = true;
            nextWaveTime = scenario1_timeToStart + timeBetweenWaves;
            FillWaypointList();
            FillSpawnpointList();
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
        {
            isScenarioTwo = true;
            nextWaveTime = scenario2_timeToStart + timeBetweenWaves;
            FillWaypointList();
            FillSpawnpointList();
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO3_SCENE))
        {
            isScenarioThree = true;
            nextWaveTime = scenario3_timeToStart + timeBetweenWaves;
            FillWaypointList();
            FillSpawnpointList();
        }
    }

    private void FillWaypointList()
    {
        waypointList = new List<Transform>();
        for (int i = 0; i < _waypointsObjectTransform.childCount; i++)
        {
            waypointList.Add(_waypointsObjectTransform.GetChild(i));
        }
        numWaypoints = waypointList.Count;
    }

    private void FillSpawnpointList()
    {
        spawnpointList = new List<Transform>();
        for (int i = 0; i < _spawnpointsObjectTransform.childCount; i++)
        {
            spawnpointList.Add(_spawnpointsObjectTransform.GetChild(i));
        }
        numSpawnpoints = spawnpointList.Count;
    }

    IEnumerator spawnEnemy()
    {
        print("Spawning enemies in 3 min...");
        canSpawnEnemy = false;
        nextWaveTime = timeBetweenWaves;
        yield return new WaitForSeconds(timeBetweenWaves); //CHANGE BACK TO 200!!!
        print("Spawning enemies...");
        if (Random.Range(0, 2) == 0) //Increment the number of units spawned
        {
            spawnMeleeCount++;
        }
        else
        {
            spawnRangedCount++;
        }

        int meleeCount = 0;
        while (meleeCount < spawnMeleeCount)
        {
            //print("HERE_M " + spawnMeleeCount);
            yield return new WaitForSeconds(1);
            int spawnIndex = Random.Range(0, spawnpointList.Count);
            GameObject newEnemyUnit = Instantiate(_enemyMeleePrefab, spawnpointList[spawnIndex].position, Quaternion.identity);
            StartCoroutine(Release(newEnemyUnit));
            meleeCount++;
        }

        int rangedCount = 0;
        while (rangedCount < spawnRangedCount)
        {
            //print("HERE_R " + spawnRangedCount);
            yield return new WaitForSeconds(1);
            int spawnIndex = Random.Range(0, spawnpointList.Count);
            GameObject newEnemyUnit = Instantiate(_enemyRangedPrefab, spawnpointList[spawnIndex].position, Quaternion.identity);
            StartCoroutine(Release(newEnemyUnit));
            rangedCount++;
        }

        canSpawnEnemy = true;
    }

    private IEnumerator Release(GameObject enemyUnit)
    {
        print("<<< RELEASED: " + enemyUnit.name + " >>> ");
        yield return new WaitForSeconds(1);

        string typeTag = enemyUnit.transform.GetChild(0).tag;
        Transform childTransform = enemyUnit.transform.GetChild(0).transform;
        int i = 0;

        if (enemyUnit && typeTag.Equals(TagList.MELEE_UNIT))
        {
            bool first = true;
            EnemyMeleeScript script = childTransform.GetComponent<EnemyMeleeScript>();
            while (enemyUnit && i < waypointList.Count)
            {
                yield return new WaitForSeconds(2);
                if (enemyUnit && script.getIsIdle() && (first || atNextWayPoint(childTransform, waypointList[i])))
                {
                    if (enemyUnit && i + 1 < waypointList.Count)
                    {
                        script.MoveToLocation(waypointList[i + 1].position);
                        first = false;
                    }
                    i++;
                }
            }
        }
        else if (enemyUnit && typeTag.Equals(TagList.RANGED_UNIT))
        {
            bool first = true;
            EnemyRangedScript script = enemyUnit.transform.GetChild(0).GetComponent<EnemyRangedScript>();
            while (enemyUnit && i < waypointList.Count)
            {
                yield return new WaitForSeconds(2);
                if (enemyUnit && script.getIsIdle() && (first || atNextWayPoint(childTransform, waypointList[i])))
                {
                    if (enemyUnit && i + 1 < waypointList.Count)
                    {
                        script.MoveToLocation(waypointList[i + 1].position);
                        first = false;
                    }
                    i++;
                }
            }
        }
    }

    private bool atNextWayPoint(Transform enemyUnit, Transform nextWaypoint)
    {
        if (enemyUnit)
        {
            if (Mathf.Abs(enemyUnit.position.x - nextWaypoint.position.x) < 1 &&
                Mathf.Abs(enemyUnit.position.y - nextWaypoint.position.y) < 1)
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateTime()
    {
        nextWaveTime -= Time.deltaTime;

        if (nextWaveTime <= 0)
        {
            nextWaveTime = 0.0F;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        string sec = "seconds";
        if ((int)nextWaveTime == 1)
        {
            sec = "second";
        }
        string numText = ((int)nextWaveTime).ToString("D3");
        timeNextWaveText.text = " " + numText + " " + sec;
    }
}
