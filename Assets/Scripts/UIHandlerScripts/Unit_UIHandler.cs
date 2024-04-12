using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_UIHandler : MonoBehaviour
{
    private Setup_Manager _setupManager;
    private GameObject _informationPanel;
    private Text _titleText;
    private Text _hitpointText;
    private Text _descriptionText;
    private GameObject _resourceTexts;
    private Text _metalText;
    private Text _electricityText;
    private Text _platinumText;
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
        _metalText = _setupManager.GetMetalText();
        _electricityText = _setupManager.GetElectricityText();
        _platinumText = _setupManager.GetPlatinumText();
}

    public void Selected()
    {
        _informationPanel.SetActive(true);
        _resourceTexts.SetActive(false);
        SetRightText(gameObject.transform.parent.gameObject.tag);
    }

    private void SetRightText(string unitTag)
    {
        if (unitTag.Equals(TagList.WORKER))
        {
            _resourceTexts.SetActive(true);
            _titleText.text = UnitTechNameList.WORKER_TECH;
            UnitScript unitScript = gameObject.GetComponent<UnitScript>();
            _hitpointText.text = CreateHealthString(unitScript.GetCurrentHealth(), unitScript.GetMaxHealth());
            FillResourceStrings(unitScript.GetMetalAmount(), unitScript.GetElectricityAmount(), unitScript.GetPlatinumAmount());
            _descriptionText.text = DescriptionList.WORKER_DESCRIPTION;
        }
        else if (unitTag.Equals(TagList.MELEE_UNIT))
        {
            _titleText.text = UnitTechNameList.MELEE_TECH;
            UnitScript meleeScript = gameObject.GetComponent<UnitScript>();
            _hitpointText.text = CreateHealthString(meleeScript.GetCurrentHealth(), meleeScript.GetMaxHealth());
            _descriptionText.text = DescriptionList.MELEE_DESCRIPTION;
        }
        else if (unitTag.Equals(TagList.RANGED_UNIT))
        {
            _titleText.text = UnitTechNameList.RANGED_TECH;
            UnitScript rangedScript = gameObject.GetComponent<UnitScript>();
            _hitpointText.text = CreateHealthString(rangedScript.GetCurrentHealth(), rangedScript.GetMaxHealth());
            _descriptionText.text = DescriptionList.RANGED_DESCRIPTION;
        }
        else if (unitTag.Equals(TagList.ENEMY_UNIT)) // for the enemy buildings now
        {
            string specificTag = gameObject.tag;
            if (specificTag.Equals(TagList.MELEE_UNIT))
            {
                _titleText.text = "Enemy " + UnitNameList.MELEE + " Unit";
                EnemyUnitScript enemyUnitScript = gameObject.GetComponent<EnemyUnitScript>();
                _hitpointText.text = CreateHealthString(enemyUnitScript.GetCurrentHealth(), enemyUnitScript.GetMaxHealth());
                _descriptionText.text = DescriptionList.ENEMY_MELEE_DESCRIPTION;
            }
            else if (specificTag.Equals(TagList.RANGED_UNIT))
            {
                _titleText.text = "Enemy " + UnitNameList.RANGED + " Unit";
                EnemyUnitScript enemyUnitScript = gameObject.GetComponent<EnemyUnitScript>();
                _hitpointText.text = CreateHealthString(enemyUnitScript.GetCurrentHealth(), enemyUnitScript.GetMaxHealth());
                _descriptionText.text = DescriptionList.ENEMY_RANGED_DESCRIPTION;
            }
        }
    }

    private string CreateHealthString(int currentHealth, int maxHealth)
    {
        
        return "Health: " + currentHealth + "/" + maxHealth;
    }

    private void FillResourceStrings(int metal, int electricity, int platinum)
    {
        _metalText.text = "Metal: " + metal + "/" + 10;
        _electricityText.text = "Electricity: " + electricity + "/" + 10;
        _platinumText.text = "Platinum: " + platinum + "/" + 10;
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
