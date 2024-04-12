using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreation_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static UnitCreation_Manager _Instance { get; private set; } = null;

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

    private Click_Manager _clickManager;
    private Resource_Manager _resourceManager;
    public CoreScript _coreScript;

    private int lastMetal = 0;
    private int lastElectricity = 0;
    private int lastPlatinum = 0;

    private string lastTag = "";

    // Start is called before the first frame update
    void Start()
    {
        _clickManager = Click_Manager._Instance;
        _resourceManager = Resource_Manager._Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddUnit(string unitTag)
    {
        if (unitTag.Equals(TagList.WORKER))
        {
            lastMetal = ResourceCostList.WORKER_METAL_COST;
            lastElectricity = ResourceCostList.WORKER_ELECTRICITY_COST;
            lastPlatinum = ResourceCostList.WORKER_PLATINUM_COST;
            if (HaveRequiredResources())
            {
                _coreScript.AddUnit(TagList.WORKER);
            }
        }
        else if (unitTag.Equals(TagList.MELEE_UNIT))
        {
            FactoryScript factoryScript = _clickManager.GetLastFactory();
            if (factoryScript)
            {
                lastMetal = ResourceCostList.MELEE_METAL_COST;
                lastElectricity = ResourceCostList.MELEE_ELECTRICITY_COST;
                lastPlatinum = ResourceCostList.MELEE_PLATINUM_COST;
                if (HaveRequiredResources())
                {
                    factoryScript.AddUnit(TagList.MELEE_UNIT);
                }
            }
        }
        else if (unitTag.Equals(TagList.RANGED_UNIT))
        {
            FactoryScript factoryScript = _clickManager.GetLastFactory();
            if (factoryScript)
            {
                lastMetal = ResourceCostList.RANGED_METAL_COST;
                lastElectricity = ResourceCostList.RANGED_ELECTRICITY_COST;
                lastPlatinum = ResourceCostList.RANGED_PLATINUM_COST;
                if (HaveRequiredResources())
                {
                    factoryScript.AddUnit(TagList.RANGED_UNIT);
                }
            }
        }
        else
        {
            print("Unit tag not implemented yet");
        }
    }

    private bool HaveRequiredResources()
    {
        return (lastMetal <= _resourceManager.GetMetalAmount()
            && lastElectricity <= _resourceManager.GetElectricyAmount()
            && lastPlatinum <= _resourceManager.GetPlatinumAmount());
    }

    public bool HaveRequiredResources(int metal, int electricity, int platinum)
    {
        return (metal <= _resourceManager.GetMetalAmount()
            && electricity <= _resourceManager.GetElectricyAmount()
            && platinum <= _resourceManager.GetPlatinumAmount());
    }

    public void SetLastTag(string tag)
    {
        lastTag = tag;
    }

    public void RemoveUnit(int index)
    {
        string buildingTag = lastTag;
        if (buildingTag.Equals(TagList.CORE))
        {
            _coreScript.RemoveUnit(index);
        }
        else if (buildingTag.Equals(TagList.FACTORY))
        {
            FactoryScript factoryScript = _clickManager.GetLastFactory();
            if (factoryScript)
            {
                factoryScript.RemoveUnit(index);
            }
        }
        else
        {
            print("Building tag " + buildingTag + " not implemented yet");
        }
    }
}
