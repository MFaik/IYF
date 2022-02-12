using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWall : MonoBehaviour
{
    [SerializeField] GameObject  WallPrefab;
    [SerializeField] int MaxSize;
    [SerializeField] Transform WallPool;
    
    GameObject m_currentWall;

    LineRenderer m_lineRenderer;
    EdgeCollider2D m_edgeCollider;
    List<Vector2> m_mousePositions;
    Camera m_camera;

    void Start() {
        m_camera = Camera.main;
        m_mousePositions = new List<Vector2>();
    }

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
        
        if(WallPool)
            m_currentWall.GetComponent<Transform>().SetParent(WallPool);

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
    
        if(m_lineRenderer.positionCount >= MaxSize){
            m_mousePositions.RemoveAt(0);
            m_edgeCollider.points = m_mousePositions.ToArray();
            m_lineRenderer.positionCount--;
            m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, newMousePos);
            
            //nub unity can't convert vector2 array to vector3 array
            Vector3[] wtfUnity = new Vector3[m_mousePositions.Count];
            for(int i = 0; i < m_mousePositions.Count; i++){
                wtfUnity[i] = m_mousePositions[i];
            }

            m_lineRenderer.SetPositions(wtfUnity);
        }
    }
}
