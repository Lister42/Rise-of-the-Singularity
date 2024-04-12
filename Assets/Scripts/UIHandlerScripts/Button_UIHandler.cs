using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Button_UIHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    private Build_Manager _buildManager;
    private Setup_Manager _setup_Manager;
    private Click_Manager _clickManager;
    public GameObject _informationPanel;
    public Text _titleText;
    public Text _resourceCostText;
    public Text _descriptionText;
    private GameObject _resourceTexts;

    private void Start()
    {
        _buildManager = Build_Manager._Instance;
        _setup_Manager = Setup_Manager._Instance;
        _clickManager = Click_Manager._Instance;
        _resourceTexts = _setup_Manager.GetResourceTexts();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _informationPanel.SetActive(true);
        _resourceTexts.SetActive(false);
        _clickManager.ResetAllUI();
        SetRightText(gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _informationPanel.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        string buttonName = gameObject.name;

        if (buttonName.Equals(ButtonNameList.MINE_BUTTON) && _buildManager.HaveRequiredResources(
            ResourceCostList.MINE_METAL_COST, 
            ResourceCostList.MINE_ELECTRICITY_COST, 
            ResourceCostList.MINE_PLATINUM_COST))
        {
            _informationPanel.SetActive(false);
        }
        else if (buttonName.Equals(ButtonNameList.STATIC_COLLECTOR_BUTTON) && _buildManager.HaveRequiredResources(
            ResourceCostList.STATIC_COLLECTOR_METAL_COST,
            ResourceCostList.STATIC_COLLECTOR_ELECTRICITY_COST,
            ResourceCostList.STATIC_COLLECTOR_PLATINUM_COST))
        {
            _informationPanel.SetActive(false);
        }
        else if (buttonName.Equals(ButtonNameList.FACTORY_BUTTON) && _buildManager.HaveRequiredResources(
           ResourceCostList.FACTORY_METAL_COST,
           ResourceCostList.FACTORY_ELECTRICITY_COST,
           ResourceCostList.FACTORY_PLATINUM_COST))
        {
            _informationPanel.SetActive(false);
        }
        else if (buttonName.Equals(ButtonNameList.TURRET_BUTTON) && _buildManager.HaveRequiredResources(
           ResourceCostList.TURRET_METAL_COST,
           ResourceCostList.TURRET_ELECTRICITY_COST,
           ResourceCostList.TURRET_PLATINUM_COST))
        {
            _informationPanel.SetActive(false);
        }
    }

    private void SetRightText(string buttonName)
    {
        if (buttonName.Equals(ButtonNameList.MINE_BUTTON))
        {
            _titleText.text = BuildingNameList.MINE;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.MINE_METAL_COST, 
                ResourceCostList.MINE_ELECTRICITY_COST, 
                ResourceCostList.MINE_PLATINUM_COST);
            _descriptionText.text = DescriptionList.MINE_DESCRIPTION;
        }
        else if (buttonName.Equals(ButtonNameList.STATIC_COLLECTOR_BUTTON))
        {
            _titleText.text = BuildingNameList.STATIC_COLLECTOR;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.STATIC_COLLECTOR_METAL_COST,
                ResourceCostList.STATIC_COLLECTOR_ELECTRICITY_COST,
                ResourceCostList.STATIC_COLLECTOR_PLATINUM_COST);
            _descriptionText.text = DescriptionList.STATIC_COLLECTOR_DESCRIPTION;
        }
        else if (buttonName.Equals(ButtonNameList.FACTORY_BUTTON))
        {
            _titleText.text = BuildingNameList.FACTORY;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.FACTORY_METAL_COST,
                ResourceCostList.FACTORY_ELECTRICITY_COST,
                ResourceCostList.FACTORY_PLATINUM_COST);
            _descriptionText.text = DescriptionList.FACTORY_DESCRIPTION;
        }
        else if (buttonName.Equals(ButtonNameList.TURRET_BUTTON))
        {
            _titleText.text = BuildingNameList.TURRET;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.TURRET_METAL_COST,
                ResourceCostList.TURRET_ELECTRICITY_COST,
                ResourceCostList.TURRET_PLATINUM_COST);
            _descriptionText.text = DescriptionList.TURRET_DESCRIPTION;
        }
        else if (buttonName.Equals(ButtonNameList.WORKER_BUTTON))
        {
            _titleText.text = UnitTechNameList.WORKER_TECH;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.WORKER_METAL_COST,
                ResourceCostList.WORKER_ELECTRICITY_COST,
                ResourceCostList.WORKER_PLATINUM_COST);
            _descriptionText.text = DescriptionList.WORKER_DESCRIPTION;
        }
        else if (buttonName.Equals(ButtonNameList.MELEE_UNIT_BUTTON))
        {
            _titleText.text = UnitTechNameList.MELEE_TECH;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.MELEE_METAL_COST,
                ResourceCostList.MELEE_ELECTRICITY_COST,
                ResourceCostList.MELEE_PLATINUM_COST);
            _descriptionText.text = DescriptionList.MELEE_DESCRIPTION;
        }
        else if (buttonName.Equals(ButtonNameList.RANGED_UNIT_BUTTON))
        {
            _titleText.text = UnitTechNameList.RANGED_TECH;
            _resourceCostText.text = CreateResourceString(
                ResourceCostList.RANGED_METAL_COST,
                ResourceCostList.RANGED_ELECTRICITY_COST,
                ResourceCostList.RANGED_PLATINUM_COST);
            _descriptionText.text = DescriptionList.RANGED_DESCRIPTION;
        }
    }

    private string CreateResourceString(int metal, int electricity, int platinum)
    {
        return "Metal: " + metal + ", Electricity: " + electricity + ", Platinum: " + platinum;
    }
}
