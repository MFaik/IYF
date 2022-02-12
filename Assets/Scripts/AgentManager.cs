using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    private AgentManager() { }
    private static AgentManager instance = null;
    public static AgentManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AgentManager();
            }
            return instance;
        }
    }

    void Start()
    {
        
    }

    public void SpawnNewAgent(){

    }
}
