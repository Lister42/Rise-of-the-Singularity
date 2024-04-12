using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyMeleeScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private SpriteRenderer _spriteRenderer;
    private Build_Manager _buildManager;
    private BoxCollider2D _boxCollider2D;

    private int idleCounter = 0;
    private int attackingBuildingCounter = 0;
    private int attackingUnitCounter = 0;
    private bool stuckTimeSaved = false;
    private float stuckTime = 0.0f;
    private float stuckTimeDelay = 3f;

    #region State Variables
    private bool isIdle = true;
    private bool isMoving = false;
    private bool isAttackingUnit = false;
    private bool isAttackingBuilding = false;
    #endregion

    #region Intention Variables
    private bool goingToBeIdle = false;
    private bool goingToAttackUnit = false;
    private bool goingToAttackBuilding = false;
    #endregion

    #region Last Interacted With Variables
    private GameObject lastEnemyUnit;
    private GameObject lastBuilding;
    private UnitScript lastUnitScript;
    private GoodBuildingScript lastBuildingScript;
    private Vector2 lastUnitPosition;
    #endregion

    #region General Variables
    private Rigidbody2D _rbody;
    private Transform _transform;
    private Vector2 movePos;
    private float minimumDistanceForDetection = PathfindingValues.MINIMUM_DETECTION_DISTANCE;
    private float nextDetectionTime = 0.0f;
    private float nextDetectionIncrement = PathfindingValues.DETECTION_DELAY;

    private bool justArrivedToAttackBuilding = false;
    private bool justLeftAttackingBuilding = false;
    private bool justArrivedToAttackUnit = false;
    private bool justLeftAttackingUnit = false;
    #endregion

    #region Pathfinding Variables
    private Seeker _seeker;
    float nextWayPointDistance = PathfindingValues.NEXT_WAYPOINT_DISTANCE;
    private Path _path;
    private int _currentWaypoint = 0;
    private int maxWaypointIndex = 3;
    private bool reachedEndOfPath = false;
    private float lastGeneratePathTime = 0.0F;
    private float genPathDelay = PathfindingValues.GENERATE_PATH_DELAY;
    private float enemyMoveDistGen = PathfindingValues.UNIT_MOVE_DISTANCE_GENERATION;
    #endregion

    #region Sight Variables
    private float nextSightCheck = 0.0F;
    private float sightCheckDelay = PathfindingValues.SIGHT_DETECTION_DELAY;
    private UnitSight _unitSight;
    #endregion

    #region Animation
    private Animator _animator;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _buildManager = Build_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _transform = transform.parent;
        _unitSight = _transform.GetChild(1).GetComponent<UnitSight>();
        _boxCollider2D = _transform.GetComponent<BoxCollider2D>();

        //AI Initialization
        _seeker = _transform.gameObject.GetComponent<Seeker>();
        _rbody = _transform.gameObject.GetComponent<Rigidbody2D>();

        //Animation
        _animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        SetSortingLayerOrder();
        CheckAttackBuilding();
        CheckAttackUnit();
        isStuck();
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            PathFinder();
        }
        else if (isIdle || isAttackingBuilding || isAttackingUnit)
        {
            _rbody.velocity = new Vector2(0, 0);
            _animator.SetBool(AnimStateList.IS_IDLE_ENEMY_MELEE, true);
            _animator.SetBool(AnimStateList.IS_MOVING_DOWN_ENEMY_MELEE, false);
            _animator.SetBool(AnimStateList.IS_MOVING_UP_ENEMY_MELEE, false);
            _animator.SetBool(AnimStateList.IS_MOVING_LEFT_ENEMY_MELEE, false);
            _animator.SetBool(AnimStateList.IS_MOVING_RIGHT_ENEMY_MELEE, false);

        }

        if (isIdle && nextSightCheck <= Time.time)
        {
            nextSightCheck = Time.time + sightCheckDelay;
            IdleSightAttack();
        }
    }

    private void IdleSightAttack()
    {
        GameObject objectInSight = _unitSight.GetNextAttack();

        // if no object in sight to attack, then return
        if (!objectInSight)
        {
            return;
        }

        // there is an object to attack, so attack it
        string objectInSightTeamTag = objectInSight.tag;

        // check for unit or building
        if (objectInSightTeamTag.Equals(TagList.MELEE_UNIT) || objectInSightTeamTag.Equals(TagList.RANGED_UNIT) || objectInSightTeamTag.Equals(TagList.WORKER))
        {
            MoveAndAttackEnemyUnit(objectInSight, objectInSight.transform.position);
        }
        else if (objectInSightTeamTag.Equals(TagList.BUILD_SITE) || objectInSightTeamTag.Equals(TagList.CORE) 
            || objectInSightTeamTag.Equals(TagList.FACTORY) || objectInSightTeamTag.Equals(TagList.MINE)
            || objectInSightTeamTag.Equals(TagList.STATIC_COLLECTOR) || objectInSightTeamTag.Equals(TagList.TURRET)
            || objectInSightTeamTag.Equals(TagList.OUTPOST))
        {
            MoveAndAttackEnemyBuilding(objectInSight, objectInSight.transform.position);
        }
    }

    public void MoveToLocation(Vector2 location)
    {
        ResetAllStates();
        ResetAllIntentions();
        movePos = location;
        isMoving = true;
        goingToBeIdle = true;
        _currentWaypoint = 0;
        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

    public void MoveAndAttackEnemyUnit(GameObject enemyUnit, Vector2 location)
    {
        ResetAllStates();
        ResetAllIntentions();
        movePos = location;
        isMoving = true;
        goingToAttackUnit = true;
        lastEnemyUnit = enemyUnit;
        lastUnitPosition = location;
        lastUnitScript = enemyUnit.transform.GetChild(0).gameObject.GetComponent<UnitScript>();
        _currentWaypoint = 0;
        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

    public void MoveAndAttackEnemyBuilding(GameObject enemyBuilding, Vector2 location)
    {
        ResetAllStates();
        ResetAllIntentions();
        movePos = location;
        isMoving = true;
        goingToAttackBuilding = true;
        lastBuilding = enemyBuilding;
        lastBuildingScript = enemyBuilding.transform.GetChild(0).gameObject.GetComponent<GoodBuildingScript>();
        _currentWaypoint = 0;
        _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
        lastGeneratePathTime = Time.time;
    }

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

        CheckAttackBuildingIntentionIsWithinRange(); //Checks to see if within the minRange of a resource and will stop pre-emptively if so
        CheckAttackUnitIntentionIsWithinRange();

        if (distance <= nextWayPointDistance)
        {
            _currentWaypoint++;
        }

        if (nextDetectionTime <= Time.time)
        {
            nextDetectionTime = Time.time + nextDetectionIncrement;
            if (DetectObjectInPath())
            {
                _currentWaypoint = 0;
                _seeker.StartPath(_transform.position, new Vector3(movePos.x, movePos.y, 0), OnPathComplete);
                lastGeneratePathTime = Time.time;
            }

        }

        checkAnimation(direction);
    }

    void CheckAttackBuildingIntentionIsWithinRange()
    {
        if (goingToAttackBuilding && lastBuilding)
        {
            bool inRange = false;
            if (WithinRange(_transform.position, lastBuilding.transform.position))
            {
                inRange = true;
            }

            string buildingTag = lastBuilding.transform.GetChild(0).gameObject.tag;

            if (buildingTag.Equals(TagList.STATIC_COLLECTOR))
            {
                if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                }
            }
            else if (buildingTag.Equals(TagList.FACTORY))
            {
                if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(0, 1, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(1, 1, 0)))
                {
                    inRange = true;
                }
            }

            else if (buildingTag.Equals(TagList.CORE))
            {
                if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(1, 0, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(0, 1, 0)))
                {
                    inRange = true;
                }
                else if (WithinRange(_transform.position, lastBuilding.transform.position + new Vector3(1, 1, 0)))
                {
                    inRange = true;
                }
            }

            if (inRange)
            {
                print("In range to attack building");
                reachedEndOfPath = true;
                _currentWaypoint = 0;
                CompleteIntention();
                _rbody.velocity = new Vector2(0, 0);
                _path = null;
                justArrivedToAttackBuilding = true;
            }

        }
    }

    void CheckAttackUnitIntentionIsWithinRange()
    {
        if (goingToAttackUnit && lastEnemyUnit)
        {
            if (WithinRange(_transform.position, lastEnemyUnit.transform.position))
            {
                print("In range to attack enemy unit");
                reachedEndOfPath = true;
                _currentWaypoint = 0;
                CompleteIntention();
                _rbody.velocity = new Vector2(0, 0);
                _path = null;
                justArrivedToAttackUnit = true;
            }

        }
    }

    private void CheckAttackBuilding()
    {
        if (justArrivedToAttackBuilding)
        {
            lastBuildingScript.AddAttacker(gameObject);
            justArrivedToAttackBuilding = false;
        }
        else if (justLeftAttackingBuilding)
        {
            lastBuildingScript.RemoveAttacker(gameObject);
            justLeftAttackingBuilding = false;
        }

        if (isAttackingBuilding && !lastBuilding)
        {
            print("Building destroyed");
            isAttackingBuilding = false;
            goingToBeIdle = true;
            CompleteIntention();
        }
    }

    private void CheckAttackUnit()
    {
        if (justArrivedToAttackUnit)
        {
            lastUnitScript.AddAttacker(gameObject);
            justArrivedToAttackUnit = false;
        }
        else if (justLeftAttackingUnit)
        {
            lastUnitScript.RemoveAttacker(gameObject);
            justLeftAttackingUnit = false;

            // enemy moved so follow and attack
            if (isAttackingUnit && lastEnemyUnit)
            {
                MoveAndAttackEnemyUnit(lastEnemyUnit, lastEnemyUnit.transform.position);
            }
        }

        if (isAttackingUnit && !lastEnemyUnit)
        {
            print("Unit destroyed");
            isAttackingUnit = false;
            goingToBeIdle = true;
            CompleteIntention();
        }

        CheckEnemyUnitLocation();
    }

    private void CheckEnemyUnitLocation()
    {
        // check if the enemy unit has moved
        if (lastEnemyUnit)
        {
            if (isAttackingUnit && !WithinRange(_transform.position, lastEnemyUnit.transform.position))
            {
                justLeftAttackingUnit = true;
            }

            if (goingToAttackUnit)
            {
                if (lastGeneratePathTime + genPathDelay < Time.time)
                {
                    // check if unit moved
                    if (Vector2.Distance(lastUnitPosition, lastEnemyUnit.transform.position) >= enemyMoveDistGen)
                    {
                        MoveAndAttackEnemyUnit(lastEnemyUnit, lastEnemyUnit.transform.position);
                    }
                }
            }
        }
    }

    private void ResetAllStates()
    {
        if (isAttackingBuilding)
        {
            justLeftAttackingBuilding = true;
        }
        if (isAttackingUnit)
        {
            justLeftAttackingUnit = true;
        }
        isIdle = false;
        isMoving = false;
        isAttackingUnit = false;
        isAttackingBuilding = false;
    }

    private void ResetAllIntentions()
    {
        goingToBeIdle = false;
        goingToAttackUnit = false;
        goingToAttackBuilding = false;
    }

    private void CompleteIntention()
    {
        if (goingToBeIdle)
        {
            idleCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isIdle = true;
            print("Melee Robot is now idle: " + idleCounter);
        }
        else if (goingToAttackBuilding)
        {
            attackingBuildingCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isAttackingBuilding = true;
            print("Melee Robot is now attacking a building: " + attackingBuildingCounter);
        }
        else if (goingToAttackUnit)
        {
            attackingUnitCounter++;
            ResetAllStates();
            ResetAllIntentions();
            isAttackingUnit = true;
            print("Melee Robot is now attacking a unit: " + attackingUnitCounter);
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
        return Vector2.Distance(workerPosition, otherPosition) <= UnitRangesList.MELEE;
    }
    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void checkAnimation(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_LEFT_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_RIGHT_ENEMY_MELEE, true);
            }
            else
            {
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_LEFT_ENEMY_MELEE, true);
                _animator.SetBool(AnimStateList.IS_MOVING_RIGHT_ENEMY_MELEE, false);
            }
            _animator.SetBool(AnimStateList.IS_IDLE_ENEMY_MELEE, false);
        }
        else
        {
            if (direction.y > 0)
            {
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_ENEMY_MELEE, true);
                _animator.SetBool(AnimStateList.IS_MOVING_LEFT_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_RIGHT_ENEMY_MELEE, false);
            }
            else
            {
                _animator.SetBool(AnimStateList.IS_MOVING_DOWN_ENEMY_MELEE, true);
                _animator.SetBool(AnimStateList.IS_MOVING_UP_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_LEFT_ENEMY_MELEE, false);
                _animator.SetBool(AnimStateList.IS_MOVING_RIGHT_ENEMY_MELEE, false);
            }
            _animator.SetBool(AnimStateList.IS_IDLE_ENEMY_MELEE, false);
        }
    }

    public bool getIsIdle()
    {
        return isIdle;
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
