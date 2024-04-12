using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private SpriteRenderer _spriteRenderer;

    private float createdTime;
    private float life = 5F;

    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        createdTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        SetSortingLayerOrder();
        CheckDelete();
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.position.y);
    }

    private void CheckDelete()
    {
        if (Time.time >= createdTime + life)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(TagList.MELEE_UNIT) 
            || collision.gameObject.tag.Equals(TagList.RANGED_UNIT)
            || collision.gameObject.tag.Equals(TagList.WORKER))
        {
            UnitScript unitScript = collision.transform.GetChild(0).gameObject.GetComponent<UnitScript>();
            if (unitScript)
            {
                unitScript.DealDamage(UnitAttackAmountList.ENEMY_BULLET_ATTACK);
            }     
            Destroy(gameObject);
        }
    }
}
