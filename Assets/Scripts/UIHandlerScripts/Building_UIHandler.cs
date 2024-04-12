using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building_UIHandler : MonoBehaviour
{
    private Setup_Manager _setupManager;
    private GameObject _informationPanel;
    private Text _titleText;
    private Text _hitpointText;
    private Text _descriptionText;
    private GameObject _resourceTexts;
    private bool isVisible = false;

    private void Start()
    {
        _setupManager = Setup_Manager._Instance;
        LoadVariables();
    }

    private void Update()
    {
        if (isVisible)
        {
            Selected();
        }
    }

    private void LoadVariables()
    {
        _informationPanel = _setupManager.GetSecondaryPanel();
        _titleText = _setupManager.GetTitleText();
        _hitpointText = _setupManager.GetHeaderText();
        _descriptionText = _setupManager.GetBodyText();
        _resourceTexts = _setupManager.GetResourceTexts();
    }

    public void Selected()
    {
        _informationPanel.SetActive(true);
        _resourceTexts.SetActive(false);
        SetRightText(gameObject.transform.parent.tag);
    }

    private void SetRightText(string buildingTag)
    {
        if (buildingTag.Equals(TagList.CORE))
        {
            _titleText.text = BuildingNameList.CORE;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.CORE_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.OUTPOST))
        {
            _titleText.text = BuildingNameList.OUTPOST;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.OUTPOST_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.BUILD_SITE))
        {
            BuildSiteScript buildSiteScript = gameObject.GetComponent<BuildSiteScript>();
            string buildTag = buildSiteScript.GetTypePrefab().tag;
            string titleText = BuildingNameList.BUILD_SITE + " (";

            if (buildTag.Equals(TagList.MINE))
            {
                titleText += BuildingNameList.MINE + ")";
            }
            else if (buildTag.Equals(TagList.OUTPOST))
            {
                titleText += BuildingNameList.OUTPOST + ")";
            }
            else if (buildTag.Equals(TagList.STATIC_COLLECTOR))
            {
                titleText += BuildingNameList.STATIC_COLLECTOR + ")";
            }
            else if (buildTag.Equals(TagList.FACTORY))
            {
                titleText += BuildingNameList.FACTORY + ")";
            }
            else if (buildTag.Equals(TagList.TURRET))
            {
                titleText += BuildingNameList.TURRET + ")";
            }
            _titleText.text = titleText;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.BUILD_SITE_DESCRIPTION;
        }
        else if(buildingTag.Equals(TagList.MINE))
        {
            _titleText.text = BuildingNameList.MINE;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.MINE_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.STATIC_COLLECTOR))
        {
            _titleText.text = BuildingNameList.STATIC_COLLECTOR;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.STATIC_COLLECTOR_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.FACTORY))
        {
            _titleText.text = BuildingNameList.FACTORY;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.FACTORY_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.TURRET))
        {
            _titleText.text = BuildingNameList.TURRET;
            GoodBuildingScript goodBuildingScript = gameObject.GetComponent<GoodBuildingScript>();
            _hitpointText.text = CreateHitpointString(goodBuildingScript.GetCurrentHitpoints(), goodBuildingScript.GetMaxHitpoints());
            _descriptionText.text = DescriptionList.TURRET_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.ENEMY_BUILDING)) // now for the enemy buildings
        {
            string specificTag = gameObject.tag;
            if (specificTag.Equals(TagList.CORE))
            {
                _titleText.text = "Enemy " + BuildingNameList.CORE;
                EnemyBuildingScript enemyBuildingScript = gameObject.GetComponent<EnemyBuildingScript>();
                _hitpointText.text = CreateHitpointString(enemyBuildingScript.GetCurrentHitpoints(), enemyBuildingScript.GetMaxHitpoints());
                _descriptionText.text = DescriptionList.ENEMY_CORE_DESCRIPTION;
            }
            else if (specificTag.Equals(TagList.OUTPOST))
            {
                _titleText.text = "Enemy " + BuildingNameList.OUTPOST;
                EnemyBuildingScript enemyBuildingScript = gameObject.GetComponent<EnemyBuildingScript>();
                _hitpointText.text = CreateHitpointString(enemyBuildingScript.GetCurrentHitpoints(), enemyBuildingScript.GetMaxHitpoints());
                _descriptionText.text = DescriptionList.ENEMY_OUTPOST_DESCRIPTION;
            }
            else if (specificTag.Equals(TagList.MINE))
            {
                _titleText.text = "Enemy " + BuildingNameList.MINE;
                EnemyBuildingScript enemyBuildingScript = gameObject.GetComponent<EnemyBuildingScript>();
                _hitpointText.text = CreateHitpointString(enemyBuildingScript.GetCurrentHitpoints(), enemyBuildingScript.GetMaxHitpoints());
                _descriptionText.text = DescriptionList.ENEMY_MINE_DESCRIPTION;
            }
            else if (specificTag.Equals(TagList.STATIC_COLLECTOR))
            {
                _titleText.text = "Enemy " + BuildingNameList.STATIC_COLLECTOR;
                EnemyBuildingScript enemyBuildingScript = gameObject.GetComponent<EnemyBuildingScript>();
                _hitpointText.text = CreateHitpointString(enemyBuildingScript.GetCurrentHitpoints(), enemyBuildingScript.GetMaxHitpoints());
                _descriptionText.text = DescriptionList.ENEMY_STATIC_COLLECTOR_DESCRIPTION;
            }
            else if (specificTag.Equals(TagList.FACTORY))
            {
                _titleText.text = "Enemy " + BuildingNameList.FACTORY;
                EnemyBuildingScript enemyBuildingScript = gameObject.GetComponent<EnemyBuildingScript>();
                _hitpointText.text = CreateHitpointString(enemyBuildingScript.GetCurrentHitpoints(), enemyBuildingScript.GetMaxHitpoints());
                _descriptionText.text = DescriptionList.ENEMY_FACTORY_DESCRIPTION;
            }
            else if (specificTag.Equals(TagList.TURRET))
            {
                _titleText.text = "Enemy " + BuildingNameList.TURRET;
                EnemyBuildingScript enemyBuildingScript = gameObject.GetComponent<EnemyBuildingScript>();
                _hitpointText.text = CreateHitpointString(enemyBuildingScript.GetCurrentHitpoints(), enemyBuildingScript.GetMaxHitpoints());
                _descriptionText.text = DescriptionList.ENEMY_TURRET_DESCRIPTION;
            }
        }
    }

    private string CreateHitpointString(int currentHitpoints, int maxHitpoints)
    {
        return "Hitpoints: " + currentHitpoints + "/" + maxHitpoints;
    }

    public void SetIsVisible(bool v)
    {
        isVisible = v;
    }

    private void OnDestroy()
    {
        if (_informationPanel)
        {
            _informationPanel.SetActive(false);
        }
    }
}
