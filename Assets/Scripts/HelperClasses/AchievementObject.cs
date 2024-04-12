using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementObject
{
    private string key;
    private string description;
    private int completed;

    public AchievementObject(string k, string d, int c)
    {
        key = k;
        description = d;
        completed = c;
    }

    public string GetKey()
    {
        return key;
    }
    public string GetDescription()
    {
        return description;
    }
    public int GetCompleted()
    {
        return completed;
    }

    public void SetCompleted(int c)
    {
        completed = c;
    }
}
