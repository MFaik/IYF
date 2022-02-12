using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] Transform wallParent;
    GameObject m_currentGrid;
    GridManager.GridWall m_currentGridWall;

    GameObject[,] grids;

    void Start() {
        grids = new GameObject[99, 99];
    }

    public void SpawnGrid(Vector2 position) {
        m_currentGridWall = new GridManager.GridWall(position, 1);
        if (grids[m_currentGridWall.Row, m_currentGridWall.Collumn] == null){
            m_currentGrid = Instantiate(grid, Vector3.zero, Quaternion.identity);
            m_currentGrid.transform.parent = wallParent;
            grids[m_currentGridWall.Row, m_currentGridWall.Collumn] = m_currentGrid;
            Vector2 newPos = Vector2.zero;
            FindPlace(ref newPos);
            m_currentGrid.transform.position = newPos;
        } else {
            Debug.Log("Dolu");
        }
    }

    public void RemoveGrid(Vector2 position) {
        int collumn = (int)((5f - position.y) / 1);
        int row = (int)((position.x + 8f) / 1);

        if (grids[row, collumn] != null){
            Destroy(grids[row, collumn]);
        } else{
            Debug.Log("Zaten boş");
        }
    }

    void FindPlace(ref Vector2 position) {
        float startX = -7.5f;
        float startY = 4.5f;
        float endX = 7.5f;
        float endY = -4.5f;

        position.x = startX + m_currentGridWall.Row;
        position.y = startY - m_currentGridWall.Collumn;
    }
}
