using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    private WallManager() { }
    private static WallManager instance = null;
    public static WallManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(WallManager)) as WallManager;

            return instance;
        }
    }

    Camera m_camera;

    [SerializeField] float GridSize;

    [SerializeField] MouseChecker mouseChecker;
    [SerializeField] GameObject grid;
    [SerializeField] Transform wallParent;
    [SerializeField] int MaxWallNumber;

    GameObject m_currentGrid;
    Wall m_currentGridWall;
    int m_currentWallNumber = 0;

    Dictionary<Vector2,GameObject> m_allGrid = new Dictionary<Vector2, GameObject>();
    public List<GameObject> m_currentWalls;
    //Queue<Vector2> m_wallPositions = new Queue<Vector2>();

    public class Wall
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Collumn { get; set; }
        public int Row { get; set; }

        float gridSize;

        public Wall(float x, float y, float gridSize) {
            X = x;
            Y = y;

            Collumn = (int)((5f - Y) / gridSize);
            Row = (int)((X + 8f) / gridSize);

            this.gridSize = gridSize;
        }

        public Wall(Vector2 pos, float gridSize) {
            X = pos.x;
            Y = pos.y;

            Collumn = (int)((5f - Y) / gridSize);
            Row = (int)((X + 8f) / gridSize);

            this.gridSize = gridSize;
        }

        public Vector2 GetRealPosition() {
            Vector2 position = new Vector2();

            float startX = -8f + (gridSize / 2);
            float startY = 5f - (gridSize / 2);

            position.x = startX + (this.Row * gridSize);
            position.y = startY - (this.Collumn * gridSize);

            return position;
        }
    }

    void Start() {
        m_camera = Camera.main;
        m_currentWalls = new List<GameObject>();
    }

    void Update() {
        if (Input.GetMouseButton(0)){
            mouseChecker.transform.position = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if(!mouseChecker.CheckForAgent())
                SpawnWall(m_camera.ScreenToWorldPoint(Input.mousePosition));
        }
        /*if (Input.GetMouseButtonUp(0)){
            Reset();
        }
        */
    }

    void SpawnWall(Vector2 position) {
        if (m_currentWalls.Count >= MaxWallNumber){
            Destroy(m_currentWalls[0]);
            m_currentWalls.RemoveAt(0);
            
            m_currentGridWall = new Wall(position, GridSize);
            if (!m_allGrid.ContainsKey(new Vector2(m_currentGridWall.Row, m_currentGridWall.Collumn))){
                m_currentGrid = Instantiate(grid, Vector3.zero, Quaternion.identity);
                m_currentWalls.Add(m_currentGrid);

                m_currentGrid.transform.parent = wallParent;
                m_allGrid[new Vector2(m_currentGridWall.Row, m_currentGridWall.Collumn)] = m_currentGrid;

                m_currentGrid.transform.position = m_currentGridWall.GetRealPosition();
            }
        }
        else{
            m_currentGridWall = new Wall(position, GridSize);
            if (!m_allGrid.ContainsKey(new Vector2(m_currentGridWall.Row, m_currentGridWall.Collumn))){
                m_currentGrid = Instantiate(grid, Vector3.zero, Quaternion.identity);
                m_currentWalls.Add(m_currentGrid);
                Debug.Log(m_currentWalls[0]);
                m_currentGrid.transform.parent = wallParent;
                m_allGrid[new Vector2(m_currentGridWall.Row, m_currentGridWall.Collumn)] = m_currentGrid;

                m_currentGrid.transform.position = m_currentGridWall.GetRealPosition();
            }
        }
    }

    void RemoveWall(Vector2 position) {
        int collumn = (int)((5f - position.y) / GridSize);
        int row = (int)((position.x + 8f) / GridSize);

        RemoveWall(row, collumn);
    }

    void RemoveWall(int row, int collumn) {
        if (m_allGrid.ContainsKey(new Vector2(row, collumn))){
            Destroy(m_allGrid[new Vector2(row, collumn)]);
            m_allGrid.Remove(new Vector2(row, collumn));
        }
    }

    void Reset() {
        m_currentWallNumber = 0;
        m_currentWalls.Clear();
    }
}
