using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] WallManager WManager;
    Camera m_camera;

    public class GridWall
    {
        public float X { get; set; }
        public float Y { get; set; }
        public int Collumn { get; set; }
        public int Row { get; set; }

        public GridWall(float x, float y, int gridSize) {
            X = x;
            Y = y;
                  
            Collumn = (int)((5f - Y) / gridSize);
            Row = (int)((X + 8f) / gridSize);
        }

        public GridWall(Vector2 pos, int gridSize) {
            X = pos.x;
            Y = pos.y;
                  
            Collumn = (int)((5f - Y) / gridSize);
            Row = (int)((X + 8f) / gridSize);
        }
    }

    void Start() {
        m_camera = Camera.main;
    }

    void Update() {
        if (Input.GetMouseButton(0)){
            WManager.SpawnGrid(m_camera.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetMouseButton(1)){
            WManager.RemoveGrid(m_camera.ScreenToWorldPoint(Input.mousePosition));
        }
    }
}
