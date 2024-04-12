using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusHelper
{
    private List<Vector3Int> tutorial2_OutpostToGoodCore;

    private List<Vector3Int> scenario1_OutpostToGoodCore;
    private List<Vector3Int> scenario1_OutpostToEnemyCore;

    private List<Vector3Int> scenario2_Outpost1ToGoodCore;
    private List<Vector3Int> scenario2_Outpost2ToGoodCore;
    private List<Vector3Int> scenario2_Outpost3ToGoodCore;
    private List<Vector3Int> scenario2_Outpost1ToEnemyCore;
    private List<Vector3Int> scenario2_Outpost2ToEnemyCore;
    private List<Vector3Int> scenario2_Outpost3ToEnemyCore;
    private List<Vector3Int> scenario2_Junction1ToEnemyCore;
    private List<Vector3Int> scenario2_Junction2ToEnemyCore;

    public BusHelper()
    {
        SetUpTutorial2_OutpostToGoodCore();

        SetUpScenario1_OutpostToGoodCore();
        SetUpScenario1_OutpostToEnemyCore();

        SetUpScenario2_Outpost1ToGoodCore();
        SetUpScenario2_Outpost2ToGoodCore();
        SetUpScenario2_Outpost3ToGoodCore();
        SetUpScenario2_Outpost1ToEnemyCore();
        SetUpScenario2_Outpost2ToEnemyCore();
        SetUpScenario2_Outpost3ToEnemyCore();
        SetUpScenario2_Junction1ToEnemyCore();
        SetUpScenario2_Junction2ToEnemyCore();
    }

    private void SetUpTutorial2_OutpostToGoodCore()
    {
        tutorial2_OutpostToGoodCore = new List<Vector3Int>();
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(10, 8, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(10, 7, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(10, 6, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(10, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(9, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(8, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(7, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(6, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(5, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(4, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(3, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(2, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(1, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, 5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, 4, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, 3, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, 2, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, 1, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, 0, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -1, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -2, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -3, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -4, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -5, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -6, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -7, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -8, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -9, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -10, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -11, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -12, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -13, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(0, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-1, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-2, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-3, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-4, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-5, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-6, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-7, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-8, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-9, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-10, -14, 0));
        tutorial2_OutpostToGoodCore.Add(new Vector3Int(-11, -14, 0));
    }

    private void SetUpScenario1_OutpostToGoodCore()
    {
        scenario1_OutpostToGoodCore = new List<Vector3Int>();
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 7, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 6, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 5, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 4, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 3, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 2, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 1, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, 0, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -1, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -2, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -3, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -4, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -5, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -6, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -7, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -8, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -9, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -10, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -11, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -12, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -13, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -14, 0));
        scenario1_OutpostToGoodCore.Add(new Vector3Int(0, -15, 0));
    }
    private void SetUpScenario1_OutpostToEnemyCore()
    {
        scenario1_OutpostToEnemyCore = new List<Vector3Int>();
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(0, 11, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(0, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(1, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(2, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(3, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(4, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(5, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(6, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(7, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(8, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(9, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(10, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(11, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(12, 12, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(12, 13, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(12, 14, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(12, 15, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(12, 16, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(12, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(11, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(10, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(9, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(8, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(7, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(6, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(5, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(4, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(3, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(2, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(1, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(0, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-1, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-2, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-3, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-4, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-5, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-6, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-7, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-8, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-9, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-10, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-11, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-12, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-13, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-14, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-15, 17, 0));
        scenario1_OutpostToEnemyCore.Add(new Vector3Int(-16, 17, 0));

    }

    private void SetUpScenario2_Outpost1ToGoodCore()
    {
        scenario2_Outpost1ToGoodCore = new List<Vector3Int>();
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -26, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -25, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -24, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -23, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -22, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -21, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -20, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -19, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -18, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -17, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(27, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(26, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(25, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(24, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(23, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(22, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(21, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(20, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(19, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(18, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(17, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(16, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(15, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(14, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(13, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(12, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(11, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(10, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(9, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(8, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(7, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(6, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(5, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(4, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(3, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(2, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(1, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(0, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-1, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-2, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-3, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-4, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-5, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-6, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-7, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-8, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-9, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-10, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-11, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-12, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-13, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-14, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-15, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-16, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-17, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-18, -16, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-18, -17, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-18, -18, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-18, -19, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-18, -20, 0));
        scenario2_Outpost1ToGoodCore.Add(new Vector3Int(-18, -21, 0));
    }
    private void SetUpScenario2_Outpost2ToGoodCore()
    {
        scenario2_Outpost2ToGoodCore = new List<Vector3Int>();
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(27, -26, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(0, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(1, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(2, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(3, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(4, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(4, -2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(4, -1, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(4, 0, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(4, 1, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(4, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(3, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(2, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(1, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(0, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-1, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-2, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-3, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-4, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-5, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-6, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-7, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-8, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-9, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-10, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-11, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-12, 2, 0));

        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, 1, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, 0, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -1, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -4, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -5, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -6, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -7, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -8, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -9, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -10, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -11, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -12, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -13, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -14, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -15, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-14, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-15, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-16, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-17, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -17, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -18, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -19, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -20, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -21, 0));
    }
    private void SetUpScenario2_Outpost3ToGoodCore()
    {
        scenario2_Outpost3ToGoodCore = new List<Vector3Int>();
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-23, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-22, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-21, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-20, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-19, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-18, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-17, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-16, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-15, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-14, 24, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 24, 0));

        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 23, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 22, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 21, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 20, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 19, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 18, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 17, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 16, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 15, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 14, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 13, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 12, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 11, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 10, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 9, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 8, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 7, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 6, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 5, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 4, 0));
        scenario2_Outpost3ToGoodCore.Add(new Vector3Int(-13, 3, 0));

        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, 2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, 1, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, 0, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -1, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -2, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -3, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -4, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -5, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -6, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -7, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -8, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -9, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -10, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -11, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -12, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -13, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -14, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -15, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-13, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-14, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-15, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-16, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-17, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -16, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -17, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -18, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -19, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -20, 0));
        scenario2_Outpost2ToGoodCore.Add(new Vector3Int(-18, -21, 0));
    }
    private void SetUpScenario2_Outpost1ToEnemyCore()
    {
        scenario2_Outpost1ToEnemyCore = new List<Vector3Int>();
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -26, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -25, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -24, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -23, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -22, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -21, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -20, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -19, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -18, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -17, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(27, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(26, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(25, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(24, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(23, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(22, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(21, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(20, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(19, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(18, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(17, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(16, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(15, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(14, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(13, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(12, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(11, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -16, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -15, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -14, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -13, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -12, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -11, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -10, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -9, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -8, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -7, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -6, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -5, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -4, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -3, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -2, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, -1, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, 0, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, 1, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(10, 2, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(9, 2, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(8, 2, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(7, 2, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(6, 2, 0));
        scenario2_Outpost1ToEnemyCore.Add(new Vector3Int(5, 2, 0));
        // gets to junction
    }
    private void SetUpScenario2_Outpost2ToEnemyCore()
    {
        scenario2_Outpost2ToEnemyCore = new List<Vector3Int>();
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(0, -3, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(1, -3, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(2, -3, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(3, -3, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(4, -3, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(4, -2, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(4, -1, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(4, 0, 0));
        scenario2_Outpost2ToEnemyCore.Add(new Vector3Int(4, 1, 0));
    }
    private void SetUpScenario2_Outpost3ToEnemyCore()
    {
        scenario2_Outpost3ToEnemyCore = new List<Vector3Int>();
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-23, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-22, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-21, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-20, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-19, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-18, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-17, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-16, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-15, 24, 0));
        scenario2_Outpost3ToEnemyCore.Add(new Vector3Int(-14, 24, 0));
    }
    private void SetUpScenario2_Junction1ToEnemyCore()
    {
        scenario2_Junction1ToEnemyCore = new List<Vector3Int>();
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(4, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(3, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(2, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(1, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(0, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-1, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-2, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-3, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-4, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-5, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-6, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-7, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-8, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-9, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-10, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-11, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-12, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 2, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 3, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 4, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 5, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 6, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 7, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 8, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 9, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 10, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 11, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 12, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 13, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 14, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 15, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 16, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 17, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 18, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 19, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 20, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 21, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 22, 0));
        scenario2_Junction1ToEnemyCore.Add(new Vector3Int(-13, 23, 0));
    }
    private void SetUpScenario2_Junction2ToEnemyCore()
    {
        scenario2_Junction2ToEnemyCore = new List<Vector3Int>();
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-13, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-12, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-11, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-10, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-9, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-8, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-7, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-6, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-5, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-4, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-3, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-2, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(-1, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(0, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(1, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(2, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(3, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(4, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(5, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(6, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(7, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(8, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(9, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(10, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(11, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(12, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(13, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(14, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(15, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(16, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(17, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(18, 24, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(18, 23, 0));
        scenario2_Junction2ToEnemyCore.Add(new Vector3Int(19, 23, 0));
    }

    public List<Vector3Int> GetTutorial2_OutpostToGoodCore()
    {
        return tutorial2_OutpostToGoodCore;
    }

    public List<Vector3Int> GetScenario1_OutpostToGoodCore()
    {
        return scenario1_OutpostToGoodCore;
    }
    public List<Vector3Int> GetScenario1_OutpostToEnemyCore()
    {
        return scenario1_OutpostToEnemyCore;
    }

    public List<Vector3Int> GetScenario2_Outpost1ToGoodCore()
    {
        return scenario2_Outpost1ToGoodCore;
    }
    public List<Vector3Int> GetScenario2_Outpost2ToGoodCore()
    {
        return scenario2_Outpost2ToGoodCore;
    }
    public List<Vector3Int> GetScenario2_Outpost3ToGoodCore()
    {
        return scenario2_Outpost3ToGoodCore;
    }
    public List<Vector3Int> GetScenario2_Outpost1ToEnemyCore()
    {
        return scenario2_Outpost1ToEnemyCore;
    }
    public List<Vector3Int> GetScenario2_Outpost2ToEnemyCore()
    {
        return scenario2_Outpost2ToEnemyCore;
    }
    public List<Vector3Int> GetScenario2_Outpost3ToEnemyCore()
    {
        return scenario2_Outpost3ToEnemyCore;
    }
    public List<Vector3Int> GetScenario2_Junction1ToEnemyCore()
    {
        return scenario2_Junction1ToEnemyCore;
    }
    public List<Vector3Int> GetScenario2_Junction2ToEnemyCore()
    {
        return scenario2_Junction2ToEnemyCore;
    }
}
