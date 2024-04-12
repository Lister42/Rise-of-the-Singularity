using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setup_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Setup_Manager _Instance { get; private set; } = null;

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

    #region Secondary Panel Variables
    public GameObject _secondaryPanel;
    public Text _titleText;
    public Text _headerText;
    public Text _bodyText;
    public GameObject _resourceTexts;
    public Text _metalText;
    public Text _electricityText;
    public Text _platinumText;
    #endregion

    #region Build Site Variables
    public GameObject _buildSitePanel;
    public Slider _buildSiteSlider;
    public Text _buildSiteSliderText;
    public Image _buildSiteImage;
    #endregion

    #region Core Variables
    public Slider _coreSlider;
    public Text _coreSliderText;
    public Image _coreImage1;
    public Image _coreImage2;
    public Image _coreImage3;
    public Image _coreImage4;
    public Image _coreImage5;
    #endregion

    #region Factory Variables
    public Slider _factorySlider;
    public Text _factorySliderText;
    public Image _factoryImage1;
    public Image _factoryImage2;
    public Image _factoryImage3;
    public Image _factoryImage4;
    public Image _factoryImage5;
    #endregion

    public GameObject _unitParent;
    public GameObject _buildingParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupGame()
    {

    }

    public GameObject GetSecondaryPanel()
    {
        return _secondaryPanel;
    }
    public Text GetTitleText()
    {
        return _titleText;
    }
    public Text GetHeaderText()
    {
        return _headerText;
    }
    public Text GetBodyText()
    {
        return _bodyText;
    }
    
    public GameObject GetBuildSitePanel()
    {
        return _buildSitePanel;
    }
    public Slider GetBuildSiteSlider()
    {
        return _buildSiteSlider;
    }
    public Text GetBuildSiteSliderText()
    {
        return _buildSiteSliderText;
    }
    public Image GetBuildSiteImage()
    {
        return _buildSiteImage;
    }

    public Slider GetCoreSlider()
    {
        return _coreSlider;
    }
    public Text GetCoreSliderText()
    {
        return _coreSliderText;
    }
    public Image GetCoreImage1()
    {
        return _coreImage1;
    }
    public Image GetCoreImage2()
    {
        return _coreImage2;
    }
    public Image GetCoreImage3()
    {
        return _coreImage3;
    }
    public Image GetCoreImage4()
    {
        return _coreImage4;
    }
    public Image GetCoreImage5()
    {
        return _coreImage5;
    }

    public Slider GetFactorySlider()
    {
        return _factorySlider;
    }
    public Text GetFactorySliderText()
    {
        return _factorySliderText;
    }
    public Image GetFactoryImage1()
    {
        return _factoryImage1;
    }
    public Image GetFactoryImage2()
    {
        return _factoryImage2;
    }
    public Image GetFactoryImage3()
    {
        return _factoryImage3;
    }
    public Image GetFactoryImage4()
    {
        return _factoryImage4;
    }
    public Image GetFactoryImage5()
    {
        return _factoryImage5;
    }

    public GameObject GetResourceTexts()
    {
        return _resourceTexts;
    }
    public Text GetMetalText()
    {
        return _metalText;
    }
    public Text GetElectricityText()
    {
        return _electricityText;
    }
    public Text GetPlatinumText()
    {
        return _platinumText;
    }

    public GameObject GetUnitParent()
    {
        return _unitParent;
    }
    public GameObject GetBuildingParent()
    {
        return _buildingParent;
    }
}
