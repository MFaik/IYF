using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawWall : MonoBehaviour
{
    [SerializeField] GameObject  WallPrefab;
    [SerializeField] int MaxSize;
    [SerializeField] float MaxPullSize;
    [SerializeField] Transform WallPool;
    [SerializeField] GameObject BoxColliderPrefab;
    
    GameObject m_currentWall;

    LineRenderer m_lineRenderer;
    EdgeCollider2D m_edgeCollider;
    List<Vector2> m_mousePositions;
    Camera m_camera;
    Vector2 m_colliderCounter = Vector2.zero;
    Vector2 m_colliderEnd = Vector2.zero;

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
            m_currentWall.transform.SetParent(WallPool);

        m_lineRenderer = m_currentWall.GetComponent<LineRenderer>();
        m_edgeCollider = m_currentWall.GetComponent<EdgeCollider2D>();

        m_mousePositions.Clear();
        m_mousePositions.Add(m_camera.ScreenToWorldPoint(Input.mousePosition));
        m_mousePositions.Add(m_camera.ScreenToWorldPoint(Input.mousePosition));

        m_colliderEnd = m_camera.ScreenToWorldPoint(Input.mousePosition);
        m_colliderCounter = Vector2.zero;

        m_lineRenderer.SetPosition(0, m_mousePositions[0]);
        m_lineRenderer.SetPosition(1, m_mousePositions[1]);

        m_edgeCollider.points = m_mousePositions.ToArray();
    }

    void SpawnColliders() {
        GameObject newCollider = Instantiate(BoxColliderPrefab, Vector3.zero, Quaternion.identity);
        newCollider.transform.SetParent(m_currentWall.transform);
        BoxCollider2D collider = newCollider.GetComponent<BoxCollider2D>();
        newCollider.transform.position = m_colliderEnd;
        collider.size = new Vector2(1f, 1f);
    }

    void UpdateWall(Vector2 newMousePos) {
        Vector2 fromLast = newMousePos - m_mousePositions[m_mousePositions.Count - 1];
        
        m_colliderEnd += fromLast;

        fromLast.x = Mathf.Abs(fromLast.x);
        fromLast.y = Mathf.Abs(fromLast.y);
        
        if (m_colliderCounter.x > 0.5 || m_colliderCounter.y > 0.5){
            SpawnColliders();
            m_colliderCounter = Vector2.zero;
        } else {
            m_colliderCounter += fromLast;
        }

        //cok hizli cekiyorsak line gg
        if (!(fromLast.x > MaxPullSize || fromLast.y > MaxPullSize)){
            m_mousePositions.Add(newMousePos);
            m_lineRenderer.positionCount++;
            m_lineRenderer.SetPosition(m_lineRenderer.positionCount - 1, newMousePos);
            m_edgeCollider.points = m_mousePositions.ToArray();
        }
    
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
