using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreScript : MonoBehaviour
{
    private Setup_Manager _setupManager;
    private Build_Manager _buildManager;
    private Resource_Manager _resourceManager;
    private Pause_Manager _pauseManger;
    private SortingLayerOrder_Manager _sortingLayerOrder_Manager;
    private SpriteRenderer _spriteRenderer;
    private bool clicked = false;
    private List<Vector2> spotList;

    #region Slider and Creation Variables
    public GameObject _workerPrefab;
    private List<GameObject> createList;
    private int maxListSize = 5;
    private List<Image> imageList;
    private Slider coreSlider;
    private Text coreSliderText;
    private Image coreImage1;
    private Image coreImage2;
    private Image coreImage3;
    private Image coreImage4;
    private Image coreImage5;
    private int amountCreation = 0;
    private int maxAmountCreation = 100;
    private int creationChangeAmount = 0;
    private float nextTime = 1.0F;
    private float timeIncrement = 1.0F;
    private bool startingUp = true;
    private List<Vector2> creationSpots;
    private GameObject unitParent;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _setupManager = Setup_Manager._Instance;
        _buildManager = Build_Manager._Instance;
        _resourceManager = Resource_Manager._Instance;
        _sortingLayerOrder_Manager = SortingLayerOrder_Manager._Instance;
        _pauseManger = Pause_Manager._Instance;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        imageList = new List<Image>();
        createList = new List<GameObject>();
        creationSpots = new List<Vector2>();

        SetSortingLayerOrder();

        LoadVariables();
        LoadLists();
    }

    // Update is called once per frame
    void Update()
    {
        CheckCreation();
        CheckSliderAmountCreation();
        DisplayUI();
    }

    private void LoadVariables()
    {
        coreSlider = _setupManager.GetCoreSlider();
        coreSliderText = _setupManager.GetCoreSliderText();
        coreImage1 = _setupManager.GetCoreImage1();
        coreImage2 = _setupManager.GetCoreImage2();
        coreImage3 = _setupManager.GetCoreImage3();
        coreImage4 = _setupManager.GetCoreImage4();
        coreImage5 = _setupManager.GetCoreImage5();
        unitParent = _setupManager.GetUnitParent();
    }

    private void SetSortingLayerOrder()
    {
        _spriteRenderer.sortingOrder = _sortingLayerOrder_Manager.ComputeSortingLayer(transform.parent.transform.position.y);
    }

    private void LoadLists()
    {
        // load image list
        imageList.Add(coreImage1);
        imageList.Add(coreImage2);
        imageList.Add(coreImage3);
        imageList.Add(coreImage4);
        imageList.Add(coreImage5);

        // load creation spot list
        Vector2 currentPos = gameObject.transform.parent.transform.position;
        creationSpots.Add(new Vector2(currentPos.x, currentPos.y - 1));
        creationSpots.Add(new Vector2(currentPos.x + 1, currentPos.y - 1));
        creationSpots.Add(new Vector2(currentPos.x + 2, currentPos.y - 1));
        creationSpots.Add(new Vector2(currentPos.x + 2, currentPos.y));
        creationSpots.Add(new Vector2(currentPos.x + 2, currentPos.y + 1));
        creationSpots.Add(new Vector2(currentPos.x + 2, currentPos.y + 2));
        creationSpots.Add(new Vector2(currentPos.x + 1, currentPos.y + 2));
        creationSpots.Add(new Vector2(currentPos.x, currentPos.y + 2));
        creationSpots.Add(new Vector2(currentPos.x - 1, currentPos.y + 2));
        creationSpots.Add(new Vector2(currentPos.x - 1, currentPos.y + 1));
        creationSpots.Add(new Vector2(currentPos.x - 1, currentPos.y));
        creationSpots.Add(new Vector2(currentPos.x - 1, currentPos.y - 1));
    }

    private void CheckCreation()
    {
        if (createList.Count == 0)
        {
            startingUp = true;
        }
        else if (createList.Count > 0)
        {
            if (startingUp)
            {
                nextTime = Time.time + timeIncrement;
                startingUp = false;
            }

            SetCurrentCreationChangeAmount();
            if (Time.time >= nextTime)
            {
                nextTime += timeIncrement;
                amountCreation += creationChangeAmount;

                if (amountCreation >= maxAmountCreation)
                {
                    CreateUnit();
                }
                if (amountCreation > maxAmountCreation)
                {
                    amountCreation = maxAmountCreation;
                }
            }
        }
    }

    private void SetCurrentCreationChangeAmount()
    {
        GameObject firstUnit = createList[0];
        string tag = firstUnit.tag;

        if (tag.Equals(TagList.WORKER))
        {
           creationChangeAmount = UnitCreationAmountList.WORKER_AMOUNT;
        }
    }

    private void CreateUnit()
    {
        if (_resourceManager.HaveAvailableMemory())
        {
            bool unitWasCreated = false;
            for (int i = 0; i < creationSpots.Count; i++)
            {
                Vector2 currentSpot = creationSpots[i];

                if (validSpot(currentSpot))
                {
                    GameObject createdUnit = Instantiate(createList[0], currentSpot, Quaternion.identity);
                    createdUnit.transform.parent = unitParent.transform;
                    createList.RemoveAt(0);
                    ResetAmountCreation();
                    unitWasCreated = true;
                    _buildManager.UpdateAllLists();
                    break;
                }
            }
            if (!unitWasCreated)
            {
                print("No valid spots to create unit");
            }
        }
        else 
        {
            print("Not enough memory to create unit");
        }
    }

    private bool validSpot(Vector2 placeSpot)
    {
        spotList = _buildManager.GetSpotList();
        foreach (Vector2 spot in spotList)
        {
            if (placeSpot.Equals(spot))
            {
                return false;
            }
        }

        return true;
    }

    private void DisplayUI()
    {
        if (clicked)
        {
            DisplayCreationList();
            UpdateSliderAndText();
        }
    }

    public void DisplayCreationList()
    {
        // remove all the sprites from the images
        foreach (Image i in imageList)
        {
            i.sprite = null;
        }

        for (int i = 0; i < createList.Count; i++)
        {
            FixAndSetImageSprite(imageList[i], createList[i]);
        }

        foreach (Image i in imageList)
        {
            if (!i.sprite)
            {
                i.gameObject.SetActive(false);
            }
        }
    }

    public void UpdateSliderAndText()
    {
        coreSlider.value = amountCreation;
        coreSliderText.text = amountCreation + "%";
    }

    private void FixAndSetImageSprite(Image unitImage, GameObject unit)
    {
        string tag = unit.tag;

        if (tag.Equals(TagList.WORKER))
        {
            unitImage.rectTransform.sizeDelta = new Vector2(10, 14);
        }

        unitImage.sprite = unit.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite;
        unitImage.transform.localPosition = new Vector3(0, 1, 0);
        unitImage.gameObject.SetActive(true);
    }

    public void AddUnit(string type)
    {
        if (createList.Count < maxListSize)
        {
            if (type.Equals(TagList.WORKER))
            {
                _resourceManager.SubtractResources(
                    ResourceCostList.WORKER_METAL_COST, 
                    ResourceCostList.WORKER_ELECTRICITY_COST, 
                    ResourceCostList.WORKER_PLATINUM_COST);
                createList.Add(_workerPrefab);
            }
            else
            {
                print("Invalid unit type given");
            }
        }
    }

    public void RemoveUnit(int index)
    {
        // check if create list has the unit the button is trying to remove
        if (createList.Count > index)
        {
            string type = createList[index].tag;
            if (type.Equals(TagList.WORKER))
            {
                _resourceManager.AddResources(
                    ResourceCostList.WORKER_METAL_COST,
                    ResourceCostList.WORKER_ELECTRICITY_COST,
                    ResourceCostList.WORKER_PLATINUM_COST);
                createList.RemoveAt(index);

                if (index == 0)
                {
                    ResetAmountCreation();
                }
            }
            else
            {
                print("Invalid unit type given");
            }
        }
        else
        {
            print("No unit to remove at index " + index);
        }
    }

    private void ResetAmountCreation()
    {
        amountCreation = 0;
    }

    private void CheckSliderAmountCreation()
    {
        if (createList.Count <= 0)
        {
            ResetAmountCreation();
        }
    }

    public void SetClicked(bool b)
    {
        clicked = b;
    }

}
