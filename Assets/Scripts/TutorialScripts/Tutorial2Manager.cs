using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial2Manager : MonoBehaviour
{
    public Text popUp;
    public int popUpIndex;
    private Resource_Manager resources;
    public GameObject _workerButton;
    public GameObject _turretButton;
    public GameObject _mineButton;
    public GameObject _factoryButton;
    public GameObject _staticCollectorButton;
    public GameObject _meleeButton;
    public GameObject _rangedButton;
    public GameObject _factorySlider;
    public GameObject _factoryImages;
    private Build_Manager _buildManager;
    private int numStartingUnits = 1;
    private int metalAmount = 300;
    private int staticAmount = 200;
    private int platinumAmount = 200;

    bool condition0 = false;
    bool condition1 = false;
    bool condition2 = false;
    bool condition3 = false;

    public Text c0Text;
    public Text c1Text;
    public Text c2Text;
    public Text c3Text;

    public GameObject check0;
    public GameObject check1;
    public GameObject check2;
    public GameObject check3;

    public GameObject image0;
    public GameObject image1;
    public GameObject image2;
    public GameObject image3;

    // Start is called before the first frame update
    void Start()
    {
        popUpIndex = 1;
        //camera = FindObjectOfType<CameraController>();
        resources = Resource_Manager._Instance;
        _buildManager = Build_Manager._Instance;
        _workerButton.SetActive(false);
        _turretButton.SetActive(false);
        _mineButton.SetActive(false);
        _factoryButton.SetActive(false);
        _staticCollectorButton.SetActive(false);
        _rangedButton.SetActive(false);
        _meleeButton.SetActive(false);
        _factorySlider.SetActive(false);
        _factoryImages.SetActive(false);

        c0Text.enabled = false;
        c1Text.enabled = false;
        c2Text.enabled = false;
        c3Text.enabled = false;

        check0.SetActive(false);
        check1.SetActive(false);
        check2.SetActive(false);
        check3.SetActive(false);

        image0.SetActive(false);
        image1.SetActive(false);
        image2.SetActive(false);
        image3.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        if (condition0)
        {
            check0.SetActive(true);
        }
        else
        {
            check0.SetActive(false);
        }
        if (condition1)
        {
            check1.SetActive(true);
        }
        else
        {
            check1.SetActive(false);
        }
        if (condition2)
        {
            check2.SetActive(true);
        }
        else
        {
            check2.SetActive(false);
        }
        if (condition3)
        {
            check3.SetActive(true);
        }
        else
        {
            check3.SetActive(false);
        }

        if (c0Text.enabled)
        {
            image0.SetActive(true);
        }
        else
        {
            image0.SetActive(false);
        }
        if (c1Text.enabled)
        {
            image1.SetActive(true);
        }
        else
        {
            image1.SetActive(false);
        }
        if (c2Text.enabled)
        {
            image2.SetActive(true);
        }
        else
        {
            image2.SetActive(false);
        }
        if (c3Text.enabled)
        {
            image3.SetActive(true);
        }
        else
        {
            image3.SetActive(false);
        }

        if (popUpIndex == 1)
        {
            condition0 = false;
            condition1 = false;
            c0Text.enabled = true;
            c1Text.enabled = true;
            _factoryButton.SetActive(true);
            _turretButton.SetActive(true);

            c0Text.text = "Build a Factory";
            c1Text.text = "Build a Turret";

            popUp.text = TutorialTextList.TUTORIAL_FACTORY_TURRET;

            List<GameObject> temp = _buildManager.GetBuildingList();
            bool hasFactory = false;
            bool hasTurret = false;
            foreach (GameObject g in temp)
            {
                if (g.tag.Equals(TagList.FACTORY))
                {
                    hasFactory = true;
                }
                else if (g.tag.Equals(TagList.TURRET))
                {
                    hasTurret = true;
                }
            }
            if (hasFactory)
            {
                condition0 = true;
            }
            if (hasTurret)
            {
                condition1 = true;
            }

            if (condition0 && condition1)
            {
                popUpIndex = 2;
            }

        }

        if (popUpIndex == 2)
        {
            _meleeButton.SetActive(true);
            _rangedButton.SetActive(true);
            _factorySlider.SetActive(true);
            _factoryImages.SetActive(true);
            condition0 = false;
            condition1 = false;

            c0Text.text = "Make a melee unit";
            c1Text.text = "Make a ranged unit";

            popUp.text = TutorialTextList.TUTORIAL_CREATE_COMBAT_UNITS;
            List<GameObject> temp = _buildManager.GetUnitList();
            bool hasMelee = false;
            bool hasRanged = false;
            foreach (GameObject g in temp)
            {
                if (g.tag.Equals(TagList.MELEE_UNIT))
                {
                    hasMelee = true;
                }
                else if (g.tag.Equals(TagList.RANGED_UNIT))
                {
                    hasRanged = true;
                }
            }
            if (hasMelee)
            {
                condition0 = true;
            }
            if (hasRanged)
            {
                condition1 = true;
            }
            if (condition0 && condition1)
            {
                popUpIndex = 3;
            }
        }
        if (popUpIndex == 3)
        {
            condition0 = false;
            condition1 = false;
            c1Text.enabled = false;
            c0Text.text = "Destroy Enemy Outpost";
            popUp.text = TutorialTextList.TUTORIAL_ATTACK_OUTPOST;
            bool existOutpost = false;
            List<GameObject> temp = _buildManager.GetBuildingList();
            foreach (GameObject g in temp)
            {
                if (g.tag.Equals(TagList.ENEMY_BUILDING))
                {
                    if (g.transform.GetChild(0).tag.Equals(TagList.OUTPOST))
                    {
                        existOutpost = true;
                    }
                }                

                
            }
            if (!existOutpost)
            {
                condition0 = true;
            }
            if (condition0)
            {
                popUpIndex = 4;
            }
        }
        if (popUpIndex == 4)
        {
            condition0 = false;
            c0Text.text = "Build Outpost";
            popUp.text = TutorialTextList.TUTORIAL_BUILD_OUTPOST;

            bool existOutpost = false;
            List<GameObject> temp = _buildManager.GetBuildingList();
            foreach (GameObject g in temp)
            {
                if (g.gameObject.tag.Equals(TagList.OUTPOST))
                {
                    existOutpost = true;
                }
            }
            if (existOutpost)
            {
                condition0 = true;
            }
            if (condition0)
            {
                popUpIndex = 5;
            }
        }
        if (popUpIndex == 5)
        {
            condition0 = false;
            c0Text.enabled = false;
            popUp.text = "Building the Bus...";
            StartCoroutine(BuildBus());
        }
        if (popUpIndex == 6) {
            condition0 = false;
            c0Text.enabled = false;

            popUp.text = TutorialTextList.TUTORIAL_COMPLETE;
            Invoke("MoveToMenu", 5f);
        }
        
    }

    private void MoveToMenu()
    {
        Achievement_Manager._Instance.SetTutorial2AchievementComplete();
        SceneManager.LoadScene(SceneNameList.MAINMENU_SCENE);
    }

    IEnumerator BuildBus()
    {
        yield return new WaitForSeconds(10);
        popUpIndex = 6;
    }
}
