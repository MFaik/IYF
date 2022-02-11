using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWall : MonoBehaviour
{
    [SerializeField] GameObject  WallPrefab;
    [SerializeField] GameObject MainCamera;
    GameObject m_currentWall;

    LineRenderer m_lineRenderer;
    EdgeCollider2D m_edgeCollider;
    List<Vector2> m_mousePositions;
    Camera m_camera;

    // Start is called before the first frame update
    void Start() {
        m_camera = MainCamera.GetComponent<Camera>();
        m_mousePositions = new List<Vector2>();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetMouseButtonDown(0))
            SpawnWall();
        
        if(Input.GetMouseButton(0)){
            Vector2 tempMousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            
            if(Vector2.Distance(tempMousePos, m_mousePositions[m_mousePositions.Count - 1]) > .1f)
                UpdateWall(tempMousePos);
        }
    }

    void SpawnWall() {
        m_currentWall = Instantiate(WallPrefab, Vector3.zero, Quaternion.identity);

        m_lineRenderer = m_currentWall.GetComponent<LineRenderer>();
        m_edgeCollider = m_currentWall.GetComponent<EdgeCollider2D>();

        m_mousePositions.Clear();
        m_mousePositions.Add(m_camera.ScreenToWorldPoint(Input.mousePosition));
        m_mousePositions.Add(m_camera.ScreenToWorldPoint(Input.mousePosition));

        m_lineRenderer.SetPosition(0, m_mousePositions[0]);
        m_lineRenderer.SetPosition(1, m_mousePositions[1]);

        m_edgeCollider.points = m_mousePositions.ToArray();
    }

    void UpdateWall(Vector2 newMousePos) {
        m_mousePositions.Add(newMousePos);
        m_lineRenderer.positionCount++;
        m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, newMousePos);
        m_edgeCollider.points = m_mousePositions.ToArray();
    }
}
