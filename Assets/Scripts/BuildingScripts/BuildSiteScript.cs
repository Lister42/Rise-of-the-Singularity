using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildSiteScript : MonoBehaviour
{
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private Build_Manager _buildManger;
    private SpriteRenderer _spriteRenderer;
    private Setup_Manager _setupManager;
    private bool clicked = false;
    public GameObject _healthBar;

    private GameObject typePrefab;
    private Transform _transform;

    #region Build Variables
    private GameObject buildingParent;
    private float totalBuildAmount = 100.0F;
    private float currentBuildAmount = 0.0F;
    private float addAmount = 10.0F;
    private float time = 0.0F;
    private float nextTime = 1.0F;
    private float timeIncrement = 1.0F;
    private int numWorkers = 0;
    #endregion

    #region Slider Variables
    private GameObject buildSitePanel;
    private GameObject secondaryPanel;
    private Sprite buildSiteSprite;
    private Slider buildSiteSlider;
    private Text buildSiteSliderText;
    private Image buildSiteImage;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _buildManger = Build_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _setupManager = Setup_Manager._Instance;
        _transform = transform.parent;
        SetSortingLayerOrder();
        LoadVariables();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        BuildSiteSlider();
        CheckBuild();
        //CheckMultiple();
    }

    private void LoadVariables()
    {
        buildSitePanel = _setupManager.GetBuildSitePanel();
        secondaryPanel = _setupManager.GetSecondaryPanel();
        buildSiteSlider = _setupManager.GetBuildSiteSlider();
        buildSiteSliderText = _setupManager.GetBuildSiteSliderText();
        buildSiteImage = _setupManager.GetBuildSiteImage();
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void CheckBuild()
    {
        if (nextTime <= Time.time && numWorkers > 0)
        //if (nextTime <= time)
        {
            nextTime = Time.time + timeIncrement;
            //currentBuildAmount += addAmount;
            currentBuildAmount += addAmount * numWorkers;

            if (currentBuildAmount >= totalBuildAmount)
            {
                DoneBuilding();
            }
        }
    }
    
    private void DoneBuilding()
    {
        if (typePrefab)
        {
            GameObject building = Instantiate(typePrefab, transform.parent.transform.position, Quaternion.identity);
            building.transform.parent = buildingParent.transform;
            building.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = SortingLayerList.BUILDING;
            building.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            if (clicked)
            {
                buildSitePanel.SetActive(false);
                secondaryPanel.SetActive(false);
            }
            Destroy(transform.parent.gameObject);
        }
    }

    public void SetTypePrefab(GameObject prefab)
    {
        typePrefab = prefab;
        float hbOffset_x = 0.5F;
        float hbOffset_y = 1.0F;
        float hbOffsetScale_x = 1.0F;

        if (prefab.tag == TagList.STATIC_COLLECTOR)
        {
            transform.parent.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(.5F, 0);
            transform.parent.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 1);
            _healthBar.transform.localPosition = new Vector3(_healthBar.transform.localPosition.x + hbOffset_x, _healthBar.transform.localPosition.y, 0);
            _healthBar.transform.localScale = new Vector3(_healthBar.transform.localScale.x + hbOffsetScale_x, _healthBar.transform.localScale.y, 0);
        }
        else if (prefab.tag == TagList.FACTORY)
        {
            transform.parent.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(.5F, .5F);
            transform.parent.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(2, 2);
            _healthBar.transform.localPosition = new Vector3(_healthBar.transform.localPosition.x + hbOffset_x, _healthBar.transform.localPosition.y + hbOffset_y, 0);
            _healthBar.transform.localScale = new Vector3(_healthBar.transform.localScale.x + hbOffsetScale_x, _healthBar.transform.localScale.y, 0);
        }

        buildSiteSprite = prefab.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public GameObject GetTypePrefab()
    {
        return typePrefab;
    }

    public void SetBuildingParent(GameObject parent)
    {
        buildingParent = parent;
    }

    public float GetCurrentBuildAmount()
    {
        return currentBuildAmount;
    }

    public Sprite GetBuildSprite()
    {
        return typePrefab.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void AddWorker()
    {
        numWorkers++;
    }

    public void RemoveWorker()
    {
        if (numWorkers > 0)
        {
            numWorkers--;
        }
    }

    public void SetClicked(bool b)
    {
        clicked = b;
    }

    private void BuildSiteSlider()
    {
        if (clicked)
        {
            buildSiteSlider.value = currentBuildAmount;
            buildSiteSliderText.text = currentBuildAmount.ToString() + "%";
            FixAndSetBuildPanelSprite();
        }
    }

    private void FixAndSetBuildPanelSprite()
    {
        string tag = typePrefab.tag;

        if (tag.Equals(TagList.OUTPOST))
        {
            buildSiteImage.rectTransform.sizeDelta = new Vector2(20, 32);
        }
        else if (tag.Equals(TagList.MINE))
        {
            buildSiteImage.rectTransform.sizeDelta = new Vector2(20, 25);
        }
        else if (tag.Equals(TagList.STATIC_COLLECTOR))
        {
            buildSiteImage.rectTransform.sizeDelta = new Vector2(40, 20);
        }
        else if (tag.Equals(TagList.FACTORY))
        {
            buildSiteImage.rectTransform.sizeDelta = new Vector2(20, 30);
        }
        else if (tag.Equals(TagList.TURRET))
        {
            buildSiteImage.rectTransform.sizeDelta = new Vector2(10, 10);
        }
        buildSiteImage.sprite = buildSiteSprite;
    }

    private void CheckMultiple()
    {
        Vector2 position = _transform.position;
        List<GameObject> buildingList = _buildManger.GetBuildingList();

        foreach(GameObject building in buildingList)
        {
            if (building.tag.Equals(TagList.OUTPOST))
            {
                if (building.transform.position.Equals(position))
                {
                    Destroy(transform.parent.gameObject);
                    break;
                }
            }
        }
    }
}
