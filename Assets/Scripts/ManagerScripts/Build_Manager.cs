using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Build_Manager _Instance { get; private set; } = null;

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


    private Resource_Manager _resourceManager;
    private SortingLayerOrder_Manager _sortingLayerOrderManager;
    private List<GameObject> buildingList;
    private List<GameObject> unitList;
    private List<GameObject> resourceList;
    private List<GameObject> environmentList;
    public List<Vector2> spotList;
    public GameObject _mouseFollower;
    private GameObject tBuilding;
    private Click_Manager _clickManager;
    public GameObject _buildingParent;
    public GameObject _unitParent;
    public GameObject _resourceParent;
    public GameObject _environmentParent;

    private int lastMetal = 0;
    private int lastElectricity = 0;
    private int lastPlatinum = 0;

    private Color currentColor = new Color(1, 1, 1, .4F);
    private Vector2 lastSpot = new Vector2(0, 0);
    private bool leftShiftPressed = false;

    private Color redColor = new Color(1, 0, 0, .4F);
    private Color whiteColor = new Color(1, 1, 1, .4F);

    private float lastBuildingEditTime = 0.0F;

    #region Building Prefabs
    private GameObject tempPrefab;
    public GameObject _minePrefab;
    public GameObject _buildSitePrefab;
    public GameObject _staticCollectorPrefab;
    public GameObject _factoryPrefab;
    public GameObject _turretPrefab;
    #endregion

    #region State Variables
    private bool templateActive = false;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _clickManager = Click_Manager._Instance;
        _sortingLayerOrderManager = SortingLayerOrder_Manager._Instance;
        _resourceManager = Resource_Manager._Instance;
        buildingList = new List<GameObject>();
        unitList = new List<GameObject>();
        resourceList = new List<GameObject>();
        environmentList = new List<GameObject>();
        spotList = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectShiftButtonDown();
        ObjectFollowMouse();
        SnapTemplatePlacement();
        CheckTemplateMoved();
    }

    private void DetectShiftButtonDown()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            leftShiftPressed = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            leftShiftPressed = false;
        }
    }

    private void ObjectFollowMouse()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _mouseFollower.transform.position = new Vector3(mouseWorldPosition.x, mouseWorldPosition.y, -0.1F);
    }

    public void BuildBuilding(string tag)
    {
        if (!templateActive)
        {
            if (tag.Equals(TagList.MINE))
            {
                lastMetal = ResourceCostList.MINE_METAL_COST;
                lastElectricity = ResourceCostList.MINE_ELECTRICITY_COST;
                lastPlatinum = ResourceCostList.MINE_PLATINUM_COST;
                if (!HaveRequiredResources())
                {
                    return;
                }

                tempPrefab = _minePrefab;
                tBuilding = Instantiate(_minePrefab, _mouseFollower.transform.position, Quaternion.identity);
            }
            else if (tag.Equals(TagList.STATIC_COLLECTOR))
            {
                lastMetal = ResourceCostList.STATIC_COLLECTOR_METAL_COST;
                lastElectricity = ResourceCostList.STATIC_COLLECTOR_ELECTRICITY_COST;
                lastPlatinum = ResourceCostList.STATIC_COLLECTOR_PLATINUM_COST;
                if (!HaveRequiredResources())
                {
                    return;
                }

                tempPrefab = _staticCollectorPrefab;
                tBuilding = Instantiate(_staticCollectorPrefab, _mouseFollower.transform.position, Quaternion.identity);
            }
            else if (tag.Equals(TagList.FACTORY))
            {
                lastMetal = ResourceCostList.FACTORY_METAL_COST;
                lastElectricity = ResourceCostList.FACTORY_ELECTRICITY_COST;
                lastPlatinum = ResourceCostList.FACTORY_PLATINUM_COST;
                if (!HaveRequiredResources())
                {
                    return;
                }

                tempPrefab = _factoryPrefab;
                tBuilding = Instantiate(_factoryPrefab, _mouseFollower.transform.position, Quaternion.identity);
            }
            else if (tag.Equals(TagList.TURRET))
            {
                lastMetal = ResourceCostList.TURRET_METAL_COST;
                lastElectricity = ResourceCostList.TURRET_ELECTRICITY_COST;
                lastPlatinum = ResourceCostList.TURRET_PLATINUM_COST;
                if (!HaveRequiredResources())
                {
                    return;
                }

                tempPrefab = _turretPrefab;
                tBuilding = Instantiate(_turretPrefab, _mouseFollower.transform.position, Quaternion.identity);
            }
            else
            {
                print("Invalid building argument; should never get here. Weird things could happen.");
            }

            tBuilding.transform.parent = _mouseFollower.transform;
            tBuilding.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingLayerName = SortingLayerList.UI;
            //tBuilding.GetComponent<DynamicGridObstacle>().enabled = false;
            tBuilding.GetComponent<BoxCollider2D>().enabled = false;
            templateActive = true;
            _clickManager.SetTemplateActive(true);
            _clickManager.DeactivateAllPanels();
        }
    }

    private bool HaveRequiredResources()
    {
        return (lastMetal <= _resourceManager.GetMetalAmount()
            && lastElectricity <= _resourceManager.GetElectricyAmount()
            && lastPlatinum <= _resourceManager.GetPlatinumAmount());
    }

    public bool HaveRequiredResources(int metal, int electricity, int platinum)
    {
        return (metal <= _resourceManager.GetMetalAmount()
            && electricity <= _resourceManager.GetElectricyAmount()
            && platinum <= _resourceManager.GetPlatinumAmount());
    }

    public bool PlaceTemplate()
    {
        if (currentColor == whiteColor)
        {
            if (!HaveRequiredResources())
            {
                return false;
            }

            // use up resources
            _resourceManager.SubtractResources(lastMetal, lastElectricity, lastPlatinum);

            // make building
            GameObject building = Instantiate(_buildSitePrefab, tBuilding.transform.position, Quaternion.identity);
            building.transform.parent = _buildingParent.transform;
            building = building.transform.GetChild(0).gameObject;
            building.GetComponent<BuildSiteScript>().SetTypePrefab(tempPrefab);
            building.GetComponent<BuildSiteScript>().SetBuildingParent(_buildingParent);

            lastBuildingEditTime = Time.time;

            if (building.GetComponent<BuildSiteScript>().GetTypePrefab().tag == TagList.STATIC_COLLECTOR)
            {
                building.transform.localScale = new Vector3(4, 2, 1);
                building.transform.localPosition = new Vector3(0.5F, 0, 0);
            }
            else if (building.GetComponent<BuildSiteScript>().GetTypePrefab().tag == TagList.FACTORY)
            {
                building.transform.localScale = new Vector3(4, 4, 1);
                building.transform.localPosition = new Vector3(0.5F, 0.5F, 0);
            }

            building.GetComponent<SpriteRenderer>().sortingOrder = _sortingLayerOrderManager.ComputeSortingLayer(building.transform.position.y);

            // update A*
            AstarPath.active.UpdateGraphs(building.transform.parent.gameObject.GetComponent<BoxCollider2D>().bounds);

            // move the worker to build the places build site
            _clickManager.PlacedSite_MoveWorker(building.transform.parent.gameObject);

            if (leftShiftPressed)
            {
                return false;
            }
            Reset();
            return true;
        }
        return false;
    }

    private void ResetAllStates()
    {
        templateActive = false;
    }

    public void SetTemplateActive(bool value)
    {
        templateActive = value;
    }

    public void Reset()
    {
        ResetAllStates();
        if (tBuilding)
        {
            Destroy(tBuilding);
        }
    }

    private void CheckTemplateMoved()
    {
        //check if moved
        if(tBuilding)
        {
            bool moved = false;
            
            if (!(tBuilding.transform.position.Equals(lastSpot)))
            {
                moved = true;
            }

            if (moved)
            {
                CheckTemplateOverlap();
                lastSpot = new Vector2(tBuilding.transform.position.x, tBuilding.transform.position.y);
            }
        }
    }

    private void CheckTemplateOverlap()
    {
        if (!IsValidSpot())
        {
            currentColor = redColor;
            tBuilding.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = redColor;
        }
        else
        {
            currentColor = whiteColor;
            tBuilding.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = whiteColor;
        }
    }

    private bool IsValidSpot()
    {
        UpdateAllLists();

        Vector2 tBuildingPosition = tBuilding.transform.position;

        foreach (Vector2 spot in spotList)
        {
            if (tBuildingPosition.Equals(spot))
            {
                return false;
            }

            if (tBuilding.tag == TagList.STATIC_COLLECTOR)
            {
                if ((tBuildingPosition + new Vector2(1, 0)).Equals(spot))
                {
                    return false;
                }
            }
            else if (tBuilding.tag == TagList.FACTORY)
            {
                if ((tBuildingPosition + new Vector2(1, 0)).Equals(spot)
                    || (tBuildingPosition + new Vector2(0, 1)).Equals(spot)
                    || (tBuildingPosition + new Vector2(1, 1)).Equals(spot))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public List<Vector2> GetSpotList()
    {
        UpdateAllLists();
        return spotList;
    }

    public void UpdateAllLists()
    {
        UpdateBuildingList();
        UpdateUnitList();
        UpdateResourceList();
        UpdateEnvironmentList();
        UpdateSpotList();
    }

    private void UpdateBuildingList()
    {
        buildingList.Clear();
        
        // get buildings
        for (int i = 0; i < _buildingParent.transform.childCount; i++)
        {
            GameObject b = _buildingParent.transform.GetChild(i).gameObject;

            if (b.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals(SortingLayerList.BUILDING))
            {
                buildingList.Add(b);
            }
        }        
    }

    private void UpdateUnitList()
    {
        unitList.Clear();

        // get units
        for (int i = 0; i < _unitParent.transform.childCount; i++)
        {
            GameObject b = _unitParent.transform.GetChild(i).gameObject;
            if (b.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals(SortingLayerList.UNIT))
            {
                unitList.Add(b);
            }
        }
    }

    private void UpdateResourceList()
    {
        resourceList.Clear();

        // get resources
        for (int i = 0; i < _resourceParent.transform.childCount; i++)
        {
            GameObject b = _resourceParent.transform.GetChild(i).gameObject;

            if (b.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sortingLayerName.Equals(SortingLayerList.RESOURCE))
            {
                resourceList.Add(b);
            }
        }
    }

    private void UpdateEnvironmentList()
    {
        environmentList.Clear();

        // get environment
        for (int i = 0; i < _environmentParent.transform.childCount; i++)
        {
            GameObject b = _environmentParent.transform.GetChild(i).gameObject;

            if (b.transform.GetChild(0).gameObject.tag.Equals(TagList.MOUNTAIN))
            {
                environmentList.Add(b);
            }
        }
    }

    private void UpdateSpotList()
    {
        spotList.Clear();

        // get all building points
        foreach (GameObject b in buildingList)
        {
            spotList.Add(new Vector2(b.transform.position.x, b.transform.position.y));
            string bTag = b.transform.GetChild(0).gameObject.tag;

            if (bTag == TagList.STATIC_COLLECTOR)
            {
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y));
            }
            else if (bTag == TagList.BUILD_SITE
                && b.transform.GetChild(0).gameObject.GetComponent<BuildSiteScript>().GetTypePrefab().tag == TagList.STATIC_COLLECTOR)
            {
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y));
            }
            else if (bTag == TagList.FACTORY)
            {
                spotList.Add(new Vector2(b.transform.position.x, b.transform.position.y + 1));
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y));
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y + 1));
            }
            else if (bTag == TagList.BUILD_SITE
                && b.transform.GetChild(0).gameObject.GetComponent<BuildSiteScript>().GetTypePrefab().tag == TagList.FACTORY)
            {
                spotList.Add(new Vector2(b.transform.position.x, b.transform.position.y + 1));
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y));
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y + 1));
            }
            else if (bTag == TagList.CORE)
            {
                spotList.Add(new Vector2(b.transform.position.x, b.transform.position.y + 1));
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y));
                spotList.Add(new Vector2(b.transform.position.x + 1, b.transform.position.y + 1));
            }
        }

        float offset = 0.5F;

        // get all unit points
        foreach (GameObject u in unitList)
        {
            float x = u.transform.position.x;
            float y = u.transform.position.y;
            float xMod = 1;
            float yMod = 1;

            xMod = (x >= 0) ? 1 : -1;
            yMod = (y >= 0) ? 1 : -1;

            x = (int)x;
            x += xMod * offset;
            y = (int)y;
            y += yMod * offset;

            spotList.Add(new Vector2(x, y));
        }

        // get all resource points
        foreach (GameObject r in resourceList)
        {
            spotList.Add(new Vector2(r.transform.position.x, r.transform.position.y));
        }

        // get all environment points
        foreach (GameObject e in environmentList)
        {
            spotList.Add(new Vector2(e.transform.position.x, e.transform.position.y));
        }
    }

    private void SnapTemplatePlacement()
    {
        if (tBuilding)
        {
            float offset = 0.5F;
            Vector2 vec2 = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector3 newPlacement = new Vector3((int)vec2.x - offset, (int)vec2.y - offset, 0);

            tBuilding.transform.position = newPlacement;
        }
    }      

    public List<GameObject> GetUnitList()
    {
        UpdateUnitList();
        return unitList;
    }

    public List<GameObject> GetBuildingList()
    {
        UpdateBuildingList();
        return buildingList;
    }

    public List<GameObject> GetResourceList()
    {
        UpdateResourceList();
        return resourceList;
    }

    public float GetLastBuildingEditTime()
    {
        return lastBuildingEditTime;
    }

    public GameObject GetResourceParent()
    {
        return _resourceParent;
    }
}
