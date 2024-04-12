using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHelperClass : MonoBehaviour
{
    #region Singleton Creation
    public static AchievementHelperClass _Instance { get; private set; } = null;

    void Awake()
    {
        if (_Instance != null && _Instance != this)
        {
            Destroy(this);
            return;
        }
        _Instance = this;
        LoadLists();
    }
    #endregion

    private List<string> keyList;
    private List<string> difficultyList;
    private List<AchievementObject> achievementList;
    private List<AchievementObject> easyList;
    private List<AchievementObject> mediumList;
    private List<AchievementObject> hardList;

    // Load is called in the Awake above
    void LoadLists()
    {
        keyList = LoadKeyList();
        difficultyList = LoadDifficultyList();
        achievementList = LoadAchievementList(keyList.Count);
        easyList = LoadAchievementList(keyList.Count, "e");
        mediumList = LoadAchievementList(keyList.Count, "m");
        hardList = LoadAchievementList(keyList.Count, "h");
    }

    private List<string> LoadKeyList()
    {
        List<string> tempList = new List<string>();
        tempList.Add(AchievementInformationList.APPRENTICE_KEY);
        tempList.Add(AchievementInformationList.SQUIRE_KEY);
        tempList.Add(AchievementInformationList.KNIGHT_KEY);
        tempList.Add(AchievementInformationList.CAVALIER_KEY);
        tempList.Add(AchievementInformationList.PALADIN_KEY);
        tempList.Add(AchievementInformationList.CATAPHRACT_KEY);
        tempList.Add(AchievementInformationList.KING_KEY);
        tempList.Add(AchievementInformationList.METAL_MASTER_KEY);
        tempList.Add(AchievementInformationList.STATIC_SURVEYOR_KEY);
        tempList.Add(AchievementInformationList.PLATINUM_PURVEYOR_KEY);
        tempList.Add(AchievementInformationList.SAVIOR_KEY);
        tempList.Add(AchievementInformationList.ULTIMATE_SAVIOR_KEY);

        return tempList;
    }

    private List<AchievementObject> LoadAchievementList(int numAchievements)
    {
        List<AchievementObject> tempList = new List<AchievementObject>();
        for (int i = 0; i < numAchievements; i++)
        {
            string description = GetRightDescription(keyList[i]);
            int completed = PlayerPrefs.GetInt(keyList[i] + "_C", 0);

            tempList.Add(new AchievementObject(keyList[i], description, completed));
        }

        return tempList;
    }

    private List<AchievementObject> LoadAchievementList(int numAchievements, string diff)
    {
        List<AchievementObject> tempList = new List<AchievementObject>();
        for (int i = 0; i < numAchievements; i++)
        {
            if (diff.Equals(difficultyList[i]))
            {
                string description = GetRightDescription(keyList[i]);
                int completed = PlayerPrefs.GetInt(keyList[i] + "_C", 0);

                tempList.Add(new AchievementObject(keyList[i], description, completed));
            }
        }

        return tempList;
    }

    private List<string> LoadDifficultyList()
    {
        List<string> tempList = new List<string>();
        tempList.Add(AchievementInformationList.APPRENTICE_DIFFICULTY);
        tempList.Add(AchievementInformationList.SQUIRE_DIFFICULTY);
        tempList.Add(AchievementInformationList.KNIGHT_DIFFICULTY);
        tempList.Add(AchievementInformationList.CAVALIER_DIFFICULTY);
        tempList.Add(AchievementInformationList.PALADIN_DIFFICULTY);
        tempList.Add(AchievementInformationList.CATAPHRACT_DIFFICULTY);
        tempList.Add(AchievementInformationList.KING_DIFFICULTY);
        tempList.Add(AchievementInformationList.METAL_MASTER_DIFFICULTY);
        tempList.Add(AchievementInformationList.STATIC_SURVEYOR_DIFFICULTY);
        tempList.Add(AchievementInformationList.PLATINUM_PURVEYOR_DIFFICULTY);
        tempList.Add(AchievementInformationList.SAVIOR_DIFFICULTY);
        tempList.Add(AchievementInformationList.ULTIMATE_SAVIOR_DIFFICULTY);

        return tempList;
    }

    private string GetRightDescription(string key)
    {
        if (key.Equals(AchievementInformationList.METAL_MASTER_KEY))
        {
            return AchievementInformationList.METAL_MASTER_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.STATIC_SURVEYOR_KEY))
        {
            return AchievementInformationList.STATIC_SURVEYOR_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.PLATINUM_PURVEYOR_KEY))
        {
            return AchievementInformationList.PLATINUM_PURVEYOR_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.SAVIOR_KEY))
        {
            return AchievementInformationList.SAVIOR_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.ULTIMATE_SAVIOR_KEY))
        {
            return AchievementInformationList.ULTIMATE_SAVIOR_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.APPRENTICE_KEY))
        {
            return AchievementInformationList.APPRENTICE_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.SQUIRE_KEY))
        {
            return AchievementInformationList.SQUIRE_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.KNIGHT_KEY))
        {
            return AchievementInformationList.KNIGHT_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.CAVALIER_KEY))
        {
            return AchievementInformationList.CAVALIER_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.PALADIN_KEY))
        {
            return AchievementInformationList.PALADIN_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.KING_KEY))
        {
            return AchievementInformationList.KING_DESCRIPTION;
        }
        else if (key.Equals(AchievementInformationList.CATAPHRACT_KEY))
        {
            return AchievementInformationList.CATAPHRACT_DESCRIPTION;
        }
        else
        {
            return "Placeholder Description";
        }
    }

    public List<AchievementObject> GetAchievementList()
    {
        return achievementList;
    }
    public List<AchievementObject> GetEasyList()
    {
        return easyList;
    }
    public List<AchievementObject> GetMediumList()
    {
        return mediumList;
    }
    public List<AchievementObject> GetHardList()
    {
        return hardList;
    }
    public List<string> GetKeyList()
    {
        return keyList;
    }

    public int GetNumAchievements()
    {
        return achievementList.Count;
    }
    public int GetNumEasy()
    {
        return easyList.Count;
    }
    public int GetNumMedium()
    {
        return mediumList.Count;
    }
    public int GetNumHard()
    {
        return hardList.Count;
    }
}
