using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyOutpostScript : MonoBehaviour
{
    private Setup_Manager _setupManager;
    private SortingLayerOrder_Manager _sortingLayerOrderManager;
    private SpriteRenderer _spriteRenderer;
    private InteractiveAudio_Manager _interactiveAudioManager;
    private Bus_Manager _busManager;

    public GameObject _buildSitePrefab;
    public GameObject _goodOutpostPrefab;
    private Transform _transform;
    private GameObject buildingParent;
    private bool createdBuildSite = false;
    private string sceneName;
    public int outpostNum;

    #region Firing Variables
    public GameObject _bulletPrefab;
    #endregion

    #region Sight Variables
    private float nextSightCheck = 0.0F;
    public BuildingSight _buildingSight;
    private float buildingSightDelay = BuildingSightList.BUILDING_SIGHT_DELAY;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _setupManager = Setup_Manager._Instance;
        _sortingLayerOrderManager = SortingLayerOrder_Manager._Instance;
        _interactiveAudioManager = InteractiveAudio_Manager._Instance;
        _busManager = Bus_Manager._Instance;
        _transform = transform.parent;

        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_buildingSight = _transform.GetChild(2).GetComponent<BuildingSight>();

        sceneName = SceneManager.GetActiveScene().name;

        SetSortingLayerOrder();
        LoadVariables();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
    }

    private void LoadVariables()
    {
        buildingParent = _setupManager.GetBuildingParent();
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrderManager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void CheckFire()
    {
        if (nextSightCheck <= Time.time)
        {
            nextSightCheck = Time.time + buildingSightDelay;
            IdleSightFire();
        }
    }

    private void IdleSightFire()
    {
        GameObject objectInSight = _buildingSight.GetNextAttack();

        // if no object in sight to attack, then return
        if (!objectInSight)
        {
            return;
        }

        // there is an object to fire at, so fire at it
        Fire(objectInSight);
    }

    private void Fire(GameObject enemy)
    {
        if (_spriteRenderer.isVisible)
        {
            _interactiveAudioManager.turretFire();
        }
        float speed = 5F;
        Vector2 direction = ((enemy.transform.position - transform.position));
        direction.Normalize();
        GameObject firedBullet = Instantiate(_bulletPrefab, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        firedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * speed, direction.y * speed);
    }

    public void IsDestroyed()
    {
        if (!createdBuildSite)
        {
            createdBuildSite = true;
            GameObject building = Instantiate(_buildSitePrefab, _transform.position, Quaternion.identity);
            building.transform.parent = buildingParent.transform;
            building = building.transform.GetChild(0).gameObject;
            building.GetComponent<BuildSiteScript>().SetTypePrefab(_goodOutpostPrefab);
            building.GetComponent<BuildSiteScript>().SetBuildingParent(buildingParent);

            building.GetComponent<SpriteRenderer>().sortingOrder = _sortingLayerOrderManager.ComputeSortingLayer(building.transform.position.y);
            BusActivation();
        }
    }

    private void BusActivation()
    {
        if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
        {
            _busManager.Scenario1_OutpostToEnemyCore_Deactivate();
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
        {
            if (outpostNum == 1)
            {
                _busManager.Scenario2_Outpost1ToEnemyCore_Deactivate();
            }
            else if (outpostNum == 2)
            {
                _busManager.Scenario2_Outpost2ToEnemyCore_Deactivate();
            }
            else if (outpostNum == 3)
            {
                _busManager.Scenario2_Outpost3ToEnemyCore_Deactivate();
            }
        }
    }
}
