using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
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
        if (collision.gameObject.tag.Equals(TagList.ENEMY_UNIT))
        {
            EnemyUnitScript enemyUnitScript = collision.transform.GetChild(0).gameObject.GetComponent<EnemyUnitScript>();
            if (enemyUnitScript)
            {
                enemyUnitScript.DealDamage(UnitAttackAmountList.BULLET_ATTACK);
            }
            Destroy(gameObject);
        }
    }
}
