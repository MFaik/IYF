using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSorter : MonoBehaviour
{
    [SerializeField] int SortingOrderBase = 5000;
    [SerializeField] int Offset = 0;
    Renderer m_renderer;
    void Awake()
    {
        m_renderer = GetComponent<Renderer>();
        if(m_renderer == null)
            m_renderer = GetComponentInChildren<Renderer>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        m_renderer.sortingOrder = (int)(SortingOrderBase - transform.position.y*10f - Offset);
    }
}
