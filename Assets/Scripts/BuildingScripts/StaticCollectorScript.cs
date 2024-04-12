using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollectorScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();

        SetSortingLayerOrder();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }
}
