using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSpawner : MonoBehaviour
{
    [SerializeField] Transform agent;
    void DoorComplete()
    {
        agent.gameObject.SetActive(true);
    }
    void SpawnComplete(){
        AgentManager.Instance.OnAgentSpawn(transform, agent);
    }
}
