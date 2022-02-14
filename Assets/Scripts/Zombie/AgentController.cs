using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public float Speed = 1f;

    [SerializeField] float ActionTime = 3f; 
    float m_timer = 0;
    int m_wanderingCount = 0;
    float m_cryingTime = 4f;
    bool m_crying = false;

    Transform m_target;
    Animator m_animator;
    CircleCollider2D m_collider;
    Rigidbody2D m_rb;
    [SerializeField] List<Transform> m_agents;

    [SerializeField] GameObject[] Walls;
    [SerializeField] float Acceleration;
    [SerializeField] float MaxSpeed;
    
    [SerializeField] GameObject EndAnimation;

    // Start is called before the first frame update
    void Start(){
        m_animator = GetComponentInChildren<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer > 0){
            m_timer -= Time.deltaTime;
            return;
        }
        m_timer = ActionTime;

        if (m_crying){
            m_animator.SetTrigger("StopCrying");
            return;
        }

        if (m_target == null)
            m_target = ScanForTarget();

        if (CheckPath(m_target)) {
            m_rb.velocity = ((m_target.position - transform.position).normalized * Speed);
        } else {
            m_target = null;

            if(m_wanderingCount == 6){
                m_crying = true;
                m_animator.SetTrigger("StartCrying");
                m_timer = m_cryingTime;
                m_wanderingCount = 0;
            } else if (m_rb.velocity.magnitude < MaxSpeed){
                m_wanderingCount++;

                m_rb.velocity = GetRandomVector3().normalized * Acceleration;
            }
                
        }
    }

    public void UpdateAgents(List<Transform> t){
        foreach (Transform agent in t)
            if(!ReferenceEquals(agent, transform))  m_agents.Add(agent);
    }

    Transform ScanForTarget(){
        List<RaycastHit2D> raycasts = new List<RaycastHit2D>();
        foreach (Transform agent in m_agents){
            RaycastHit2D temp = Physics2D.Raycast(transform.position + (agent.position - transform.position).normalized * m_collider.bounds.size.magnitude/2f, agent.position - transform.position);
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

    Vector3 GetRandomVector3() {
        return Quaternion.Euler(0, 0, Random.Range(0, 360)) * new Vector2(1, 0);
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("Agent")){
            if(AgentManager.Instance.IsFinished) return;
            Instantiate(EndAnimation, transform.position, Quaternion.identity);
            AgentManager.Instance.IsFinished = true;
        }
    }
}
