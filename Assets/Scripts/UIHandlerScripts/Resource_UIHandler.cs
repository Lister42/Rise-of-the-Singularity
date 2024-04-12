using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource_UIHandler : MonoBehaviour
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
        SetRightText(gameObject.tag);
    }

    private void SetRightText(string buildingTag)
    {
        if (buildingTag.Equals(TagList.METAL))
        {
            _titleText.text = ResourceNameList.METAL;
            MetalScript metalScript = gameObject.GetComponent<MetalScript>();
            _hitpointText.text = CreateResourceString(ResourceNameList.METAL, metalScript.GetCurrentResourceAmount(), metalScript.GetMaxResourceAmount());
            _descriptionText.text = DescriptionList.METAL_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.STATIC))
        {
            _titleText.text = ResourceNameList.STATIC;
            StaticScript staticScript = gameObject.GetComponent<StaticScript>();
            _hitpointText.text = CreateResourceString(ResourceNameList.STATIC, staticScript.GetCurrentResourceAmount(), staticScript.GetMaxResourceAmount());
            _descriptionText.text = DescriptionList.STATIC_DESCRIPTION;
        }
        else if (buildingTag.Equals(TagList.PLATINUM))
        {
            _titleText.text = ResourceNameList.PLATINUM;
            PlatinumScript platinumScript = gameObject.GetComponent<PlatinumScript>();
            _hitpointText.text = CreateResourceString(ResourceNameList.PLATINUM, platinumScript.GetCurrentResourceAmount(), platinumScript.GetMaxResourceAmount());
            _descriptionText.text = DescriptionList.PLATINUM_DESCRIPTION;
        }
    }

    private string CreateResourceString(string name, int currentResources, int maxResources)
    {
        return name + ": " + currentResources + "/" + maxResources;
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
