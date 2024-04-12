using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingLayerOrder_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static SortingLayerOrder_Manager _Instance { get; private set; } = null;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
    }
    #endregion

    private int mapHeight = 100;

    public int ComputeSortingLayer(float yPosition)
    {
        return -1 * (int)((yPosition + mapHeight) * 100);
    }
}
