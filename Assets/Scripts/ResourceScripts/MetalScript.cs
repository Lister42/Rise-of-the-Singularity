using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private Pathfinding_Manager _pathfinding_Manager;
    private SpriteRenderer _spriteRenderer;

    #region Resource Variables
    private int currentResourceAmount = ResourceAmountList.METAL_AMOUNT;
    private int maxResourceAmount = ResourceAmountList.METAL_AMOUNT;
    private int moreResourceAmount = (int)((float)ResourceAmountList.METAL_AMOUNT * 0.66);
    private int lessResourceAmount = (int)((float)ResourceAmountList.METAL_AMOUNT * 0.33);
    private bool isFullSprite = true;
    private bool isMoreSprite = false;
    private bool isLessSprite = false;
    
    #endregion

    public Sprite _moreResourceSprite;
    public Sprite _lessResourceSprite;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _pathfinding_Manager = Pathfinding_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetSortingLayerOrder();
    }

    // Update is called once per frame
    void Update()
    {
        CheckResourceAmount();
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void CheckResourceAmount()
    {
        if (isFullSprite && currentResourceAmount <= moreResourceAmount)
        {
            _spriteRenderer.sprite = _moreResourceSprite;
            isFullSprite = false;
            isMoreSprite = true;
        }
        else if (isMoreSprite && currentResourceAmount <= lessResourceAmount)
        {
            _spriteRenderer.sprite = _lessResourceSprite;
            isMoreSprite = false;
            isLessSprite = true;
        }
        else if (isLessSprite && currentResourceAmount <= 0)
        {
            IsDestroyed();
            Destroy(transform.parent.gameObject);
        }
    }

    public bool IsDepleted()
    {
        return currentResourceAmount <= 0;
    }

    public int GetCurrentResourceAmount()
    {
        return currentResourceAmount;
    }
    public int GetMaxResourceAmount()
    {
        return maxResourceAmount;
    }

    public void DecrementResource()
    {
        currentResourceAmount--;
    }

    private void IsDestroyed()
    {
        // tell the Astar graph to remove this object
        Bounds bounds = transform.parent.GetComponent<Collider2D>().bounds;
        _pathfinding_Manager.AddPoint(bounds.center);
    }
}
