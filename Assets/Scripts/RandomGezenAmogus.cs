using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGezenAmogus : MonoBehaviour
{
    [SerializeField] GameObject[] Walls;
    [SerializeField] float Acceleration;
    [SerializeField] float StartWaitTime;
    [SerializeField] Transform MoveStop;
    [SerializeField] float MinX, MinY, MaxX, MaxY;

    Rigidbody2D m_rigidbody;

    float m_waitTime;

    void Start() {
        m_rigidbody = GetComponent<Rigidbody2D>();
        UpdateMoveStop();
    }

    void Update() {
        m_rigidbody.AddForce((MoveStop.position - transform.position) * Acceleration * Time.deltaTime);

        if (Vector2.Distance(transform.position, MoveStop.position) < 0.3f){
            if(m_waitTime <= 0){
                ResetVelocity();
                UpdateMoveStop();
            } else{
                m_waitTime -= Time.deltaTime;
            }
        }
    }

    public void UpdateMoveStop() {
        MoveStop.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        m_waitTime = StartWaitTime;
    }

    public void ResetVelocity() {
        m_rigidbody.velocity = Vector2.zero;
    }
}
