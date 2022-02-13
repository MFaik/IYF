using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] List<Transform> m_agents;
    [SerializeField] GameObject m_spawnerPrefab;
    [SerializeField] Transform m_agentParent;
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
        foreach (Transform t in m_agentParent)
            m_agents.Add(t);

        foreach(Transform t in m_agents){
            t.gameObject.GetComponent<AgentController>().UpdateAgents(m_agents);
        }
    }
    public void SpawnNewAgent(float hx, float hy){
        Vector2 pos;
        do {
            pos = new Vector2(Random.Range(hx, -hx), Random.Range(hy, -hy));
        } while (WallManager.Instance.CheckForWall(pos));
        

        GameObject agent = Instantiate(m_spawnerPrefab, pos, Quaternion.identity);
    }

    public void OnAgentSpawn(Transform spawner, Transform agent)
    {
        agent.parent = m_agentParent;
        agent.tag = "Agent";
        agent.GetComponent<AgentController>().enabled = true;
        agent.GetComponent<CircleCollider2D>().enabled = true;
        Destroy(spawner.gameObject);
        UpdateAgents();
    }
}
