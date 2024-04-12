using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private SpriteRenderer _spriteRenderer;
    private InteractiveAudio_Manager _audioManager;

    private List<GameObject> attackerList;

    #region Health Variables
    private int currentHealth;
    private int maxHealth;
    private GameObject _healthObject;
    private GameObject _redHealth;
    private float deactivateTime = 0.0F;
    private float deactivateDelay = 2.0F;
    private bool forceHealthBar = false;
    #endregion

    private float nextTime = 1.0F;
    private float timeIncrement = 1.0F;

    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        attackerList = new List<GameObject>();
        _audioManager = InteractiveAudio_Manager._Instance;

        _healthObject = transform.parent.GetChild(2).gameObject;
        _redHealth = _healthObject.transform.GetChild(0).gameObject;
        _healthObject.SetActive(false);

        DecipherTypeOfEnemyUnit();
        SetSortingLayerOrder();
    }

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
        CheckAttack();
        UpdateHealthBar();
        UpdateHealthActive();
    }

    private void DecipherTypeOfEnemyUnit()
    {
        string tag = gameObject.tag;

        if (tag.Equals(TagList.MELEE_UNIT))
        {
            currentHealth = HealthList.MELEE_HEALTH;
            maxHealth = HealthList.MELEE_HEALTH;
        }
        else if (tag.Equals(TagList.RANGED_UNIT))
        {
            currentHealth = HealthList.RANGED_HEALTH;
            maxHealth = HealthList.RANGED_HEALTH;
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
            nextTime = Time.time + timeIncrement;
            RemoveNullAttackList();

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


    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    private void CheckHealth()
    {
        if (currentHealth <= 0)
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        _audioManager.unitDeath(gameObject);
        Destroy(transform.parent.gameObject);
    }
    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        CheckHealth();
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
            float modifier = 1.0F - (float)currentHealth / (float)maxHealth;
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
