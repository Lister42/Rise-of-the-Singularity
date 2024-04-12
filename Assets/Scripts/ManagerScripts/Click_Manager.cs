using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Click_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Click_Manager _Instance { get; private set; } = null;

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

    private Build_Manager _buildManager;
    private InteractiveAudio_Manager _interactiveAudioManager;
    public Camera _camera;

    private float screenHeight = 0.0F;
    private float screenWidth = 0.0F;

    private List<RaycastHit2D> clickList;
    private Vector2 leftClickPosDown;
    private Vector2 leftClickPosUp;
    private Vector2 rightClickPosDown;
    private Vector2 rightClickPosUp;
    public RectTransform selectionBox;
    private Vector2 startPos;

    #region Last Objects
    private List<GameObject> lastClickedUnits;
    private GameObject lastObjectClicked;
    private Color normalColor = new Color(1, 1, 1, 1);
    private Color metalColor = new Color(113F/255F, 104F/255F, 128F/255F, 1);
    private Color enemyMeleeColor = new Color(1, 180F/255F, 180F/255F, 1);
    private CoreScript lastCore;
    private FactoryScript lastFactory;
    private WorkerScript lastWorker;
    private MeleeScript lastMelee;
    private RangedScript lastRanged;
    private BuildSiteScript lastBuildSite;
    #endregion

    #region Panel Objects
    private List<GameObject> panels;
    public GameObject _buildPanel;
    public GameObject _corePanel;
    public GameObject _outpostPanel;
    public GameObject _buildSitePanel;
    public GameObject _minePanel;
    public GameObject _staticCollectorPanel;
    public GameObject _factoryPanel;
    public GameObject _turretPanel;
    public GameObject _secondaryPanel;
    #endregion

    #region Click State Variables
    private bool normalState = true;
    private bool templateActive = false;
    private bool clickOnBuilding = false;
    private bool clickOnUnit = false;
    private bool clickOnWorker = false;
    private bool clickOnMelee = false;
    private bool clickOnRanged = false;
    private bool clickOnResource = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _buildManager = Build_Manager._Instance;
        _interactiveAudioManager = InteractiveAudio_Manager._Instance;

        clickList = new List<RaycastHit2D>();
        lastClickedUnits = new List<GameObject>();

        panels = new List<GameObject>();
        panels.Add(_buildPanel);
        panels.Add(_corePanel);
        panels.Add(_buildSitePanel);
        panels.Add(_minePanel);
        panels.Add(_staticCollectorPanel);
        panels.Add(_factoryPanel);
        panels.Add(_turretPanel);
        panels.Add(_secondaryPanel);

        DeactivateAllPanels();
    }

    // Update is called once per frame
    void Update()
    {
        lastClickedUnits.RemoveAll(item => item == null);
        screenHeight = _camera.scaledPixelHeight;
        screenWidth = _camera.scaledPixelWidth;
        DetectClicks();
        DetectLeftMouseClick();
        DetectRightMouseClick();
        DetectDelete();
    }

    private void DetectLeftMouseClick()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (ClickedOnPanel())
            {
                return;
            }

            foreach (GameObject unit in lastClickedUnits)
            {
                GameObject child = unit.transform.GetChild(0).gameObject;
                SetUnSelectColor(child);
                if (unit.tag.Equals(TagList.ENEMY_UNIT))
                {
                    child.GetComponent<EnemyUnitScript>().SetForceHealthBar(false);
                }
                else
                {
                    child.GetComponent<UnitScript>().SetForceHealthBar(false);
                }
            }
            lastClickedUnits.Clear();

            Vector2 vec2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D startHit = Physics2D.Raycast(vec2, Vector2.zero, 0);

            if (clickList.Count <= 0)
            {
                clickList.Add(startHit);
            }
            if (lastObjectClicked)
            {
                GameObject child = lastObjectClicked.transform.GetChild(0).gameObject;
                SetUnSelectColor(child);
                if (lastObjectClicked.tag.Equals(TagList.ENEMY_UNIT)) {
                    child.GetComponent<EnemyUnitScript>().SetForceHealthBar(false);
                }
                else if (lastObjectClicked.tag.Equals(TagList.ENEMY_BUILDING))
                {
                    child.GetComponent<EnemyBuildingScript>().SetForceHealthBar(false);
                }
                else if (lastObjectClicked.tag.Equals(TagList.CORE)
                    || lastObjectClicked.tag.Equals(TagList.MINE)
                    || lastObjectClicked.tag.Equals(TagList.STATIC_COLLECTOR)
                    || lastObjectClicked.tag.Equals(TagList.FACTORY)
                    || lastObjectClicked.tag.Equals(TagList.TURRET)
                    || lastObjectClicked.tag.Equals(TagList.OUTPOST)
                    || lastObjectClicked.tag.Equals(TagList.BUILD_SITE))
                {
                    child.GetComponent<GoodBuildingScript>().SetForceHealthBar(false);
                }
                else
                {
                    // causes an error and is not needed
                    //child.GetComponent<UnitScript>().SetForceHealthBar(false);
                }

            }

            foreach (RaycastHit2D hit in clickList)
            {
                if (hit)
                {
                    string tag = hit.transform.tag;
                    GameObject child;

                    if (tag.Equals(TagList.BUILDING_UI))
                    {
                        print("Clicked the building UI");
                        tag = hit.transform.parent.tag;
                        lastObjectClicked = hit.transform.parent.gameObject;
                        child = hit.transform.parent.GetChild(0).gameObject;
                    }
                    else
                    {
                        print("Clicked the normal collider");
                        lastObjectClicked = hit.transform.gameObject;
                        child = hit.transform.GetChild(0).gameObject;
                    }

                    if (tag.Equals(TagList.GROUND))
                    {
                        print("Clicked " + tag + "; LCOG");
                        LeftClickOnGround();
                    }
                    else if (child.GetComponent<SpriteRenderer>().sortingLayerName != SortingLayerList.UI)
                    {

                        print("Clicked " + tag);
                        if (tag.Equals(TagList.CORE))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.CORE, TagList.CORE);
                            LeftClickReset();
                            _corePanel.SetActive(true);
                            ActivateBuildingUI(child);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            lastCore = child.gameObject.GetComponent<CoreScript>();
                            lastCore.SetClicked(true);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.OUTPOST))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.OUTPOST, TagList.OUTPOST);
                            LeftClickReset();
                            //_outpostPanel.SetActive(true);
                            ActivateBuildingUI(child);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            //lastCore = child.gameObject.GetComponent<CoreScript>();
                            //lastCore.SetClicked(true);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.MINE))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.MINE, TagList.MINE);
                            LeftClickReset();
                            //_minePanel.SetActive(true);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            ActivateBuildingUI(child);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.STATIC_COLLECTOR))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.STATIC_COLLECTOR, TagList.STATIC_COLLECTOR);
                            LeftClickReset();
                            //_staticCollectorPanel.SetActive(true);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            ActivateBuildingUI(child);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.FACTORY))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.FACTORY, TagList.FACTORY);
                            LeftClickReset();
                            _factoryPanel.SetActive(true);
                            ActivateBuildingUI(child);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            lastFactory = child.gameObject.GetComponent<FactoryScript>();
                            lastFactory.SetClicked(true);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.TURRET))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.TURRET, TagList.TURRET);
                            LeftClickReset();
                            //_turretPanel.SetActive(true);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            ActivateBuildingUI(child);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.BUILD_SITE))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.BUILD_SITE, TagList.BUILD_SITE);
                            LeftClickReset();
                            _buildSitePanel.SetActive(true);
                            ActivateBuildingUI(child);
                            child.GetComponent<GoodBuildingScript>().SetForceHealthBar(true);
                            lastBuildSite = child.gameObject.GetComponent<BuildSiteScript>();
                            lastBuildSite.SetClicked(true);
                            SetSelectColor(child);

                            clickOnBuilding = true;
                        }
                        else if (tag.Equals(TagList.WORKER))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.WORKER, TagList.WORKER);
                            LeftClickReset();
                            _buildPanel.SetActive(true);
                            child.GetComponent<UnitScript>().SetForceHealthBar(true);
                            lastWorker = child.GetComponent<WorkerScript>();
                            lastClickedUnits.Add(lastObjectClicked);
                            ActivateUnitUI(child);
                            SetSelectColor(child);

                            clickOnUnit = true;
                            clickOnWorker = true;
                        }
                        else if (tag.Equals(TagList.MELEE_UNIT))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.MELEE_UNIT, TagList.MELEE_UNIT);
                            LeftClickReset();
                            child.GetComponent<UnitScript>().SetForceHealthBar(true);
                            lastMelee = child.GetComponent<MeleeScript>();
                            lastClickedUnits.Add(lastObjectClicked);
                            ActivateUnitUI(child);
                            SetSelectColor(child);

                            clickOnUnit = true;
                            clickOnMelee = true;
                        }
                        else if (tag.Equals(TagList.RANGED_UNIT))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.RANGED_UNIT, TagList.RANGED_UNIT);
                            LeftClickReset();
                            child.GetComponent<UnitScript>().SetForceHealthBar(true);
                            lastRanged = child.GetComponent<RangedScript>();
                            lastClickedUnits.Add(lastObjectClicked);
                            ActivateUnitUI(child);
                            SetSelectColor(child);

                            clickOnUnit = true;
                            clickOnRanged = true;
                        }
                        else if (tag.Equals(TagList.METAL))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.METAL, TagList.METAL);
                            LeftClickReset();
                            ActivateResourceUI(child);
                            SetSelectColor(child);

                            clickOnResource = true;
                        }
                        else if (tag.Equals(TagList.STATIC))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.STATIC, TagList.STATIC);
                            LeftClickReset();
                            ActivateResourceUI(child);
                            SetSelectColor(child);

                            clickOnResource = true;
                        }
                        else if (tag.Equals(TagList.PLATINUM))
                        {
                            _interactiveAudioManager.PlayInteractiveSound(TagList.PLATINUM, TagList.PLATINUM);
                            LeftClickReset();
                            ActivateResourceUI(child);
                            SetSelectColor(child);

                            clickOnResource = true;
                        }
                        else if (tag.Equals(TagList.ENEMY_UNIT))
                        {
                            string childTag = child.tag;
                            _interactiveAudioManager.PlayInteractiveSound(TagList.ENEMY_UNIT, childTag);
                            LeftClickReset();
                            child.GetComponent<EnemyUnitScript>().SetForceHealthBar(true);
                            ActivateUnitUI(child);
                            SetSelectColor(child);
                        }
                        else if (tag.Equals(TagList.ENEMY_BUILDING))
                        {
                            string childTag = child.tag;
                            _interactiveAudioManager.PlayInteractiveSound(TagList.ENEMY_BUILDING, childTag);
                            LeftClickReset();
                            child.GetComponent<EnemyBuildingScript>().SetForceHealthBar(true);
                            ActivateBuildingUI(child);
                            SetSelectColor(child);
                        }
                        else
                        {
                            print(tag + " tag not implemented yet in Click Manager; LCR");
                            LeftClickReset();
                        }
                    }
                    else
                    {
                        print("Clicked something in the UI sorting layer; LCR");
                        LeftClickReset();
                    }

                }
                else
                {
                    print("Object clicked has no 2D Collider; LCOG");
                    LeftClickOnGround();
                }
            }
        }
    }

    private void DetectRightMouseClick()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (templateActive)
            {
                if (_buildManager.PlaceTemplate())
                {
                    foreach (GameObject unit in lastClickedUnits)
                    {
                        SetUnSelectColor(unit.transform.GetChild(0).gameObject);
                    }
                    lastClickedUnits.Clear();
                    LeftClickReset();
                    normalState = true;
                }
            } 
            else if (clickOnUnit)
            {
                // rayCast
                Vector2 vec2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                RaycastHit2D hit = Physics2D.Raycast(vec2, Vector2.zero, 0);

                if (hit)
                {
                    string tag = hit.transform.tag;
                    GameObject clickedObject = hit.transform.gameObject;

                    if (tag.Equals(TagList.BUILDING_UI))
                    {
                        print("Clicked the building UI");
                        tag = hit.transform.parent.tag;
                        clickedObject = hit.transform.parent.gameObject;
                    }

                    foreach (GameObject unit in lastClickedUnits)
                    {
                        GameObject child = unit.transform.GetChild(0).gameObject;
                        bool worker = IsWorker(unit);
                        bool melee = IsMelee(unit);
                        bool ranged = IsRanged(unit);

                        if (worker)
                        {
                            WorkerScript workerScipt = child.GetComponent<WorkerScript>();
                            // worker actions
                            print("Clicked on worker");

                            //string tag = hit.transform.tag;
                            if (tag.Equals(TagList.GROUND))
                            {
                                print("Worker move to location");
                                workerScipt.MoveToLocation(vec2);
                            }
                            else if (tag.Equals(TagList.BUILD_SITE))
                            {
                                print("Worker move to and build building");
                                workerScipt.MoveAndBuildBuilding(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.METAL))
                            {
                                print("Worker move to and collect metal");
                                workerScipt.MoveAndCollectResource(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.STATIC))
                            {
                                print("Worker move to and collect static");
                                workerScipt.MoveAndCollectResource(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.PLATINUM))
                            {
                                print("Worker move to and collect platinum");
                                workerScipt.MoveAndCollectResource(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.CORE))
                            {
                                print("Worker move to and deposit resources at core");
                                workerScipt.MoveAndDeposit(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.MINE))
                            {
                                print("Worker move to and deposit resources at mine");
                                workerScipt.MoveAndDeposit(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.STATIC_COLLECTOR))
                            {
                                print("Worker move to and deposit resources at static collector");
                                workerScipt.MoveAndDeposit(clickedObject, vec2);
                            }
                            //else if (tag == TagList.ENEMY_UNIT)
                            //{
                            //    print("Worker move to and attack enemy");
                            //    workerScipt.MoveAndAttackEnemyUnit(clickedObject, vec2);
                            //}
                            //else if (tag == TagList.ENEMY_BUILDING)
                            //{
                            //    print("Worker move to and attack enemy building");
                            //    workerScipt.MoveAndAttackEnemyBuilding(clickedObject, vec2);
                            //}
                            else
                            {
                                print("Invalid action");
                            }

                        }
                        else if (melee)
                        {
                            MeleeScript meleeScript = child.GetComponent<MeleeScript>();
                            print("Clicked on Melee");

                            //string tag = hit.transform.tag;
                            if (tag.Equals(TagList.GROUND))
                            {
                                print("Melee move to location");
                                meleeScript.MoveToLocation(vec2);
                            }
                            else if (tag.Equals(TagList.ENEMY_UNIT))
                            {
                                print("Melee move to and attack enemy unit");
                                meleeScript.MoveAndAttackEnemyUnit(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.ENEMY_BUILDING))
                            {
                                print("Melee move to and attack enemy building");
                                meleeScript.MoveAndAttackEnemyBuilding(clickedObject, vec2);
                            }
                            else
                            {
                                print("Melee Invalid action");
                            }

                        }
                        else if (ranged)
                        {
                            RangedScript rangedScript = child.GetComponent<RangedScript>();
                            print("Clicked on Ranged");

                            //string tag = hit.transform.tag;
                            if (tag.Equals(TagList.GROUND))
                            {
                                print("Ranged move to location");
                                rangedScript.MoveToLocation(vec2);
                            }
                            else if (tag.Equals(TagList.ENEMY_UNIT))
                            {
                                print("Ranged move to and attack enemy unit");
                                rangedScript.MoveAndAttackEnemyUnit(clickedObject, vec2);
                            }
                            else if (tag.Equals(TagList.ENEMY_BUILDING))
                            {
                                print("Ranged move to and attack enemy building");
                                rangedScript.MoveAndAttackEnemyBuilding(clickedObject, vec2);
                            }
                            else
                            {
                                print("Ranged Invalid action");
                            }

                        }
                        else
                        {
                            print("Click on unit not implemented yet");
                            // all other units
                            //unit.MoveToLocation(vec2);
                        }

                        //LeftClickReset();
                        //normalState = true;
                    }
                }
                else
                {
                    print("Object clicked has no 2D Collider");
                }
            }
            else if (clickOnBuilding)
            {
                // way point?
                print("Waypoint");

                //LeftClickReset();
                //normalState = true;
            }
            else
            {
                // no right click action implemented
                print("No right click action implemented");
            }
        }
    }

    public void PlacedSite_MoveWorker(GameObject building)
    {
        foreach (GameObject unit in lastClickedUnits)
        {
            if (IsWorker(unit))
            {
                GameObject child = unit.transform.GetChild(0).gameObject;
                WorkerScript workerScript = child.GetComponent<WorkerScript>();
                SetUnSelectColor(child.gameObject);
                child.GetComponent<UnitScript>().SetForceHealthBar(false);
                workerScript.MoveAndBuildBuilding(building, building.transform.position);
            }
        }
        lastClickedUnits.Clear();
    }

    private void LeftClickOnGround()
    {
        if (ClickedOnPanel())
        {
            return;
        }
        else
        {
            print("Reset all");
            LeftClickReset();
            normalState = true;
        }
    }

    private bool ClickedOnPanel()
    {
        Vector2 mouseSpot = Input.mousePosition;
        float primaryWidthModifier = 5.42F;
        float primaryHeightModifier = 4.5F;
        float secondaryWidthModifier = 2.82F;
        float secondaryHeightModifier = 5.8F;
        float mapWidthModifier = 1.145F;
        float mapHeightModifier = 4.5F;
        float resourceWidthModifier = 4F;
        float resourceHeightModifier = 1.125F;

        if (mouseSpot.x <= screenWidth / primaryWidthModifier && mouseSpot.y <= screenHeight / primaryHeightModifier)
        {
            print("Clicked on Primary Panel");
            return true;
        }
        else if (mouseSpot.x <= screenWidth / secondaryWidthModifier && mouseSpot.y <= screenHeight / secondaryHeightModifier)
        {
            print("Clicked on Secondary Panel");
            return true;
        }
        else if (mouseSpot.x >= screenWidth / mapWidthModifier && mouseSpot.y <= screenHeight / mapHeightModifier)
        {
            print("Clicked on Map Panel");
            return true;
        }
        else if (mouseSpot.x <= screenWidth / resourceWidthModifier && mouseSpot.y >= screenHeight / resourceHeightModifier)
        {
            print("Clicked on Resource Panel");
            return true;
        }

        return false;
    }

    public void DeactivateAllPanels()
    {
        foreach (GameObject g in panels)
        {
            g.SetActive(false);
        }
    }

    private void ResetAllStates()
    {
        normalState = false;
        clickOnBuilding = false;
        clickOnUnit = false;
        clickOnWorker = false;
        clickOnMelee = false;
        clickOnRanged = false;
        clickOnResource = false;
    }

    public void SetTemplateActive(bool value)
    {
        templateActive = value;
    }

    private void LeftClickReset()
    {
        ResetAllUI();
        ResetLastObjects();
        templateActive = false;
        _buildManager.Reset();
        DeactivateAllPanels();
        ResetAllStates();
    }

    private void ResetLastObjects()
    {
        //if (lastObjectClicked)
        //{
        //    lastObjectClicked = null;
        //}
        if (lastCore)
        {
            lastCore.SetClicked(false);
            lastCore = null;
        }
        if (lastFactory)
        {
            lastFactory.SetClicked(false);
            lastCore = null;
        }
        if (lastBuildSite)
        {
            lastBuildSite.SetClicked(false);
            lastBuildSite = null;
        }
        if (lastWorker)
        {
            lastWorker = null;
        }
        if (lastMelee)
        {
            lastMelee = null;
        }
        if (lastRanged)
        {
            lastRanged = null;
        }
    }

    private void ActivateUnitUI(GameObject child)
    {
        ResetAllUI();
        Unit_UIHandler unit_UIHandler = child.GetComponent<Unit_UIHandler>();
        unit_UIHandler.Selected();
        unit_UIHandler.SetIsVisible(true);
    }

    private void ActivateBuildingUI(GameObject child)
    {
        ResetAllUI();
        Building_UIHandler building_UIHandler = child.GetComponent<Building_UIHandler>();
        building_UIHandler.Selected();
        building_UIHandler.SetIsVisible(true);
    }

    private void ActivateResourceUI(GameObject child)
    {
        ResetAllUI();
        Resource_UIHandler resource_UIHandler = child.GetComponent<Resource_UIHandler>();
        resource_UIHandler.Selected();
        resource_UIHandler.SetIsVisible(true);
    }

    public void ResetAllUI()
    {
        List<GameObject> units = _buildManager.GetUnitList();
        List<GameObject> buildings = _buildManager.GetBuildingList();
        List<GameObject> resources = _buildManager.GetResourceList();

        foreach (GameObject u in units)
        {
            u.transform.GetChild(0).gameObject.GetComponent<Unit_UIHandler>().SetIsVisible(false);

        }
        foreach (GameObject b in buildings)
        {
            b.transform.GetChild(0).gameObject.GetComponent<Building_UIHandler>().SetIsVisible(false);
        }
        foreach (GameObject r in resources)
        {
            r.transform.GetChild(0).gameObject.GetComponent<Resource_UIHandler>().SetIsVisible(false);
        }
    }

    public FactoryScript GetLastFactory()
    {
        return lastFactory;
    }

    public void SetSelectColor(GameObject it)
    {
        if (it.GetComponent<SpriteRenderer>())
        {
            it.GetComponent<SpriteRenderer>().color = new Color(0.5F, 1, 1, 1);
        }
    }

    private void SetUnSelectColor(GameObject it)
    {
        if (it.GetComponent<SpriteRenderer>())
        {
            if (it.tag.Equals(TagList.METAL))
            {
                it.GetComponent<SpriteRenderer>().color = metalColor;
            }
            else if (it.transform.parent.tag.Equals(TagList.ENEMY_UNIT) && it.tag.Equals(TagList.MELEE_UNIT))
            {
                it.GetComponent<SpriteRenderer>().color = enemyMeleeColor;
            }
            else
            {
                it.GetComponent<SpriteRenderer>().color = normalColor;
            }
        }
    }

    // click, drag, select logic
    private void DetectClicks()
    {
        if (Input.GetMouseButtonDown(0))
        {
            leftClickPosDown = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            startPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0) && leftClickPosDown != null)
        {
            leftClickPosUp = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            FillClickList();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
        if (Input.GetMouseButton(0))
        {
            UpdateSelectionBox(Input.mousePosition);
        }
    }

    private void FillClickList()
    {
        clickList.Clear();

        float down_x = leftClickPosDown.x;
        float up_x = leftClickPosUp.x;
        float down_y = leftClickPosDown.y;
        float up_y = leftClickPosUp.y;

        float maxX = Mathf.Max(down_x, up_x);
        float minX = Mathf.Min(down_x, up_x);
        float maxY = Mathf.Max(down_y, up_y);
        float minY = Mathf.Min(down_y, up_y);

        float boxWidth = Vector2.Distance(new Vector2(maxX, 0), new Vector2(minX, 0));
        float boxHeight = Vector2.Distance(new Vector2(0, maxY), new Vector2(0, minY));
        Vector2 boxSize = new Vector2(boxWidth, boxHeight);
        Vector2 boxCenter = new Vector2(maxX - boxWidth/2, maxY - boxHeight/2);

        RaycastHit2D[] hitUnits = Physics2D.BoxCastAll(boxCenter, boxSize, 0f, transform.forward);

        foreach(RaycastHit2D hit in hitUnits)
        {
            string hitTag = hit.transform.tag;
            if (!hitTag.Equals(TagList.GROUND))
            {
                if (hitTag.Equals(TagList.MELEE_UNIT)
                    || hitTag.Equals(TagList.RANGED_UNIT)
                    || hitTag.Equals(TagList.WORKER))
                {
                    clickList.Add(hit);
                }

                if (hitTag.Equals(TagList.ENEMY_UNIT))
                {
                    clickList.Add(hit);
                }
            }
        }

        RemoveDuplicatesFromClickList();

        if (clickList.Count > 1)
        {
            RemoveEnemiesFromList();
        }
    }

    private void RemoveDuplicatesFromClickList()
    {
        for (int i = 0; i < clickList.Count - 1; i++)
        {
            for (int j = i + 1; j < clickList.Count; j++)
            {
                if (clickList[i] && clickList[j])
                {
                    if (ReferenceEquals(clickList[i].transform.gameObject, clickList[j].transform.gameObject))
                    {
                        //print("i: " + i + ", j: " + j + " === " + clickList[i].transform.name + ", " + clickList[j].transform.name);
                        clickList.RemoveAt(j);
                        i = -1;
                        break;
                    }
                }
            }
        }

        /*print(" <<< Click List >>> ");
        foreach (RaycastHit2D h in clickList)
        {
            print(" <<< " + h.transform.gameObject.name + " >>> ");
        }*/
    }

    private void RemoveEnemiesFromList()
    {
        for (int i = clickList.Count-1; i >= 0; i--)
        {
            if (clickList[i].transform.tag.Equals(TagList.ENEMY_UNIT))
            {
                clickList.RemoveAt(i);
            }
        }
    }

    private bool IsWorker(GameObject it)
    {
        return it.tag.Equals(TagList.WORKER);
    }
    private bool IsMelee(GameObject it)
    {
        return it.tag.Equals(TagList.MELEE_UNIT);
    }
    private bool IsRanged(GameObject it)
    {
        return it.tag.Equals(TagList.RANGED_UNIT);
    }

    // called when we are creating a selection box
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);
        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;
        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    // called when we release the selection box
    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);
    }


    public List<GameObject> GetLastClickedUnits()
    {
        return lastClickedUnits;
    }

    private void DetectDelete()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            foreach (GameObject unit in lastClickedUnits)
            {
                DeleteUnit(unit);
            }
            lastClickedUnits.Clear();

            if (lastObjectClicked)
            {
                string tag = lastObjectClicked.tag;
                if (tag.Equals(TagList.MINE) || tag.Equals(TagList.STATIC_COLLECTOR)
                    || tag.Equals(TagList.FACTORY) || tag.Equals(TagList.TURRET)
                    || tag.Equals(TagList.BUILD_SITE))
                {
                    DeleteBuilding(lastObjectClicked);
                }
            }
        }
    }

    private void DeleteUnit(GameObject unit)
    {
        Destroy(unit);
    }

    private void DeleteBuilding(GameObject building)
    {
        building.transform.GetChild(0).GetComponent<GoodBuildingScript>().IsDestroyed();
        Destroy(building);
    }
}
