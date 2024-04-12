using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AchievementChildItem : RecyclingListViewItem
{
    public Image image;
    public Text titleText;
    public Text descriptionText;

    public Sprite emptySprite;
    public Sprite checkmarkSprite;
    private Color checkmarkColor = new Color(0, 0, 1, 1);
    private Color emptyColor = new Color(1, 1, 1, 1);

    private AchievementObject childData;
    public AchievementObject ChildData
    {
        get { return childData; }
        set
        {
            childData = value;
            image.sprite = CompletedToSprite(childData.GetCompleted());
            titleText.text = KeyToTitle(childData.GetKey());
            descriptionText.text = childData.GetDescription();
        }
    }

    private Sprite CompletedToSprite(int c)
    {
        if (c == 1)
        {
            image.color = checkmarkColor;
            return checkmarkSprite;
        }
        image.color = emptyColor;
        return emptySprite;
    }

    private string KeyToTitle(string key)
    {
        string retString = "";
        int numUpper = 0;

        foreach (char c in key)
        {
            if (char.IsUpper(c))
            {
                numUpper++;
                if (numUpper > 1)
                {
                    retString += " ";
                }
            }

            retString += c;
        }

        return retString;
    }

}
