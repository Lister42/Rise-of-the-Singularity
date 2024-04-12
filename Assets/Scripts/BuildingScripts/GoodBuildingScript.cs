using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodBuildingScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private Pathfinding_Manager _pathfinding_Manager;
    private SpriteRenderer _spriteRenderer;
    private Pause_Manager _pauseManger;
    private List<GameObject> attackerList;
    private InteractiveAudio_Manager _audioManager;
    private GameObject buildSiteType;
    private bool invincible = false;

    #region Hitpoint Variables
    private int currentHitpoints;
    private int maxHitpoints;
    private GameObject _healthObject;
    private GameObject _redHealth;
    private float deactivateTime = 0.0F;
    private float deactivateDelay = 2.0F;
    private bool forceHealthBar = false;
    #endregion

    private float nextTime = 1.0F;
    private float timeIncrement = 1.0F;
    private bool isCore = false;

    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _pathfinding_Manager = Pathfinding_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _pauseManger = Pause_Manager._Instance;
        attackerList = new List<GameObject>();
        _audioManager = InteractiveAudio_Manager._Instance;

        _healthObject = transform.parent.GetChild(2).gameObject;
        _redHealth = _healthObject.transform.GetChild(0).gameObject;
        _healthObject.SetActive(false);

        DecipherTypeOfBuilding();
        SetSortingLayerOrder();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHitpoints();
        CheckAttack();
        UpdateHealthBar();
        UpdateHealthActive();
    }

    private void DecipherTypeOfBuilding()
    {
        string tag = gameObject.tag;

        if (tag.Equals(TagList.CORE))
        {
            currentHitpoints = HitpointList.CORE_HITPOINTS;
            maxHitpoints = HitpointList.CORE_HITPOINTS;
            isCore = true;
        }
        else if (tag.Equals(TagList.OUTPOST))
        {
            currentHitpoints = HitpointList.OUTPOST_HITPOINTS;
            maxHitpoints = HitpointList.OUTPOST_HITPOINTS;
        }
        else if (tag.Equals(TagList.MINE))
        {
            currentHitpoints = HitpointList.MINE_HITPOINTS;
            maxHitpoints = HitpointList.MINE_HITPOINTS;
        }
        else if (tag.Equals(TagList.STATIC_COLLECTOR))
        {
            currentHitpoints = HitpointList.STATIC_COLLECTOR_HITPOINTS;
            maxHitpoints = HitpointList.STATIC_COLLECTOR_HITPOINTS;
        }
        else if (tag.Equals(TagList.FACTORY))
        {
            currentHitpoints = HitpointList.FACTORY_HITPOINTS;
            maxHitpoints = HitpointList.FACTORY_HITPOINTS;
        }
        else if (tag.Equals(TagList.TURRET))
        {
            currentHitpoints = HitpointList.TURRET_HITPOINTS;
            maxHitpoints = HitpointList.TURRET_HITPOINTS;
        }
        else if (tag.Equals(TagList.BUILD_SITE))
        {
            currentHitpoints = HitpointList.BUILD_SITE_HITPOINTS;
            maxHitpoints = HitpointList.BUILD_SITE_HITPOINTS;
            buildSiteType = GetComponent<BuildSiteScript>().GetTypePrefab();
            if (buildSiteType.tag.Equals(TagList.OUTPOST))
            {
                invincible = true;
            }
        }
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void CheckAttack()
    {
        if (nextTime <= Time.time && attackerList.Count > 0)
        {
            RemoveNullAttackList();
            nextTime = Time.time + timeIncrement;

            foreach (GameObject attacker in attackerList)
            {
                if (_spriteRenderer.isVisible)
                {
                    string attackerTag = attacker.transform.tag;
                    if (attackerTag.Equals(TagList.MELEE_UNIT))
                    {
                        _audioManager.meleeFire(gameObject);
                    }
                    else if (attackerTag.Equals(TagList.RANGED_UNIT))
                    {
                        _audioManager.rangedFire(gameObject);
                    }
                }
                DealDamage(GetAttackAmount(attacker));
            }
        }
    }

    private void RemoveNullAttackList()
    {
        attackerList.RemoveAll(item => item == null);
    }

    private int GetAttackAmount(GameObject attacker)
    {
        string aTag = attacker.tag;
        if (aTag.Equals(TagList.MELEE_UNIT))
        {
            return UnitAttackAmountList.MELEE_ATTACK;
        }
        else if (aTag.Equals(TagList.RANGED_UNIT))
        {
            return UnitAttackAmountList.RANGED_ATTACK;
        }

        return 0;
    }

    public void AddAttacker(GameObject attacker)
    {
        attackerList.Add(attacker);
    }
    public void RemoveAttacker(GameObject attacker)
    {
        attackerList.Remove(attacker);
    }

    public int GetCurrentHitpoints()
    {
        return currentHitpoints;
    }

    public int GetMaxHitpoints()
    {
        return maxHitpoints;
    }

    private void CheckHitpoints()
    {
        if (currentHitpoints <= 0)
        {
            if (isCore)
            {
                // win the game
                GameOver();
            }
            IsDestroyed();
            Destroyed();
        }
    }

    private void Destroyed()
    {
        _audioManager.buildingDeath(gameObject);
        Destroy(transform.parent.gameObject);
    }
    private void GameOver()
    {
        _pauseManger.GameOver(false);
    }

    public void IsDestroyed()
    {
        // tell the Astar graph to remove this object
        Bounds bounds = transform.parent.GetComponent<Collider2D>().bounds;
        _pathfinding_Manager.AddPoint(bounds.center);
    }

    public void DealDamage(int damage)
    {
        if (!invincible)
        {
            currentHitpoints -= damage;
            CheckHitpoints();
        }
        if (!_healthObject.activeInHierarchy)
        {
            SetHealthBarActive();
        }
        deactivateTime = Time.time + deactivateDelay;
    }

    private void UpdateHealthBar()
    {
        if (_healthObject.activeInHierarchy)
        {
            float modifier = 1.0F - (float)currentHitpoints / (float)maxHitpoints;
            _redHealth.transform.localScale = new Vector3(modifier, 1, 1);
        }
    }

    private void UpdateHealthActive()
    {
        if (forceHealthBar)
        {
            if (!_healthObject.activeInHierarchy)
            {
                SetHealthBarActive();
            }
        }
        else if (deactivateTime <= Time.time)
        {
            _healthObject.SetActive(false);
        }
    }

    private void SetHealthBarActive()
    {
        _healthObject.SetActive(true);
    }

    public void SetForceHealthBar(bool force)
    {
        forceHealthBar = force;
    }
}
