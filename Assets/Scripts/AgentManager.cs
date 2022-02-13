using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] List<Transform> m_agents;
    [SerializeField] CameraExpand CameraExpand;
    [SerializeField] GameObject m_spawnerPrefab;
    [SerializeField] Transform m_agentParent;
    private AgentManager() { }
    private static AgentManager instance = null;
    public bool IsFinished = false;
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

    public void EndGame(Transform a1, Transform a2){
        foreach (Transform t in m_agents){
            if(!ReferenceEquals(t,a1) && !ReferenceEquals(t, a2)){
                t.gameObject.GetComponent<AgentController>().enabled = false;
                t.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
                t.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                t.gameObject.GetComponentInChildren<Animator>().SetBool("Stopped", true);
            }
        }

        CameraExpand.enabled = false;
        a1.gameObject.GetComponentInChildren<Animator>().SetTrigger("End");
        a2.gameObject.GetComponentInChildren<Animator>().SetTrigger("End");
    }
}
