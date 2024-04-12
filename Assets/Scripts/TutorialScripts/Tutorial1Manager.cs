using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tutorial1Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text popUp;
    public int popUpIndex;
    public CameraController camera;
    private Resource_Manager resources;
    public GameObject _workerButton;
    public GameObject _workerSlider;
    public GameObject _workerImages;
    public GameObject _startingUnit;
    public GameObject _turretButton;
    public GameObject _mineButton;
    public GameObject _factoryButton;
    public GameObject _staticCollectorButton;
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

    void Start()
    {
        popUpIndex = 1;
        //camera = FindObjectOfType<CameraController>();
        resources = Resource_Manager._Instance;
        _startingUnit.SetActive(false);
        _buildManager = Build_Manager._Instance;
        _workerButton.SetActive(false);
        _workerImages.SetActive(false);
        _workerSlider.SetActive(false);
        _turretButton.SetActive(false);
        _mineButton.SetActive(false);
        _factoryButton.SetActive(false);
        _staticCollectorButton.SetActive(false);

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

        /* for (i = 0; i < 6; i++)
         {
             if (i == popUpIndex) {
                 popUp.enabled = true;
             }
             else
             {
                 popUp.enabled = false;
             }
         }*/
        if (popUpIndex == 1)
        {
            /* Conditions:
             * Move camera vertically
             * Move camera horizontally
             * Zoom in Camera
             * Zoom out Camera
             */

            //bool condition0; //move camera vertically
            //bool condition1; //move camera horizontally
            //bool condition2; //zoom in camera
            //bool condition3; //zoom out camera
            c0Text.enabled = true;
            c1Text.enabled = true;
            c2Text.enabled = true;
            c3Text.enabled = true;

            c0Text.text = "Camera up/down";
            c1Text.text = "Camera left/right";
            c2Text.text = "Zoom in Camera";
            c3Text.text = "Zoom out Camera";

            //popUp.text = "Move your mouse to the top, bottom, left or right of the screen to move your camera in that direction. " +
            // "Scroll the mouse wheel up and down to zoom in and out, respectively.";
            popUp.text = TutorialTextList.TUTORIAL_CAMERA_MOVEMENT;
            
            if (camera.GetCameraMovingY())
            {
                condition0 = true;
            }
            if (camera.GetCameraMovingX())
            {
                condition1 = true;
            }
            if (camera.GetZoomedIn())
            {
                condition2 = true;
            }
            if (camera.GetZoomedOut())
            {
                condition3 = true;
            }

            if (condition0 && condition1 && condition2 && condition3)
            {
                popUpIndex++;
                //StartCoroutine(NextStep());

                /*condition0 = false;
                condition1 = false;
                condition2 = false;
                condition3 = false;
                c1Text.enabled = false;
                c2Text.enabled = false;
                c3Text.enabled = false;*/
            }            
        }
        if (popUpIndex == 2)
        {
            condition0 = false;
            condition1 = false;
            condition2 = false;
            condition3 = false;
            c1Text.enabled = false;
            c2Text.enabled = false;
            c3Text.enabled = false;
            /* Conditions:
             * Move the unit
             */
            c0Text.text = "Move worker";
            // if (unit.GetMoving()) {
            // popUpIndex+
            // bool condition0 = false;
            popUp.text = TutorialTextList.TUTORIAL_UNIT_CONTROL;
            _startingUnit.SetActive(true);
            if (_startingUnit.transform.GetChild(0).GetComponent<WorkerScript>().GetMoving())
            {
                //popUpIndex++;
                condition0 = true;
                popUpIndex++;
                //StartCoroutine(NextStep());
            }
           /* if (condition0)
            {
                popUpIndex++;
            }*/
        }
        if (popUpIndex == 3)
        {
            /* Conditions
             * Get a resource
             */
            c0Text.text = "Collect 10 of a resource";
            condition0 = false;
            popUp.text = TutorialTextList.TUTORIAL_MINE_RESOURCE;
            if (resources.GetMetalAmount() >= 310 || resources.GetElectricyAmount() >= 310 
                || resources.GetPlatinumAmount() >= 160)
            {
                condition0 = true;
                _workerButton.SetActive(true);
                _workerImages.SetActive(true);
                _workerSlider.SetActive(true);
                popUpIndex++;
            }
        }
        if (popUpIndex == 4)
        {
            //create a new unit
            condition0 = false;
            c0Text.text = "Create a new worker";
            popUp.text = TutorialTextList.TUTORIAL_CREATE_UNIT;
            if (_buildManager.GetUnitList().Count > numStartingUnits)
            {
                // popUpIndex++;
                condition0 = true;
                popUpIndex++;
            }

            /* Conditions
             * Create a new unit
             */
        }
        if (popUpIndex == 5)
        {

            condition0 = false;
            c1Text.enabled = true;
            c0Text.text = "Build a Mine";
            c1Text.text = "Build a Static Collector";
            popUp.text = TutorialTextList.TUTORIAL_BUILDINGS;
            _mineButton.SetActive(true);
            _staticCollectorButton.SetActive(true);
            List<GameObject> temp = _buildManager.GetBuildingList();
            bool hasMine = false;
            bool hasStaticCollector = false;
            foreach (GameObject g in temp)
            {
                if (g.tag.Equals(TagList.STATIC_COLLECTOR))
                {
                    hasStaticCollector = true;
                }
                else if (g.tag.Equals(TagList.MINE))
                {
                    hasMine = true;
                }
            }
            if (hasMine)
            {
                //popUpIndex++;
                condition0 = true;
                //StartCoroutine(NextStep());
            }
            if (hasStaticCollector)
            {
                //popUpIndex++;
                condition1 = true;
                //StartCoroutine(NextStep());
            }
            if (condition0 && condition1)
            {
                popUpIndex++;
            }
        }
        if (popUpIndex == 6)
        {
            popUp.text = TutorialTextList.TUTORIAL_GET_ALL_RESOURCES;
            condition0 = false;
            condition1 = false;
            c2Text.enabled = true;
            c0Text.text = "Have 300 metal";
            c1Text.text = "Have 200 static";
            c2Text.text = "Have 200 platinum";
            /* Conditions
             * Get Metal
             * Get Platinum
             * Get Static
             */
            /*if (resources.GetMetalAmount() >= metalAmount && resources.GetElectricyAmount() >= staticAmount
                && resources.GetPlatinumAmount() >= platinumAmount)
            {
                //popUpIndex++;
                condition0 = true;
                StartCoroutine(NextStep());
            }*/
            if (resources.GetMetalAmount() >= metalAmount)
            {
                condition0 = true;
            }
            if (resources.GetElectricyAmount() >= staticAmount)
            {
                condition1 = true;
            }
            if (resources.GetPlatinumAmount() >= platinumAmount)
            {
                condition2 = true;
            }
            if (condition0 && condition1 && condition2)
            {
                popUpIndex++;
            }
        }
        if (popUpIndex == 7)
        {
            condition0 = false;
            condition1 = false;
            condition2 = false;
            c0Text.enabled = false;
            c1Text.enabled = false;
            c2Text.enabled = false;

            popUp.text = TutorialTextList.TUTORIAL_COMPLETE;
            Invoke("MoveToMenu", 5f);
        }
        
    }

    private void MoveToMenu()
    {
        Achievement_Manager._Instance.SetTutorial1AchievementComplete();
        SceneManager.LoadScene(SceneNameList.MAINMENU_SCENE);
    }

    /*IEnumerator NextStep()
    {
        yield return new WaitForSeconds(1);
        popUpIndex++;
    }*/
}
