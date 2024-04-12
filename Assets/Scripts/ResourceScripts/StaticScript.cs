using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private Pathfinding_Manager _pathfinding_Manager;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    public RuntimeAnimatorController _fullAnimation;
    public RuntimeAnimatorController _fullSecondAnimation;
    public RuntimeAnimatorController _fullThirdAnimation;
    public RuntimeAnimatorController _moreAnimation;
    public RuntimeAnimatorController _lessAnimation;

    #region Resource Variables
    private int currentResourceAmount = ResourceAmountList.STATIC_AMOUNT;
    private int maxResourceAmount = ResourceAmountList.STATIC_AMOUNT;
    private int moreResourceAmount = (int)((float)ResourceAmountList.STATIC_AMOUNT * 0.66);
    private int lessResourceAmount = (int)((float)ResourceAmountList.STATIC_AMOUNT * 0.33);
    private bool isFullAnimation = true;
    private bool isMoreAnimation = false;
    private bool isLessAnimation = false;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _pathfinding_Manager = Pathfinding_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animator.runtimeAnimatorController = _fullAnimation;

        RunAnimation();
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

        if (isFullAnimation && currentResourceAmount <= moreResourceAmount)
        {
            _animator.runtimeAnimatorController = _moreAnimation;
            isFullAnimation = false;
            isMoreAnimation = true;
        }
        else if (isMoreAnimation && currentResourceAmount <= lessResourceAmount)
        {
            _animator.runtimeAnimatorController = _lessAnimation;
            isMoreAnimation = false;
            isLessAnimation = true;
        }
        else if (isLessAnimation && currentResourceAmount <= 0)
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

    IEnumerator StaticFullAnimations(RuntimeAnimatorController rac, float sec)
    { 
        _animator.runtimeAnimatorController = rac;
        yield return new WaitForSeconds(sec);
        RunAnimation();

    }
    private void RunAnimation()
    {
        if (!isFullAnimation)
        {
            return;
        }

        int r = Random.Range(0, 3);
        if (r == 0)
        {
            StartCoroutine(StaticFullAnimations(_fullAnimation, 0.5f));
        }
        else if (r == 1)
        {
            StartCoroutine(StaticFullAnimations(_fullSecondAnimation, 0.5f));
        }
        else if (r == 2)
        {
            StartCoroutine(StaticFullAnimations(_fullThirdAnimation, 0.5f));
        }

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
