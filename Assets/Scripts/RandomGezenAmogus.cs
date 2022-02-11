using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGezenAmogus : MonoBehaviour
{
    [SerializeField] GameObject[] Walls;
    [SerializeField] float Speed;
    [SerializeField] float StartWaitTime;
    [SerializeField] Transform MoveStop;
    [SerializeField] float MinX, MinY, MaxX, MaxY;

    float m_waitTime;

    void Start() {
        m_waitTime = StartWaitTime;
    
        UpdateMoveStop();
    }

    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, MoveStop.position, Speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, MoveStop.position) < 0.2f){
            if(m_waitTime <= 0){
                UpdateMoveStop();
                m_waitTime = StartWaitTime;
            } else{
                m_waitTime -= Time.deltaTime;
            }
        }
    }

    void UpdateMoveStop() {
        MoveStop.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        Debug.Log(MoveStop.position);
    }
}
