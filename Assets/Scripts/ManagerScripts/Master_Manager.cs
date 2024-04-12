using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Master_Manager _Instance { get; private set; } = null;

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

    public GameObject _mainCanvas;

    // Start is called before the first frame update
    void Start()
    {
        _mainCanvas.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
