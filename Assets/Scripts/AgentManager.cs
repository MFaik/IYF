using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] List<Transform> m_agents;
    [SerializeField] Transform agentParent;
    private AgentManager() { }
    private static AgentManager instance = null;
    public static AgentManager Instance{
        get
        {
            if (instance == null)
                instance = FindObjectOfType(typeof(AgentManager)) as AgentManager;

            return instance;
        }
    }

    void Start(){
        UpdateAgents();
    }

    void UpdateAgents(){
        m_agents.Clear();
        foreach (Transform t in agentParent)
            m_agents.Add(t);

        foreach(Transform t in m_agents){
            t.gameObject.GetComponent<AgentController>().UpdateAgents(m_agents);
        }
    }
    public void SpawnNewAgent(){

    }
}
