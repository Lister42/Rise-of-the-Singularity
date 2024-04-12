using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    private Transform _transform;
    private Camera _camera;
    public RawImage minimapImage;

    #region Camera Moving Variables
    private float height = 0.0F;
    private float width = 0.0F;
    private bool isCamMovingX = false;
    private bool isCamMovingY = false;
    private float camMoveSpeed = 15.0F;
    private float offset = 5.0F;
    private float sizeOfMap = 0.0F;
    #endregion

    #region Camera Zooming Variables
    private float _defaultSize = 6.0F;
    private float minSize = 4.0F;
    private float maxSize = 10.0F;
    private float zoomRate = 1.0F;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        _camera = GetComponent<Camera>();
        _camera.orthographicSize = _defaultSize;

        height = _camera.orthographicSize * 2;
        width = height * _camera.aspect;

        sizeOfMap = GetSizeForSpecificMap();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraDimensions();
        CameraMovement();
        CameraZoom();
    }

    private float GetSizeForSpecificMap()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Equals(SceneNameList.FREEPLAY_SCENE))
        {
            UpdateMiniMapDimensions(ScreenSizeList.FREEPLAY_MINIMAP_SIZE);
            return ScreenSizeList.FREEPLAY_MAP_SIZE;
        }
        else if (sceneName.Equals(SceneNameList.TUTORIAL1_SCENE))
        {
            UpdateMiniMapDimensions(ScreenSizeList.TUTORIAL1_MINIMAP_SIZE);
            return ScreenSizeList.TUTORIAL1_MAP_SIZE;
        }
        else if (sceneName.Equals(SceneNameList.TUTORIAL2_SCENE))
        {
            UpdateMiniMapDimensions(ScreenSizeList.TUTORIAL2_MINIMAP_SIZE);
            return ScreenSizeList.TUTORIAL2_MAP_SIZE;
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO1_SCENE))
        {
            UpdateMiniMapDimensions(ScreenSizeList.SCENARIO1_MINIMAP_SIZE);
            return ScreenSizeList.SCENARIO1_MAP_SIZE;
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO2_SCENE))
        {
            UpdateMiniMapDimensions(ScreenSizeList.SCENARIO2_MINIMAP_SIZE);
            return ScreenSizeList.SCENARIO2_MAP_SIZE;
        }
        else if (sceneName.Equals(SceneNameList.SCENARIO3_SCENE))
        {
            UpdateMiniMapDimensions(ScreenSizeList.SCENARIO3_MINIMAP_SIZE);
            return ScreenSizeList.SCENARIO3_MAP_SIZE;
        }
        UpdateMiniMapDimensions(ScreenSizeList.DEFAULT_MINIMAP_SIZE);
        return ScreenSizeList.DEFAULT_MAP_SIZE;
    }

    private void UpdateMiniMapDimensions(float size)
    {
        minimapImage.rectTransform.sizeDelta = new Vector2(size, size);
    }

    private void UpdateCameraDimensions()
    {
        height = _camera.orthographicSize * 2;
        width = height * _camera.aspect;
    }

    private void CameraMovement()
    {
        Vector3 newCamPosition = transform.position;

        if (Input.mousePosition.x >= Screen.width - offset && newCamPosition.x < sizeOfMap / 2)
        {
            isCamMovingX = true;
            newCamPosition.x += camMoveSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.x <= offset && newCamPosition.x > -1 * sizeOfMap / 2)
        {
            isCamMovingX = true;
            newCamPosition.x -= camMoveSpeed * Time.deltaTime;
        }
        else
        {
            isCamMovingX = false;
        }

        if (Input.mousePosition.y > Screen.height - offset && newCamPosition.y < sizeOfMap / 2)
        {
            isCamMovingY = true;
            newCamPosition.y += camMoveSpeed * Time.deltaTime;
        }
        else if (Input.mousePosition.y < offset && newCamPosition.y > -1 * sizeOfMap / 2)
        {
            isCamMovingY = true;
            newCamPosition.y -= camMoveSpeed * Time.deltaTime;
        }
        else
        {
            isCamMovingY = false;
        }

        _transform.position = newCamPosition;
    }

    private void CameraZoom()
    {
        // Mouse wheel moving forward
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && _camera.orthographicSize > minSize)
        {
            _camera.orthographicSize -= zoomRate;
        }

        // Mouse wheel moving backward
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && _camera.orthographicSize < maxSize)
        {
            _camera.orthographicSize += zoomRate;
        }
    }

    public bool CameraIsMoving()
    {
        return isCamMovingX || isCamMovingY;
    }

    public bool GetCameraMovingX()
    {
        return isCamMovingX;
    }

    public bool GetCameraMovingY()
    {
        return isCamMovingY;
    }

    public bool GetZoomedOut()
    {
        if (_camera.orthographicSize == maxSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetZoomedIn()
    {
        if (_camera.orthographicSize == minSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public float GetSizeOfMap()
    {
        return sizeOfMap;
    }
}
