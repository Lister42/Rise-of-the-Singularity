using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding_Manager : MonoBehaviour
{
    #region Singleton Creation
    public static Pathfinding_Manager _Instance { get; private set; } = null;

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

    private float delay = 1.0F;
    private float nextGraphUpdate = 0.0F;
    private List<Vector2> points;
    private Vector2 size = new Vector2(5, 5);

    // Start is called before the first frame update
    void Start()
    {
        _buildManager = Build_Manager._Instance;

        points = new List<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckUpdateGraphPoints();
    }

    private void CheckUpdateGraphPoints()
    {
        if (nextGraphUpdate < Time.time)
        {
            nextGraphUpdate = Time.time + delay;
            UpdateGraphPoints();
        }
    }

    private void UpdateGraphPoints()
    {
        for (int i = points.Count-1; i >= 0; i--)
        {
            Vector2 point = points[i];

            // tell the Astar graph to update at this spot
            Bounds bounds = new Bounds(point, size);
            AstarPath.active.UpdateGraphs(bounds);

            points.RemoveAt(i);
        }
    }

    public void AddPoint(Vector2 point)
    {
        points.Add(point);
    }
}
