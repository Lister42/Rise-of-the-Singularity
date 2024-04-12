using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFacing_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static SpriteFacing_Manager _Instance { get; private set; } = null;

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

    #region Worker Idle Sprites
    public Sprite workerFacingDown;
    public Sprite workerFacingUp;
    public Sprite workerFacingLeft;
    public Sprite workerFacingRight;
    #endregion

    #region Melee Idle Sprites
    public Sprite meleeFacingDown;
    public Sprite meleeFacingUp;
    public Sprite meleeFacingLeft;
    public Sprite meleeFacingRight;
    #endregion

    #region Ranged Idle Sprites
    public Sprite rangedFacingDown;
    public Sprite rangedFacingUp;
    public Sprite rangedFacingLeft;
    public Sprite rangedFacingRight;
    #endregion

    #region Enemy Melee Idle Sprites
    public Sprite enemyMeleeFacingDown;
    public Sprite enemyMeleeFacingUp;
    public Sprite enemyMeleeFacingLeft;
    public Sprite enemyMeleeFacingRight;
    #endregion

    #region Enemy Ranged Idle Sprites
    public Sprite enemyRangedFacingDown;
    public Sprite enemyRangedFacingUp;
    public Sprite enemyRangedFacingLeft;
    public Sprite enemyRangedFacingRight;
    #endregion


    public void changeFacingDirection(GameObject unit, Vector2 direction)
    {
        string teamTag = unit.transform.parent.tag;
        string typeTag = unit.tag;

        if (typeTag.Equals(TagList.WORKER))
        {
            SpriteRenderer workerImage = unit.GetComponent<SpriteRenderer>();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    workerImage.sprite = workerFacingRight;
                    workerImage.flipX = true;
                }
                else
                {
                    workerImage.sprite = workerFacingLeft;
                    workerImage.flipX = false;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    workerImage.sprite = workerFacingUp;
                }
                else
                {
                    workerImage.sprite = workerFacingDown;
                }
            }
        }
        else if (teamTag.Equals(TagList.MELEE_UNIT) && typeTag.Equals(TagList.MELEE_UNIT))
        {
            SpriteRenderer meleeImage = unit.GetComponent<SpriteRenderer>();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    meleeImage.sprite = meleeFacingRight;
                }
                else
                {
                    meleeImage.sprite = meleeFacingLeft;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    meleeImage.sprite = meleeFacingUp;
                }
                else
                {
                    meleeImage.sprite = meleeFacingDown;
                }
            }
        }
        else if (teamTag.Equals(TagList.RANGED_UNIT) && typeTag.Equals(TagList.RANGED_UNIT))
        {
            SpriteRenderer rangedImage = unit.GetComponent<SpriteRenderer>();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    rangedImage.sprite = rangedFacingRight;
                }
                else
                {
                    rangedImage.sprite = rangedFacingLeft;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    rangedImage.sprite = rangedFacingUp;
                }
                else
                {
                    rangedImage.sprite = rangedFacingDown;
                }
            }
        }
        else if (teamTag.Equals(TagList.ENEMY_UNIT) && typeTag.Equals(TagList.MELEE_UNIT))
        {
            SpriteRenderer enemyMeleeImage = unit.GetComponent<SpriteRenderer>();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    enemyMeleeImage.sprite = enemyMeleeFacingRight;
                }
                else
                {
                    enemyMeleeImage.sprite = enemyMeleeFacingLeft;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    enemyMeleeImage.sprite = enemyMeleeFacingUp;
                }
                else
                {
                    enemyMeleeImage.sprite = enemyMeleeFacingDown;
                }
            }
        }
        else if (teamTag.Equals(TagList.ENEMY_UNIT) && typeTag.Equals(TagList.RANGED_UNIT))
        {
            SpriteRenderer enemyRangedImage = unit.GetComponent<SpriteRenderer>();
            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (direction.x > 0)
                {
                    enemyRangedImage.sprite = enemyRangedFacingRight;
                }
                else
                {
                    enemyRangedImage.sprite = enemyRangedFacingLeft;
                }
            }
            else
            {
                if (direction.y > 0)
                {
                    enemyRangedImage.sprite = enemyRangedFacingUp;
                }
                else
                {
                    enemyRangedImage.sprite = enemyRangedFacingDown;
                }
            }
        }
        else
        {
            print("Un-identifiable unit");
        }
    }
}
