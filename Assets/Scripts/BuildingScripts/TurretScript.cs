using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SortingLayerOrder_Manager _sortingLayerOrderManager;
    private InteractiveAudio_Manager _interactiveAudioManager;

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
        _transform = transform.parent;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        //_buildingSight = _transform.GetChild(2).GetComponent<BuildingSight>();

        SetSortingLayerOrder();
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
}
