using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class BuildingSight : MonoBehaviour
{
    private Transform _parentTransform;
    private CircleCollider2D _circleCollider2D;
    private float unitSight = 0.0F;
    private string unitTeamTag = "";
    private string unitTypeTag = "";
    private bool sightActivated = true;
    public List<GameObject> sightAttackList;

    // Start is called before the first frame update
    void Start()
    {
        sightAttackList = new List<GameObject>();
        _circleCollider2D = GetComponent<CircleCollider2D>();

        _parentTransform = transform.parent;
        unitTeamTag = _parentTransform.tag;
        unitTypeTag = _parentTransform.GetChild(0).tag;

        SetUpSpecificUnitVariables();
        _circleCollider2D.radius = unitSight;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetUpSpecificUnitVariables()
    {
        if (unitTypeTag.Equals(TagList.TURRET))
        {
            unitSight = BuildingSightList.TURRET_SIGHT;
        }
        else if (unitTypeTag.Equals(TagList.OUTPOST))
        {
            unitSight = BuildingSightList.OUTPOST_SIGHT;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (sightActivated)
        {
            // check if the collision is with a unit
            if (collision.transform.GetChild(0).GetComponent<Unit_UIHandler>())
            {
                //print("SIGHT COLLIDED, " + _parentTransform.tag + " hit by " + collision.transform.tag);
                GameObject parentObject = collision.gameObject;
                CollisionDetected(parentObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // check if the collision exit is with a unit
        if (collision.transform.GetChild(0).GetComponent<Unit_UIHandler>())
        {
            //print("SIGHT COLLIDED EXIT UNIT");
            GameObject parentObject = collision.gameObject;
            CheckRemoveAttackList(parentObject);
        }
    }

    public void CollisionDetected(GameObject collidedObject)
    {
        string collidedTeamTag = collidedObject.tag;
        //string collidedUnitTag = collidedObject.transform.GetChild(0).tag;

        // Have to do this so units on the same team don't get added to collision list
        if (unitTeamTag.Equals(TagList.TURRET) || unitTeamTag.Equals(TagList.OUTPOST))
        {
            GoodUnitLogic(collidedObject, collidedTeamTag);
        }
        else if (unitTeamTag.Equals(TagList.ENEMY_BUILDING))
        {
            EnemyUnitLogic(collidedObject, collidedTeamTag);
        }
    }



    private void GoodUnitLogic(GameObject collidedObject, string collidedTeamTag)
    {
        if (collidedTeamTag.Equals(TagList.ENEMY_UNIT))
        {
            sightAttackList.Add(collidedObject);
        }
    }

    private void EnemyUnitLogic(GameObject collidedObject, string collidedTeamTag)
    {
        if (collidedTeamTag.Equals(TagList.MELEE_UNIT) || collidedTeamTag.Equals(TagList.RANGED_UNIT)
            || collidedTeamTag.Equals(TagList.WORKER))
        {
            sightAttackList.Add(collidedObject);
        }
    }

    public void CheckRemoveAttackList(GameObject colExitObject)
    {
        for (int i = sightAttackList.Count - 1; i >= 0; i--)
        {
            if (ReferenceEquals(sightAttackList[i], colExitObject))
            {
                sightAttackList.RemoveAt(i);
            }
        }
    }



    public GameObject GetNextAttack()
    {
        return GetClosestObject();
    }

    private GameObject GetClosestObject()
    {
        // remove null objects (objects that have been destroyed)
        RemoveNullAttackList();

        if (sightAttackList.Count <= 0)
        {
            return null;
        }

        float closestDistance = 100.0F;
        GameObject closestObj = null;

        foreach (GameObject obj in sightAttackList)
        {
            if (Vector2.Distance(transform.position, obj.transform.position) < closestDistance)
            {
                closestDistance = Vector2.Distance(transform.position, obj.transform.position);
                closestObj = obj;
            }
        }

        return closestObj;
    }

    private void RemoveNullAttackList()
    {
        sightAttackList.RemoveAll(item => item == null);
    }

    public void SetSightActivated(bool b)
    {
        sightActivated = b;
    }
}
