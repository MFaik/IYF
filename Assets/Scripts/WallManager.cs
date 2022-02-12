using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    Camera m_camera;

    [SerializeField] float GridSize;
    
    [SerializeField] GameObject grid;
    [SerializeField] Transform wallParent;
    GameObject m_currentGrid;
    Wall m_currentGridWall;

    GameObject[,] grids;
    Queue<Vector2> m_wallPositions = new Queue<Vector2>();

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

            float startX = -7.5f;
            float startY = 4.5f;

            position.x = startX + (this.Row * gridSize);
            position.y = startY - (this.Collumn * gridSize);
            
            return position;
        }
    }

    void Start() {
        m_camera = Camera.main;
        grids = new GameObject[99, 99];
    }

    void Update() {
        if (Input.GetMouseButton(0)){
            SpawnWall(m_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    public void SpawnWall(Vector2 position) {
        m_currentGridWall = new Wall(position, GridSize);

        if (grids[m_currentGridWall.Row, m_currentGridWall.Collumn] == null){
            m_currentGrid = Instantiate(grid, Vector3.zero, Quaternion.identity);
            
            m_currentGrid.transform.parent = wallParent;
            grids[m_currentGridWall.Row, m_currentGridWall.Collumn] = m_currentGrid;

            m_wallPositions.Enqueue(m_currentGridWall.GetRealPosition());
            m_currentGrid.transform.position = m_currentGridWall.GetRealPosition();
        } else {
            Debug.Log("Dolu");
        }
    }

    public void RemoveWall(Vector2 position) {
        int collumn = (int)((5f - position.y) / GridSize);
        int row = (int)((position.x + 8f) / GridSize);

        if (grids[row, collumn] != null){
            Destroy(grids[row, collumn]);
        } else{
            Debug.Log("Zaten boş");
        }
    }
}
