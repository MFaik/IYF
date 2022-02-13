using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChecker : MonoBehaviour
{
    int currentAgentCount = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent"))  currentAgentCount++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent")) currentAgentCount--;
    }

    public bool CheckForAgent(){
        return currentAgentCount > 0;
    }
}
