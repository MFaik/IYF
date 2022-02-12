using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    [SerializeField] Transform agentParent;

    int m_currentIndex = 0;
    NavMeshAgent m_agent;
    Animator m_animator;
    [SerializeField] List<Transform> m_agents;
    // Start is called before the first frame update
    void Start(){
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");

        agentParent = GameObject.FindGameObjectWithTag("agentParent").transform;
        m_animator = GetComponentInChildren<Animator>();
        m_agent = GetComponent<NavMeshAgent>();
        m_agent.updateRotation = false;
        m_agent.updateUpAxis = false;
        UpdateAgents();
    }

    // Update is called once per frame
    void Update(){
        m_animator.SetBool("Stopped", m_agent.isStopped);

        if (Input.GetKeyDown(KeyCode.L))
            UpdateAgents();

        if(m_agent.pathStatus == NavMeshPathStatus.PathPartial){
            m_currentIndex++;
            if(m_currentIndex > m_agents.Count - 1)
               m_currentIndex = 0;
            m_agent.isStopped = true;
        }
        else{
            m_agent.isStopped = false;
        }
       
        m_agent.SetDestination(m_agents[m_currentIndex].position);

        if (m_agent.remainingDistance < 1 && m_agent.remainingDistance > 0 && m_agent.pathStatus == NavMeshPathStatus.PathComplete)
        {
            Time.timeScale = 0;
            Debug.Log(m_agent.remainingDistance);
        }
    }

    void UpdateAgents(){
        m_agents.Clear();
        foreach (Transform t in agentParent)
            if (!ReferenceEquals(t, transform)) m_agents.Add(t);
        m_currentIndex = 0;
    }

}
