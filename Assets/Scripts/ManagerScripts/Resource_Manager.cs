using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Resource_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Resource_Manager _Instance { get; private set; } = null;

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

    private int metalAmount = 0;
    private int electricityAmount = 0;
    private int platinumAmount = 0;
    public Text metalText;
    public Text electricityText;
    public Text platinumText;
    private string sceneName = "";

    #region Memory Variables
    private int memoryAmount = 0;
    private int maxMemoryAmount = 0;
    public Text memoryText;
    public GameObject UnitParent;
    public GameObject BuildingParent;
    private List<GameObject> unitList;
    private List<GameObject> buildingList;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        unitList = new List<GameObject>();
        buildingList = new List<GameObject>();

        sceneName = SceneManager.GetActiveScene().name;
        SetUpStartingResources();
        UpdateTexts();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMemory();
    }

    private void SetUpStartingResources()
    {
        if (sceneName.Equals(SceneNameList.TUTORIAL2_SCENE))
        {
            metalAmount = ResourceAmountList.SPECIAL_METAL_STARTING_AMOUNT;
            electricityAmount = ResourceAmountList.SPECIAL_STATIC_STARTING_AMOUNT;
            platinumAmount = ResourceAmountList.SPECIAL_PLATINUM_STARTING_AMOUNT;
        }
        else
        {
            metalAmount = ResourceAmountList.METAL_STARTING_AMOUNT;
            electricityAmount = ResourceAmountList.STATIC_STARTING_AMOUNT;
            platinumAmount = ResourceAmountList.PLATINUM_STARTING_AMOUNT;
        }

        maxMemoryAmount = ResourceAmountList.STARTING_MEMORY;
    }

    private void UpdateTexts()
    {
        metalText.text = metalAmount.ToString();
        electricityText.text = electricityAmount.ToString();
        platinumText.text = platinumAmount.ToString();
    }

    public void SubtractResources(int metal, int electricity, int platinum)
    {
        metalAmount -= metal;
        electricityAmount -= electricity;
        platinumAmount -= platinum;
        UpdateTexts();
    }

    public void AddResources(int metal, int electricity, int platinum)
    {
        metalAmount += metal;
        electricityAmount += electricity;
        platinumAmount += platinum;
        UpdateTexts();
    }

    public int GetMetalAmount()
    {
        return metalAmount;
    }
    public int GetElectricyAmount()
    {
        return electricityAmount;
    }
    public int GetPlatinumAmount()
    {
        return platinumAmount;
    }

    public int GetMemoryAmount()
    {
        return memoryAmount;
    }

    private void CheckMemory()
    {
        CalculateMemory();
        memoryText.text = memoryAmount.ToString() + "/" + maxMemoryAmount.ToString();
        if (HaveAvailableMemory())
        {
            memoryText.color = Color.black;
        }
        else
        {
            memoryText.color = Color.red;
        }
    }

    public bool HaveAvailableMemory()
    {
        return memoryAmount < maxMemoryAmount;
    }


    private void FillLists()
    {
        unitList.Clear();
        for (int i = 0; i < UnitParent.transform.childCount; i++)
        {
            Transform child = UnitParent.transform.GetChild(i);
            if (!child.tag.Equals(TagList.ENEMY_UNIT))
            {
                unitList.Add(child.gameObject);
            }
        }

        buildingList.Clear();
        for (int i = 0; i < BuildingParent.transform.childCount; i++)
        {
            Transform child = BuildingParent.transform.GetChild(i);
            if (!child.tag.Equals(TagList.ENEMY_BUILDING))
            {
                buildingList.Add(child.gameObject);
            }
        }
    }

    private void CalculateMemory()
    {
        FillLists();
        int tempMemMaxAmount = ResourceAmountList.STARTING_MEMORY;

        foreach(GameObject building in buildingList)
        {
            if (building.tag.Equals(TagList.OUTPOST))
            {
                tempMemMaxAmount += ResourceAmountList.OUTPOST_MEMORY;
            }
        }

        memoryAmount = unitList.Count;
        maxMemoryAmount = tempMemMaxAmount;
    }
}
