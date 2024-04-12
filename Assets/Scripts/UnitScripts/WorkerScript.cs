using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class WorkerScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private Resource_Manager _resourceManager;
    //private SpriteFacing_Manager _spriteFacing_Manager;
    private Build_Manager _buildManager;
    private SpriteRenderer _spriteRenderer;
    private GameObject closestLocation = null;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    private InteractiveAudio_Manager _audioManager;

    int idleCounter = 0; //DELETE ME
    int platinumCounter = 0; //DELETE ME
    int metalCounter = 0; //DELETE ME
    int staticCounter = 0; //DELETE ME
    int buildCounter = 0; //DELETE ME
    private float nextHarvestTime = 0;
    private bool justArrivedBuildSite = false;
    private bool justLeftBuildSite = false;
    private GameObject _resourceParent;
    private string lastResourceTag;
    private bool stuckTimeSaved = false;
    private float stuckTime = 0.0f;
    private float stuckTimeDelay = 3f;

    #region State Variables
    private bool isIdle = true;
    private bool isMoving = false;
    private bool isBuilding = false;
    private bool isMiningMetal = false;
    private bool isMiningPlatinum = false;
    private bool isCollectingElectricity = false;
    private bool droppingOffAtCollector = false;
    private bool droppingOffAtCore = false;
    #endregion

    #region Intention Variables
    bool goingToBeIdle = false;
    bool goingToBuildBuilding = false;
    bool goingToMineMetal = false;
    bool goingToMinePlatinum = false;
    bool goingToCollectElectricity = false;
    bool goingToDropOffResources = false;
    #endregion

    #region Last Interacted With Variables
    GameObject lastResource;
    PlatinumScript lastPlatinumScript;
    MetalScript lastMetalScript;
    StaticScript lastStaticScript;
    GameObject lastBuildingSite;
    BuildSiteScript lastBuildingSiteScript;
    #endregion

    #region Worker Inventory
    private int metalAmount = 0;
    private int platinumAmount = 0;
    private int electricityAmount = 0;
    private int MAX_RESOURCE = 10;
    #endregion

    #region Pathfinding Variables
    float nextWayPointDistance = PathfindingValues.NEXT_WAYPOINT_DISTANCE;
    private Path _path;
    private int _currentWaypoint = 0;
    private int maxWaypointIndex = 3;
    private bool reachedEndOfPath = false;
    private float lastGeneratePathTime = 0.0F;
    private Seeker _seeker;
    #endregion

    #region General Variables
    private Rigidbody2D _rbody;
    private Transform _transform;
    private List<GameObject> buildingList;
    private GameObject _buildingParent;
    private Setup_Manager _setupManager;
    private bool resourceWasValid = false;
    private Vector2 movePos;
    private float minimumDistanceForDetection = PathfindingValues.MINIMUM_DETECTION_DISTANCE;
    private float nextDetectionTime = 0.0F;
    private float nextDetectionIncrement = PathfindingValues.DETECTION_DELAY;
    private bool justDeposit = false;
    #endregion

    #region Sight Variables
    private float nextSightCheck = 0.0F;
    private float sightCheckDelay = PathfindingValues.SIGHT_DETECTION_DELAY;
    private UnitSight _unitSight;
    #endregion

    #region Particle Variables
    public ParticleSystem _platParticles;
    public ParticleSystem _metalParticles;
    public ParticleSystem _staticParticles;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _setupManager = Setup_Manager._Instance;
        _resourceManager = Resource_Manager._Instance;
        _buildManager = Build_Manager._Instance;
        //_spriteFacing_Manager = SpriteFacing_Manager._Instance;
        buildingList = new List<GameObject>();
        _transform = transform.parent;
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _audioManager = InteractiveAudio_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = _transform.GetComponent<BoxCollider2D>();
        _unitSight = _transform.GetChild(1).GetComponent<UnitSight>();
        _animator = GetComponent<Animator>();
        Setup();

        //AI Initialization
        _seeker = _transform.gameObject.GetComponent<Seeker>();
        _rbody = _transform.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        SetSortingLayerOrder();
        Harvest();
        Build();
        isStuck();
    }

    private void Setup()
    {
        _buildingParent = _setupManager.GetBuildingParent();
        _resourceParent = Build_Manager._Instance.GetResourceParent();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            PathFinder();
        }
        else if (isIdle || isMiningMetal || isMiningPlatinum || isCollectingElectricity || isBuilding) //and isAttacking
        {
            _rbody.velocity = new Vector2(0, 0);
            _animator.SetBool(AnimStateList.IS_IDLE_WORKER, true);
            _animator.SetBool(AnimStateList.IS_MOVING_DOWN_WORKER, false);
            _animator.SetBool(AnimStateList.IS_MOVING_HORIZONTAL_WORKER, false);
            _animator.SetBool(AnimStateList.IS_MOVING_UP_WORKER, false);
        }
    }

    public void MoveToLocation(Vector2 location)
    {
        CheckParticleStops();
        ResetAllStates();
        ResetAllIntentions();
        movePos = location;
        isMoving = true;
        goingToBeIdle = true;
        _currentWaypoint = 0;
        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

    public void MoveAndCollectResource(GameObject resource, Vector2 location)
    {
        CheckParticleStops();
        ResetAllStates();
        ResetAllIntentions();
        justDeposit = false;
        movePos = location;
        isMoving = true;
        lastResource = resource;
        lastResourceTag = resource.tag;
        if (resource.tag.Equals(TagList.METAL))
        {
            lastMetalScript = resource.transform.GetChild(0).gameObject.GetComponent<MetalScript>();
            goingToMineMetal = true;
            electricityAmount = 0;
            platinumAmount = 0;
        }
        else if (resource.tag.Equals(TagList.PLATINUM))
        {
            lastPlatinumScript = resource.transform.GetChild(0).gameObject.GetComponent<PlatinumScript>();
            goingToMinePlatinum = true;
            metalAmount = 0;
            electricityAmount = 0;
        }
        else if (resource.tag.Equals(TagList.STATIC))
        {
            lastStaticScript = resource.transform.GetChild(0).gameObject.GetComponent<StaticScript>();
            goingToCollectElectricity = true;
            metalAmount = 0;
            platinumAmount = 0;
        }
        _currentWaypoint = 0;
        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

    public void MoveAndBuildBuilding(GameObject building, Vector2 location)
    {
        if (isBuilding && lastBuildingSiteScript)
        {
            lastBuildingSiteScript.RemoveWorker();
            justLeftBuildSite = false;          
        }
        CheckParticleStops();
        ResetAllStates();
        ResetAllIntentions();
        movePos = location;
        isMoving = true;
        goingToBuildBuilding = true;
        lastBuildingSite = building;
        lastBuildingSiteScript = building.transform.GetChild(0).gameObject.GetComponent<BuildSiteScript>();
        _currentWaypoint = 0;
        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

    public void MoveAndDeposit(GameObject building, Vector2 location)
    {
        string tag = building.tag;
        bool valid = false;
        if (tag.Equals(TagList.CORE) && (metalAmount > 0 || electricityAmount > 0 || platinumAmount > 0))
        {
            valid = true;
        }
        else if (tag.Equals(TagList.MINE) && (metalAmount > 0 || platinumAmount > 0))
        {
            valid = true;
        }
        else if (tag.Equals(TagList.STATIC_COLLECTOR) && (electricityAmount > 0))
        {
            valid = true;
        }

        CheckParticleStops();
        if (valid)
        {
            justDeposit = true;
            closestLocation = building;
            movePos = location;
            DepositResources(location);
        }
    }

    //Pathfinding Functions
    void PathFinder()
    {
        if (_path == null)
        {
            return;
        }
        if (_currentWaypoint >= _path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            _currentWaypoint = 0;
            CompleteIntention();
            _rbody.velocity = new Vector2(0f, 0f);
            _path = null;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)_path.vectorPath[_currentWaypoint] - (Vector2)_transform.position).normalized;
        _rbody.velocity = direction;
        float distance = Vector2.Distance(_transform.position, _path.vectorPath[_currentWaypoint]);

        CheckResourceIntentionIsWithinRange(); //Checks to see if within the minRange of a resource and will stop pre-emptively if so
        CheckBuildingDropResIntentionIsWithinRange();
        CheckBuildSiteIntentionIsWithinRange();

        if (distance <= nextWayPointDistance)
        {
            _currentWaypoint++;
        }

        if (nextDetectionTime <= Time.time)
        {
            nextDetectionTime = Time.time + nextDetectionIncrement;
            if (DetectObjectInPath())
            {
                print("OBJECT IN PATH");
                _currentWaypoint = 0;
                _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
                lastGeneratePathTime = Time.time;
            }
        }

        //_spriteFacing_Manager.changeFacingDirection(gameObject, direction);
        checkAnimation(direction);
    }

    void CompleteIntention()
    {
        if (goingToBeIdle)
        {
            idleCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isIdle = true;
            print("Robot is now idle: " + idleCounter);
        }
        else if (goingToMineMetal)
        {
            metalCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isMiningMetal = true;
            StartCollectingMetal();
            print("Robot is now mining metal: " + metalCounter);
        }
        else if(goingToMinePlatinum)
        {
            platinumCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isMiningPlatinum = true;
            StartCollectingPlatinum();
            print("Robot is now mining platinum: " + platinumCounter);
        }
        else if(goingToCollectElectricity)
        {
            staticCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isCollectingElectricity = true;
            StartCollectingStatic();
            print("Robot is now collecting electricity: " + staticCounter);
        }
        else if (goingToDropOffResources)
        {
            _resourceManager.AddResources(metalAmount, electricityAmount, platinumAmount);
            metalAmount = 0;
            electricityAmount = 0;
            platinumAmount = 0;
            if (lastResource && !justDeposit)
            {
                MoveAndCollectResource(lastResource, lastResource.transform.position);
            }
            else if (!lastResource)
            {
                GameObject closestResource = GetClosestResource();
                if (closestResource && CheckCloseResourceRange(closestResource))
                {
                    MoveAndCollectResource(closestResource, closestResource.transform.position);
                }
            }
        }
        else if (goingToBuildBuilding)
        {
            buildCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isBuilding = true;
            print("Robot is now building: " + buildCounter);
        }
        else
        {
            
        }
    }

    void CheckBuildingDropResIntentionIsWithinRange()
    {
        if (goingToDropOffResources && closestLocation)
        {
            bool inRange = false;
            if (WithinRange(_transform.position, closestLocation.transform.position))
            {
                inRange = true;
            }

            if (droppingOffAtCollector)
            {
                if (WithinRange(_transform.position, closestLocation.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                }
            }
            else if (droppingOffAtCore)
            {
                if (WithinRange(_transform.position, closestLocation.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                } 
                else if (WithinRange(_transform.position, closestLocation.transform.position + new Vector3(0, 1, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, closestLocation.transform.position + new Vector3(1, 1, 0)))
                {
                    inRange = true;
                }
            }

            if (inRange)
            {
                print("In Building Range");
                reachedEndOfPath = true;
                _currentWaypoint = 0;
                CompleteIntention();
                _rbody.velocity = new Vector2(0f, 0f);
                _path = null;
            }
        }
    }

    void CheckResourceIntentionIsWithinRange()
    {
        if ((goingToMineMetal || goingToMinePlatinum || goingToCollectElectricity) && lastResource)
        {
            if (WithinRange(_transform.position, lastResource.transform.position))
            {
                print("In Resource Range");
                reachedEndOfPath = true;
                _currentWaypoint = 0;
                CompleteIntention();
                _rbody.velocity = new Vector2(0f, 0f);
            }
        }
    }

    void CheckBuildSiteIntentionIsWithinRange()
    {
        if (goingToBuildBuilding && lastBuildingSite)
        {
            bool inRange = false;
            if (WithinRange(_transform.position, lastBuildingSite.transform.position))
            {
                inRange = true;
            }

            string buildingTag = lastBuildingSiteScript.GetTypePrefab().tag;
            if (buildingTag.Equals(TagList.STATIC_COLLECTOR))
            {
                if (WithinRange(_transform.position, lastBuildingSite.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                }
            }
            else if (buildingTag.Equals(TagList.FACTORY))
            {
                if (WithinRange(_transform.position, lastBuildingSite.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, lastBuildingSite.transform.position + new Vector3(0, 1, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, lastBuildingSite.transform.position + new Vector3(1, 1, 0)))
                {
                    inRange = true;
                }
            }

            if (inRange)
            {
                print("In Build Site Range");
                reachedEndOfPath = true;
                _currentWaypoint = 0;
                CompleteIntention();
                _rbody.velocity = new Vector2(0f, 0f);
                _path = null;
                justArrivedBuildSite = true;
            }
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            _path = p;
            _currentWaypoint = 0;
        }
    }

    private bool WithinRange(Vector3 workerPosition, Vector3 otherPosition)
    {
        return Vector2.Distance(workerPosition, otherPosition) <= UnitRangesList.WORKER;
    }

    private void Harvest()
    {
        bool deposit = false;
        if (isMiningMetal || isMiningPlatinum || isCollectingElectricity)
        {
            if (!HasMaxResources())
            {
                // check if resource is still there
                if (lastResource)
                {
                    // check distance from the worker to the harvesting resource
                    if (WithinRange(_transform.position, lastResource.transform.position))
                    {
                        if (Time.time > nextHarvestTime)
                        {
                            if (isMiningMetal && lastMetalScript)
                            {
                                resourceWasValid = true;
                                metalAmount++;
                                lastMetalScript.DecrementResource();
                                if (_spriteRenderer.isVisible)
                                {
                                    _audioManager.workerHarvestOre();
                                }
                                nextHarvestTime = Time.time + WorkerRateList.HARVEST_METAL_RATE;
                            }

                            if (isMiningPlatinum && lastPlatinumScript)
                            {
                                resourceWasValid = true;
                                platinumAmount++;
                                lastPlatinumScript.DecrementResource();
                                if (_spriteRenderer.isVisible)
                                {
                                    _audioManager.workerHarvestOre();
                                }
                                nextHarvestTime = Time.time + WorkerRateList.HARVEST_PLATINUM_RATE;
                            }

                            if (isCollectingElectricity && lastStaticScript)
                            {
                                resourceWasValid = true;
                                electricityAmount++;
                                lastStaticScript.DecrementResource();
                                if (_spriteRenderer.isVisible)
                                {
                                    _audioManager.workerHarvestOre();
                                }
                                _audioManager.workerHarvestStatic();
                                nextHarvestTime = Time.time + WorkerRateList.HARVEST_STATIC_RATE;
                            }
                        }
                    }
                    else
                    {
                        // not within distance so move and collect
                        print("REVALUTE TO COLLECT RESOURCE");
                        MoveAndCollectResource(lastResource, lastResource.transform.position);
                    }
                }
                else
                {
                    CheckParticleStops();

                    GameObject closestResource = GetClosestResource();
                    if (closestResource && CheckCloseResourceRange(closestResource))
                    {
                        MoveAndCollectResource(closestResource, closestResource.transform.position);
                    }
                    else
                    {
                        // resource destroyed and none close enough so idle
                        ResetAllStates();
                        ResetAllIntentions();
                        justDeposit = false;
                        goingToBeIdle = true;
                        CompleteIntention();
                    }
                }
            }
            else
            {
                deposit = true;
            }
        }
        else
        {
            StopAllParticles();
        }

        if (deposit || (( HasMaxResources() || !lastResource) && resourceWasValid))
        {
            DepositResources();
        }
    }

    private void DepositResources()
    {
        CheckParticleStops();
        UpdateBuildingList();
        GameObject closestDepositSpot = GetClosestDropOff(!(electricityAmount > 0));
        movePos = new Vector2(closestDepositSpot.transform.position.x, closestDepositSpot.transform.position.y);

        DepositResources(movePos);
    }

    private void DepositResources(Vector2 depositSpot)
    {
        resourceWasValid = false;
        ResetAllStates();
        ResetAllIntentions();
        goingToDropOffResources = true;
        isMoving = true;

        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

    private void UpdateBuildingList()
    {
        buildingList.Clear();

        buildingList = _buildManager.GetBuildingList();
    }

    private GameObject GetClosestDropOff(bool mine)
    {
        float closestDistance = 100.0F;

        foreach (GameObject building in buildingList)
        {
            if (mine)
            {
                if (building.tag.Equals(TagList.MINE))
                {
                    if (Vector2.Distance(_transform.position, building.transform.position) < closestDistance)
                    {
                        closestDistance = Vector2.Distance(_transform.position, building.transform.position);
                        closestLocation = building;
                        droppingOffAtCollector = false;
                        droppingOffAtCore = false;
                    }
                }
            }
            else
            {
                if (building.tag.Equals(TagList.STATIC_COLLECTOR))
                {
                    if (Vector2.Distance(_transform.position, building.transform.position) < closestDistance)
                    {
                        closestDistance = Vector2.Distance(_transform.position, building.transform.position);
                        closestLocation = building;
                        droppingOffAtCollector = true;
                        droppingOffAtCore = false;
                    }
                }
            }

            if (building.tag.Equals(TagList.CORE))
            {
                if (Vector2.Distance(_transform.position, building.transform.position) < closestDistance)
                {
                    closestDistance = Vector2.Distance(_transform.position, building.transform.position);
                    closestLocation = building;
                    droppingOffAtCollector = false;
                    droppingOffAtCore = true;
                }
            }

        }

        return closestLocation;
    }

    private void Build()
    {
        if (justArrivedBuildSite)
        {
            lastBuildingSiteScript.AddWorker();
            justArrivedBuildSite = false;
        }
        else if (justLeftBuildSite)
        {
            lastBuildingSiteScript.RemoveWorker();
            justLeftBuildSite = false;
        }

        if (isBuilding && !lastBuildingSite)
        {
            print("build just finished");
            isBuilding = false;
            goingToBeIdle = true;
            CompleteIntention();
        }
    }

    private bool DetectObjectInPath()
    {
        float errorAmount = 0.01F;
        List<GameObject> units = _unitSight.GetPathSightList();

        // detect any units
        foreach (GameObject u in units)
        {
            if (!u)
            {
                continue;
            }
            // don't detect itself
            if (Vector2.Distance(u.transform.position, _transform.position) <= errorAmount)
            {
                continue;
            }

            if (Vector2.Distance(_transform.position, u.transform.position) <= minimumDistanceForDetection)
            {
                if (IsInfront(_transform.position, u.transform.position))
                {
                    return true;
                }
            }
        }

        // detect any new buildings
        if (_buildManager.GetLastBuildingEditTime() > lastGeneratePathTime)
        {
            return true;
        }

        return false;
    }

    private bool IsInfront(Vector2 unitPosition, Vector2 obstaclePosition)
    {
        // could be a way to help with jitteryness
        return true;
    }


    private void ResetAllStates()
    {
        if (isBuilding)
        {
            justLeftBuildSite = true;
        }
        isIdle = false;
        isMoving = false;
        isBuilding = false;
        isMiningMetal = false;
        isMiningPlatinum = false;
        isCollectingElectricity = false;
        droppingOffAtCollector = false;
        droppingOffAtCore = false;
    }

    private void ResetAllIntentions()
    {
        goingToBeIdle = false;
        goingToBuildBuilding = false;
        goingToMineMetal = false;
        goingToMinePlatinum = false;
        goingToCollectElectricity = false;
        goingToDropOffResources = false;
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void StartCollectingMetal()
    {
        if (lastResource)
        {
            Vector2 particleScale = _metalParticles.gameObject.transform.localScale;
            float offsetx = 1f / particleScale.x;
            float offsety = 1f / particleScale.y;
            Vector2 resourcePosition = lastResource.transform.position;
            Vector2 workerPosition = _transform.position;
            Vector2 pointVector = (resourcePosition - workerPosition).normalized;
            Vector2 particlePosition = new Vector2(pointVector.x * offsetx * particleScale.x, pointVector.y * offsety * particleScale.y);
            _metalParticles.gameObject.transform.localPosition = particlePosition;
            _metalParticles.Play();
        }
    }
    private void StopCollectingMetal()
    {
        _metalParticles.Stop();
    }
    private void StartCollectingStatic()
    {
        if (lastResource)
        {
            float playerYoffset = 0.5f;
            float staticYoffset = 0.5f;
            Vector2 particleScale = _staticParticles.gameObject.transform.localScale;
            Vector3 flowDirection = (new Vector3(_transform.position.x, _transform.position.y + playerYoffset) - new Vector3(lastResource.transform.position.x, lastResource.transform.position.y + staticYoffset)).normalized;
            float offsetx = 3.2f / particleScale.x;
            float offsety = 4f / particleScale.y;
            Vector2 resourcePosition = lastResource.transform.position;
            Vector2 workerPosition = _transform.position;
            Vector2 pointVector = (resourcePosition - workerPosition).normalized;
            Vector2 particlePosition = new Vector2(pointVector.x * offsetx * particleScale.x - 0.2F, pointVector.y * offsety * particleScale.y);
            _staticParticles.gameObject.transform.localPosition = particlePosition;
            _staticParticles.gameObject.transform.rotation = Quaternion.LookRotation(flowDirection, Vector3.up);
            _staticParticles.Play();
        }
    }
    private void StopCollectingStatic()
    {
        _staticParticles.Stop();
    }
    private void StartCollectingPlatinum()
    {
        if (lastResource)
        {
            Vector2 particleScale = _platParticles.gameObject.transform.localScale;
            float offsetx = 1f / particleScale.x;
            float offsety = 1f / particleScale.y;
            Vector2 resourcePosition = lastResource.transform.position;
            Vector2 workerPosition = _transform.position;
            Vector2 pointVector = (resourcePosition - workerPosition).normalized;
            Vector2 particlePosition = new Vector2(pointVector.x * offsetx * particleScale.x, pointVector.y * offsety * particleScale.y);
            _platParticles.gameObject.transform.localPosition = particlePosition;
            _platParticles.Play();
        }
    }
    private void StopCollectingPlatinum()
    {
        _platParticles.Stop();
    }

    private void CheckParticleStops()
    {
        if (isMiningMetal)
        {
            StopCollectingMetal();
        }
        if (isMiningPlatinum)
        {
            StopCollectingPlatinum();
        }
        if (isCollectingElectricity)
        {
            StopCollectingStatic();
        }
    }

    public int GetMetalAmount()
    {
        return metalAmount;
    }
    public int GetElectricityAmount()
    {
        return electricityAmount;
    }
    public int GetPlatinumAmount()
    {
        return platinumAmount;
    }

    public void checkAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                _animator.SetBool(AnimStateList.IS_MOVING_HORIZONTAL_WORKER, true);
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_WORKER, false);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_WORKER, false);
                _animator.SetBool(AnimStateList.IS_IDLE_WORKER, false);
                _spriteRenderer.flipX = true;
            }
            else
            {
                _animator.SetBool(AnimStateList.IS_MOVING_HORIZONTAL_WORKER, true);
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_WORKER, false);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_WORKER, false);
                _animator.SetBool(AnimStateList.IS_IDLE_WORKER, false);
                _spriteRenderer.flipX = false;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                _animator.SetBool(AnimStateList.IS_MOVING_HORIZONTAL_WORKER, false);
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_WORKER, false);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_WORKER, true);
                _animator.SetBool(AnimStateList.IS_IDLE_WORKER, false);
            }
            else
            {
                _animator.SetBool(AnimStateList.IS_MOVING_HORIZONTAL_WORKER, false);
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_WORKER, true);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_WORKER, false);
                _animator.SetBool(AnimStateList.IS_IDLE_WORKER, false);
            }
        }
    }

    public bool GetMoving()
    {
        return isMoving;
    }

    private void StopAllParticles()
    {
        _metalParticles.Stop();
        _staticParticles.Stop();
        _platParticles.Stop();
    }

    private bool HasMaxResources()
    {
        return metalAmount >= MAX_RESOURCE || platinumAmount >= MAX_RESOURCE || electricityAmount >= MAX_RESOURCE;
    }

    private GameObject GetClosestResource()
    {
        List<GameObject> resourceList = new List<GameObject>();
        Transform parentTransform = _resourceParent.transform;

        for (int i = 0; i < parentTransform.childCount; i++)
        {
            GameObject child = parentTransform.GetChild(i).gameObject;

            if (child.tag.Equals(lastResourceTag) && CheckCloseResourceRange(child))
            {
                resourceList.Add(child);
            }
        }
        print("< " + resourceList.Count + " >");
        // get closest resource
        if (resourceList.Count > 0)
        {
            float closestDistance = 100.0F;
            GameObject closestObj = null;

            foreach (GameObject obj in resourceList)
            {
                if (Vector2.Distance(_transform.position, obj.transform.position) < closestDistance)
                {
                    closestDistance = Vector2.Distance(transform.position, obj.transform.position);
                    closestObj = obj;
                }
            }

            return closestObj;
        }

        return null;
    }

    private bool CheckCloseResourceRange(GameObject resource)
    {
        return Vector2.Distance(_transform.position, resource.transform.position) <= UnitRangesList.WORKER_SIGHT;
    }

    private void isStuck()
    {
        if (!stuckTimeSaved && _rbody.velocity.magnitude < .05f && isMoving)
        {
            stuckTime = Time.time;
            stuckTimeSaved = true;
        } 
        else if (stuckTimeSaved && stuckTime + stuckTimeDelay <= Time.time)
        {
            if (_rbody.velocity.magnitude < .05f && isMoving)
            {
                StartCoroutine(MakeUnstuck());
                stuckTimeSaved = false;
            }
        }
    }

    private IEnumerator MakeUnstuck()
    {
        _boxCollider2D.enabled = false;
        yield return new WaitForSeconds(2);
        _boxCollider2D.enabled = true;
    }
}
