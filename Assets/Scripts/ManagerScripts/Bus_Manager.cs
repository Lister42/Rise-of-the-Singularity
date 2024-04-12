using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class Bus_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Bus_Manager _Instance { get; private set; } = null;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
    }
    #endregion

    public Tilemap tilemap;
    private float activateDelay = 0.25F;
    private float deactivateDelay = 0.25F;
    private string sceneName;
    private bool outpost1 = true;
    private bool outpost2 = true;
    private bool outpost3 = true;
    private bool junction1 = true;
    private bool junction2 = true;

    #region Tile Variables
    [SerializeField] public TileBase cross_activated;
    [SerializeField] public TileBase cross_deactivated;
    [SerializeField] public TileBase straight1_activated;
    [SerializeField] public TileBase straight1_deactivated;
    [SerializeField] public TileBase straight2_activated;
    [SerializeField] public TileBase straight2_deactivated;
    [SerializeField] public TileBase straight3_activated;
    [SerializeField] public TileBase straight3_deactivated;
    [SerializeField] public TileBase straight4_activated;
    [SerializeField] public TileBase straight4_deactivated;
    [SerializeField] public TileBase upperLeft_activated;
    [SerializeField] public TileBase upperLeft_deactivated;
    [SerializeField] public TileBase upperRight_activated;
    [SerializeField] public TileBase upperRight_deactivated;
    [SerializeField] public TileBase lowerLeft_activated;
    [SerializeField] public TileBase lowerLeft_deactivated;
    [SerializeField] public TileBase lowerRight_activated;
    [SerializeField] public TileBase lowerRight_deactivated;
    [SerializeField] public TileBase upperT_activated;
    [SerializeField] public TileBase upperT_deactivated;
    [SerializeField] public TileBase rightT_activated;
    [SerializeField] public TileBase rightT_deactivated;
    [SerializeField] public TileBase lowerT_activated;
    [SerializeField] public TileBase lowerT_deactivated;
    [SerializeField] public TileBase leftT_activated;
    [SerializeField] public TileBase leftT_deactivated;
    #endregion

    enum Tile_Type
    {
        Unkown,
        cross_activated,
        cross_deactivated,
        straight1_activated,
        straight1_deactivated,
        straight2_activated,
        straight2_deactivated,
        straight3_activated,
        straight3_deactivated,
        straight4_activated,
        straight4_deactivated,
        upperLeft_activated,
        upperLeft_deactivated,
        upperRight_activated,
        upperRight_deactivated,
        lowerLeft_activated,
        lowerLeft_deactivated,
        lowerRight_activated,
        lowerRight_deactivated,
        upperT_activated,
        upperT_deactivated,
        rightT_activated,
        rightT_deactivated,
        lowerT_activated,
        lowerT_deactivated,
        leftT_activated,
        leftT_deactivated,
    }

    private BusHelper busHelper;
    private InteractiveAudio_Manager _interactiveAudio_Manager;

    // Start is called before the first frame update
    void Start()
    {
        _interactiveAudio_Manager = InteractiveAudio_Manager._Instance;
        sceneName = SceneManager.GetActiveScene().name;
        busHelper = new BusHelper();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tutorial2_OutpostToGoodCore_Activate()
    {
        if (sceneName.Equals(SceneNameList.TUTORIAL2_SCENE))
            StartCoroutine(SequentialChangeToActivated(busHelper.GetTutorial2_OutpostToGoodCore()));
    }

    public void Scenario1_OutpostToEnemyCore_Deactivate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
            StartCoroutine(SequentialChangeToDeactivated(busHelper.GetScenario1_OutpostToEnemyCore(), 0)); 
    }

    public void Scenario1_OutpostToGoodCore_Activate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
            StartCoroutine(SequentialChangeToActivated(busHelper.GetScenario1_OutpostToGoodCore()));
    }

    public void Scenario2_Outpost1ToGoodCore_Activate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToActivated(busHelper.GetScenario2_Outpost1ToGoodCore()));
    }

    public void Scenario2_Outpost2ToGoodCore_Activate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToActivated(busHelper.GetScenario2_Outpost2ToGoodCore()));
    }

    public void Scenario2_Outpost3ToGoodCore_Activate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToActivated(busHelper.GetScenario2_Outpost3ToGoodCore()));
    }

    public void Scenario2_Outpost1ToEnemyCore_Deactivate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToDeactivated(busHelper.GetScenario2_Outpost1ToEnemyCore(), 1));
    }

    public void Scenario2_Outpost2ToEnemyCore_Deactivate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToDeactivated(busHelper.GetScenario2_Outpost2ToEnemyCore(), 2));
    }

    public void Scenario2_Outpost3ToEnemyCore_Deactivate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToDeactivated(busHelper.GetScenario2_Outpost3ToEnemyCore(), 3));
    }

    public void Scenario2_Junction1ToEnemyCore_Deactivate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToDeactivated(busHelper.GetScenario2_Junction1ToEnemyCore(), 0));
    }

    public void Scenario2_Junction2ToEnemyCore_Deactivate()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            StartCoroutine(SequentialChangeToDeactivated(busHelper.GetScenario2_Junction2ToEnemyCore(), 0));
    }


    private void CheckScenario2Junction1()
    {
        if (!outpost1 && !outpost2 && junction1)
        {
            Scenario2_Junction1ToEnemyCore_Deactivate();
            junction1 = false;
        }
    }

    private void CheckScenario2Junction2()
    {
        if (!outpost1 && !outpost2 && !outpost3 && !junction1 && junction2)
        {
            Scenario2_Junction2ToEnemyCore_Deactivate();
            junction2 = false;
        }
    }


    private TileBase GetDeactivated(TileBase tile)
    {
        if (tile.Equals(cross_activated)) { return cross_deactivated; }
        if (tile.Equals(straight1_activated)) { return straight1_deactivated; }
        if (tile.Equals(straight2_activated)) { return straight2_deactivated; }
        if (tile.Equals(straight3_activated)) { return straight3_deactivated; }
        if (tile.Equals(straight4_activated)) { return straight4_deactivated; }
        if (tile.Equals(upperLeft_activated)) { return upperLeft_deactivated; }
        if (tile.Equals(upperRight_activated)) { return upperRight_deactivated; }
        if (tile.Equals(lowerLeft_activated)) { return lowerLeft_deactivated; }
        if (tile.Equals(lowerRight_activated)) { return lowerRight_deactivated; }
        if (tile.Equals(upperT_activated)) { return upperT_deactivated; }
        if (tile.Equals(rightT_activated)) { return rightT_deactivated; }
        if (tile.Equals(lowerT_activated)) { return lowerT_deactivated; }
        if (tile.Equals(leftT_activated)) { return leftT_deactivated; };

        if (tile.Equals(cross_deactivated)) { return cross_deactivated; }
        if (tile.Equals(straight1_deactivated)) { return straight1_deactivated; }
        if (tile.Equals(straight2_deactivated)) { return straight2_deactivated; }
        if (tile.Equals(straight3_deactivated)) { return straight3_deactivated; }
        if (tile.Equals(straight4_deactivated)) { return straight4_deactivated; }
        if (tile.Equals(upperLeft_deactivated)) { return upperLeft_deactivated; }
        if (tile.Equals(upperRight_deactivated)) { return upperRight_deactivated; }
        if (tile.Equals(lowerLeft_deactivated)) { return lowerLeft_deactivated; }
        if (tile.Equals(lowerRight_deactivated)) { return lowerRight_deactivated; }
        if (tile.Equals(upperT_deactivated)) { return upperT_deactivated; }
        if (tile.Equals(rightT_deactivated)) { return rightT_deactivated; }
        if (tile.Equals(lowerT_deactivated)) { return lowerT_deactivated; }
        if (tile.Equals(leftT_deactivated)) { return leftT_deactivated; };
        print("<NULL: " + tile.name + ">");
        return null;
    }

    private TileBase GetActivated(TileBase tile)
    {
        if (tile.name.Equals(cross_deactivated.name)) { return cross_activated; }
        if (tile.name.Equals(straight1_deactivated.name)) { return straight1_activated; }
        if (tile.name.Equals(straight2_deactivated.name)) { return straight2_activated; }
        if (tile.name.Equals(straight3_deactivated.name)) { return straight3_activated; }
        if (tile.name.Equals(straight4_deactivated.name)) { return straight4_activated; }
        if (tile.name.Equals(upperLeft_deactivated.name)) { return upperLeft_activated; }
        if (tile.name.Equals(upperRight_deactivated.name)) { return upperRight_activated; }
        if (tile.name.Equals(lowerLeft_deactivated.name)) { return lowerLeft_activated; }
        if (tile.name.Equals(lowerRight_deactivated.name)) { return lowerRight_activated; }
        if (tile.name.Equals(upperT_deactivated.name)) { return upperT_activated; }
        if (tile.name.Equals(rightT_deactivated.name)) { return rightT_activated; }
        if (tile.name.Equals(lowerT_deactivated.name)) { return lowerT_activated; }
        if (tile.name.Equals(leftT_deactivated.name)) { return leftT_activated; }

        if (tile.name.Equals(cross_activated.name)) { return cross_activated; }
        if (tile.name.Equals(straight1_activated.name)) { return straight1_activated; }
        if (tile.name.Equals(straight2_activated.name)) { return straight2_activated; }
        if (tile.name.Equals(straight3_activated.name)) { return straight3_activated; }
        if (tile.name.Equals(straight4_activated.name)) { return straight4_activated; }
        if (tile.name.Equals(upperLeft_activated.name)) { return upperLeft_activated; }
        if (tile.name.Equals(upperRight_activated.name)) { return upperRight_activated; }
        if (tile.name.Equals(lowerLeft_activated.name)) { return lowerLeft_activated; }
        if (tile.name.Equals(lowerRight_activated.name)) { return lowerRight_activated; }
        if (tile.name.Equals(upperT_activated.name)) { return upperT_activated; }
        if (tile.name.Equals(rightT_activated.name)) { return rightT_activated; }
        if (tile.name.Equals(lowerT_activated.name)) { return lowerT_activated; }
        if (tile.name.Equals(leftT_activated.name)) { return leftT_activated; }
        print("<NULL: " + tile.name + ">");
        return null;
    }

    private IEnumerator SequentialChangeToActivated(List<Vector3Int> tilePositions)
    {
        foreach (Vector3Int pos in tilePositions)
        {
            yield return new WaitForSeconds(activateDelay);
            _interactiveAudio_Manager.busIncrement();
            tilemap.SetTile(pos, GetActivated(tilemap.GetTile(pos)));
        }
        _interactiveAudio_Manager.busComplete();
    }

    private IEnumerator SequentialChangeToDeactivated(List<Vector3Int> tilePositions, int boolToSet)
    {
        foreach (Vector3Int pos in tilePositions)
        {
            yield return new WaitForSeconds(deactivateDelay);
            _interactiveAudio_Manager.busDecrement();
            tilemap.SetTile(pos, GetDeactivated(tilemap.GetTile(pos)));
        }
       
        if (boolToSet == 0)
        {
            // 0 is the code to do nothing
        }
        else if (boolToSet == 1)
        {
            outpost1 = false;
        }
        else if (boolToSet == 2)
        {
            outpost2 = false;
        }
        else if (boolToSet == 3)
        {
            outpost3 = false;
        }

        if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
        {
            CheckScenario2Junction1();
            CheckScenario2Junction2();
        }
    }
}
