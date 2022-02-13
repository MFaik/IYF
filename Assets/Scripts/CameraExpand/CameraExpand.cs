using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraExpand : MonoBehaviour
{
    [SerializeField] float CamGrowth = 0.1f;
    [SerializeField] WallExpand wallExpand;

    Camera m_camera;
    int m_lastOrthographicSize;
    void Start() {
        m_camera = GetComponent<Camera>();
        m_lastOrthographicSize = (int)m_camera.orthographicSize;
        wallExpand.UpdateSize((m_lastOrthographicSize*2-2)*16f/9f,m_lastOrthographicSize*2-2,false);
    }   

    void Update() {
        m_camera.orthographicSize += CamGrowth * Time.deltaTime;
        if(m_lastOrthographicSize != (int)m_camera.orthographicSize){
            m_lastOrthographicSize = (int)m_camera.orthographicSize;
            wallExpand.UpdateSize((m_lastOrthographicSize*2-2)*16f/9f,m_lastOrthographicSize*2-2);
        }
    }
}
