using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementListMaker : MonoBehaviour
{
    private AchievementHelperClass _aHelper;

    private List<AchievementObject> achievementList;
    private string type = "EasyWindow";

    public RecyclingListView recListView;

    // Start is called before the first frame update
    void Start()
    {
        _aHelper = AchievementHelperClass._Instance;

        SetRightType();
        SetUpRecViews();
    }

    private void SetRightType()
    {
        type = transform.parent.name;
    }

    private void SetUpRecViews()
    {
        recListView.ItemCallback = PopulateItem;

        if (type.Equals("EasyWindow"))
        {
            achievementList = _aHelper.GetEasyList();
            recListView.RowCount = _aHelper.GetNumEasy();
        }
        else if (type.Equals("MediumWindow"))
        {
            achievementList = _aHelper.GetMediumList();
            recListView.RowCount = _aHelper.GetNumMedium();
        }
        else if (type.Equals("HardWindow"))
        {
            achievementList = _aHelper.GetHardList();
            recListView.RowCount = _aHelper.GetNumHard();
        }
    }

    private void PopulateItem(RecyclingListViewItem item, int rowIndex)
    {
        var child = item as AchievementChildItem;
        child.ChildData = achievementList[rowIndex];
    }
}
