using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutpostScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrderManager;
    private SpriteRenderer _spriteRenderer;
    private InteractiveAudio_Manager _interactiveAudioManager;
    private Bus_Manager _busManager;

    private string sceneName;

    #region Firing Variables
    private Transform _transform;
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
        _sortingLayerOrderManager = SortingLayerOrder_Manager._Instance;
        _interactiveAudioManager = InteractiveAudio_Manager._Instance;
        _busManager = Bus_Manager._Instance;
        _transform = transform.parent;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_buildingSight = _transform.GetChild(2).GetComponent<BuildingSight>();
        sceneName = SceneManager.GetActiveScene().name;

        SetSortingLayerOrder();

        BusActivation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckFire();
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

    private void BusActivation()
    {
        Vector2 pos = transform.parent.position;
        Vector2 scenario2_Outpost1 = new Vector2(27.5F, -27.5F);
        Vector2 scenario2_Outpost2 = new Vector2(-1.5F, -2.5F);
        Vector2 scenario2_Outpost3 = new Vector2(-24.5F, 24.5F);

        if (sceneName.Equals(SceneNameList.TUTORIAL2_SCENE))
        {
            _busManager.Tutorial2_OutpostToGoodCore_Activate();
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
        {
            _busManager.Scenario1_OutpostToGoodCore_Activate();
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
        {
            if (pos.Equals(scenario2_Outpost1))
            {
                _busManager.Scenario2_Outpost1ToGoodCore_Activate();
            }
            else if (pos.Equals(scenario2_Outpost2))
            {
                _busManager.Scenario2_Outpost2ToGoodCore_Activate();
            }
            else if (pos.Equals(scenario2_Outpost3))
            {
                _busManager.Scenario2_Outpost3ToGoodCore_Activate();
            }  
        }
    }
}
