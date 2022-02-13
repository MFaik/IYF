using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public float Speed = 1f;

    [SerializeField] float ActionTime = 3f; 
    float m_timer = 0;

    Transform m_target;
    Animator m_animator;
    CircleCollider2D m_collider;
    Rigidbody2D m_rb;
    [SerializeField] List<Transform> m_agents;

    [SerializeField] GameObject[] Walls;
    [SerializeField] float Acceleration;
    [SerializeField] float WanderingSpeed;
    [SerializeField] float StartWaitTime;
    [SerializeField] GameObject MoveStop;
    [SerializeField] float MinX, MinY, MaxX, MaxY;
    float m_waitTime;
    public GameObject m_moveStop;
    public bool is_wandering = false;

    // Start is called before the first frame update
    void Start(){
        m_animator = GetComponentInChildren<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
        m_moveStop = Instantiate(MoveStop, Vector3.zero, Quaternion.identity);
        UpdateMoveStop();
    }

    // Update is called once per frame
    void Update(){
        if (is_wandering){
            if (m_rb.velocity.magnitude < WanderingSpeed)
                m_rb.AddForce((m_moveStop.transform.position - transform.position) * Acceleration * Time.deltaTime);

            if (Vector2.Distance(transform.position, m_moveStop.transform.position) < 1f){
                if(m_waitTime <= 0){
                    m_rb.velocity = Vector2.zero;
                    UpdateMoveStop();
                } else{
                    m_waitTime -= Time.deltaTime;
                }
            }
        }

        if(m_timer > 0){
            m_timer -= Time.deltaTime;
            return;
        }
        m_timer = ActionTime;

        if(m_target == null)
            m_target = ScanForTarget();

        if (CheckPath(m_target)){
            m_animator.SetBool("Stopped", false);
            m_animator.SetTrigger("StopCrying");
            m_rb.velocity = ((m_target.position - transform.position).normalized * Speed);
            is_wandering = false;
        } else {
            m_target = null;
            is_wandering = true;
        }
    }

    public void UpdateAgents(List<Transform> t){
        foreach (Transform agent in t)
            if(!ReferenceEquals(agent, transform))  m_agents.Add(agent);
    }

    Transform ScanForTarget(){
        List<RaycastHit2D> raycasts = new List<RaycastHit2D>();
        foreach (Transform agent in m_agents){
            RaycastHit2D temp = Physics2D.Raycast(transform.position + (agent.position - transform.position).normalized * m_collider.bounds.size.magnitude, agent.position - transform.position);
            Debug.DrawRay(transform.position + (agent.position - transform.position).normalized * m_collider.bounds.size.magnitude, agent.position - transform.position,Color.red,1f);
            if(temp && temp.collider.gameObject.CompareTag("Agent"))
                raycasts.Add(temp);
        }

        if (raycasts.Count == 0) return null;

        raycasts.Sort(delegate (RaycastHit2D t1, RaycastHit2D t2) { return t1.distance.CompareTo(t2.distance); });

        Debug.Log(transform.name + " Hit " + raycasts[0].collider.transform.name);
        return raycasts[0].collider.transform;
    }

    bool CheckPath(Transform target){
        if(target == null) return false;

        RaycastHit2D temp = Physics2D.Raycast(transform.position + (target.position - transform.position).normalized * m_collider.bounds.size.magnitude, target.position - transform.position);
        return temp && temp.transform.CompareTag("Agent");
    }

    public void UpdateMoveStop() {
        m_moveStop.transform.position = new Vector2(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY));
        m_waitTime = StartWaitTime;
    }
}
