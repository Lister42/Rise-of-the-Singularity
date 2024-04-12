using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Achievement_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Achievement_Manager _Instance { get; private set; } = null;

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

    private AchievementHelperClass _aHelper;
    private Resource_Manager _resource_Manager;

    private bool inGame = false;
    private bool goodUnitDied = false;
    private string sceneName = "";

    // Start is called before the first frame update
    void Start()
    {
        _aHelper = AchievementHelperClass._Instance;
        _resource_Manager = Resource_Manager._Instance;

        sceneName = SceneManager.GetActiveScene().name;
        inGame = sceneName != SceneNameList.MAINMENU_SCENE && sceneName != SceneNameList.TUTORIAL1_SCENE && sceneName != SceneNameList.TUTORIAL2_SCENE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inGame)
        {
            CheckAchievements();
        }
    }

    public void CheckEndGameAchievements(bool wonGame)
    {
        if (wonGame)
        {
            CompleteAchievement(AchievementInformationList.SAVIOR_KEY);

            if(!goodUnitDied)
            {
                CompleteAchievement(AchievementInformationList.ULTIMATE_SAVIOR_KEY);
            }

            if (sceneName.Equals(SceneNameList.FREEPLAY_SCENE))
            {
                SetFreeplayAchievementComplete();
            }
            else if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
            {
                SetScenario1AchievementComplete();
            }
            else if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
            {
                SetScenario2AchievementComplete();
            }
        }
        else
        {

        }
    }

    public void SetTutorial1AchievementComplete()
    {
        CompleteAchievement(AchievementInformationList.APPRENTICE_KEY);
        CheckBothTutorial();
        CheckAllMaps();
    }

    public void SetTutorial2AchievementComplete()
    {
        CompleteAchievement(AchievementInformationList.SQUIRE_KEY);
        CheckBothTutorial();
        CheckAllMaps();
    }

    public void SetScenario1AchievementComplete()
    {
        CompleteAchievement(AchievementInformationList.CAVALIER_KEY);
        CheckAllMaps();
    }

    public void SetScenario2AchievementComplete()
    {
        CompleteAchievement(AchievementInformationList.PALADIN_KEY);
        CheckAllMaps();
    }

    public void SetFreeplayAchievementComplete()
    {
        CompleteAchievement(AchievementInformationList.CATAPHRACT_KEY);
        CheckAllMaps();
    }

    private void CheckBothTutorial()
    {
        int t1 = PlayerPrefs.GetInt(AchievementInformationList.APPRENTICE_KEY + "_C", 0);
        int t2 = PlayerPrefs.GetInt(AchievementInformationList.SQUIRE_KEY + "_C", 0);
        if (t1 == 1 && t2 == 1)
        {
            CompleteAchievement(AchievementInformationList.KNIGHT_KEY);
        }
    }

    private void CheckAllMaps()
    {
        int t1 = PlayerPrefs.GetInt(AchievementInformationList.APPRENTICE_KEY + "_C", 0);
        int t2 = PlayerPrefs.GetInt(AchievementInformationList.SQUIRE_KEY + "_C", 0);
        int s1 = PlayerPrefs.GetInt(AchievementInformationList.CAVALIER_KEY + "_C", 0);
        int s2 = PlayerPrefs.GetInt(AchievementInformationList.PALADIN_KEY + "_C", 0);
        int fp = PlayerPrefs.GetInt(AchievementInformationList.CATAPHRACT_KEY + "_C", 0);
        if (t1 == 1 && t2 == 1 && s1 == 1 && s2 == 1 && fp == 1)
        {
            CompleteAchievement(AchievementInformationList.KING_KEY);
        }
    }

    public void CompleteAchievement(string key)
    {
        List<AchievementObject> tempAchievmentList = _aHelper.GetAchievementList();
        for (int i = 0; i < _aHelper.GetNumAchievements(); i++)
        {
            AchievementObject tempAchievementObject = tempAchievmentList[i];

            if (tempAchievementObject.GetKey().Equals(key) && tempAchievementObject.GetCompleted() == 0)
            {
                tempAchievementObject.SetCompleted(1);
            }
        }
    }
    
    private void CheckAchievements()
    {
        List<AchievementObject> tempAchievmentList = _aHelper.GetAchievementList();
        for (int i = 0; i < _aHelper.GetNumAchievements(); i++)
        {
            AchievementObject tempAchievementObject = tempAchievmentList[i];

            if (tempAchievementObject.GetCompleted() == 0)
            {
                if (tempAchievementObject.GetKey().Equals(AchievementInformationList.METAL_MASTER_KEY))
                {
                    if (_resource_Manager.GetMetalAmount() >= AchievementInformationList.METAL_MASTER_AMOUNT)
                    {
                        tempAchievementObject.SetCompleted(1);
                    }
                }
                else if (tempAchievementObject.GetKey().Equals(AchievementInformationList.STATIC_SURVEYOR_KEY))
                {
                    if (_resource_Manager.GetElectricyAmount() >= AchievementInformationList.STATIC_SURVEYOR_AMOUNT)
                    {
                        tempAchievementObject.SetCompleted(1);
                    }
                }
                else if (tempAchievementObject.GetKey().Equals(AchievementInformationList.PLATINUM_PURVEYOR_KEY))
                {
                    if (_resource_Manager.GetPlatinumAmount() >= AchievementInformationList.PLATINUM_PURVEYOR_AMOUNT)
                    {
                        tempAchievementObject.SetCompleted(1);
                    }
                }
            }
        }
    }

    private void SaveAchievements()
    {
        List<AchievementObject> tempAchievmentList = _aHelper.GetAchievementList();
        List<string> tempKeyList = _aHelper.GetKeyList();
        for (int i = 0; i < _aHelper.GetNumAchievements(); i++)
        {
            PlayerPrefs.SetInt(tempKeyList[i] + "_C", tempAchievmentList[i].GetCompleted());
        }
    }

    private void OnDestroy()
    {
        SaveAchievements();
    }


    public void SetGoodUnitDied(bool died)
    {
        goodUnitDied = died;
    }
}
