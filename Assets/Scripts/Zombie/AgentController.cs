using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public float Speed = 1f;

    Transform m_target;
    Animator m_animator;
    CapsuleCollider2D m_collider;
    Rigidbody2D m_rb;
    [SerializeField] List<Transform> m_agents;

    // Start is called before the first frame update
    void Start(){
        m_animator = GetComponent<Animator>();
        m_rb = GetComponent<Rigidbody2D>();
        m_collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update(){
        if (!CheckPath(m_target)){
            m_target = ScanForTarget();
            if(m_target == null) { } //wandering
            m_rb.velocity = Vector2.zero;

            m_animator.SetBool("Stopped", true);
        }
        else{
            m_animator.SetBool("Stopped", false);
            m_rb.velocity = ((m_target.position - transform.position).normalized * Speed);
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

        return temp && temp.collider.CompareTag("Agent");
    }

}
