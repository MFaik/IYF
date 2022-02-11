using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        NavMeshHit closestHit;

        if (NavMesh.SamplePosition(gameObject.transform.position, out closestHit, 500f, NavMesh.AllAreas))
            gameObject.transform.position = closestHit.position;
        else
            Debug.LogError("Could not find position on NavMesh!");

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(target.position);
        Debug.Log("status " + agent.pathStatus);
        Debug.Log("stale " + agent.isPathStale);
        Debug.Log("stopped " + agent.isStopped);
        Debug.Log("haspath " + agent.hasPath);
    }
}
