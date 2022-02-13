using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class WallExpand : MonoBehaviour
{
    [SerializeField] Transform[] Walls;
    [SerializeField] AgentManager agentManager;
    SpriteRenderer[] m_wallRenderers;
    EdgeCollider2D m_edgeCollider;
    public Vector2 Size;
    
    void Awake() {
        m_edgeCollider = GetComponent<EdgeCollider2D>();
        
        if(Walls.Length != 4){
            Debug.LogError("Wrong Wall Count");
            return;
        }

        m_wallRenderers = new SpriteRenderer[4];
        for(int i = 0;i < 4;i++){
            m_wallRenderers[i] = Walls[i].GetComponent<SpriteRenderer>();
        }
    }

    public void UpdateSize(float x,float y, bool animate = true){
        float hx = x/2, hy = y/2;
        Size = new Vector2(x, y);

        if (animate){
            Sequence DOTweenSequence = DOTween.Sequence();

            DOTweenSequence.Join(Walls[0].DOMoveY(hy+.5f,.3f));
            DOTweenSequence.Join(DOTween.To(() => m_wallRenderers[0].size, x => m_wallRenderers[0].size = x, new Vector2(x+2,1), .3f));

            DOTweenSequence.Join(Walls[1].DOMoveY(-hy-.5f,.3f));
            DOTweenSequence.Join(DOTween.To(() => m_wallRenderers[1].size, x => m_wallRenderers[1].size = x, new Vector2(x+2,1), .3f));

            DOTweenSequence.Join(Walls[2].DOMoveX(hx+.5f,.3f));
            DOTweenSequence.Join(DOTween.To(() => m_wallRenderers[2].size, x => m_wallRenderers[2].size = x, new Vector2(1,y), .3f));

            DOTweenSequence.Join(Walls[3].DOMoveX(-hx-.5f,.3f));
            DOTweenSequence.Join(DOTween.To(() => m_wallRenderers[3].size, x => m_wallRenderers[3].size = x, new Vector2(1,y), .3f));

            DOTweenSequence.Play().OnComplete(() => { agentManager.SpawnNewAgent(hx, hy); });
        } else {
            Walls[0].position = new Vector2(0,hy+.5f);
            m_wallRenderers[0].size = new Vector2(x+2,1);
            Walls[1].position = new Vector2(0,-hy-.5f);
            m_wallRenderers[1].size = new Vector2(x+2,1);
            Walls[2].position = new Vector2(hx+.5f,0);
            m_wallRenderers[2].size = new Vector2(1,y);
            Walls[3].position = new Vector2(-hx-.5f,0);
            m_wallRenderers[3].size = new Vector2(1,y);
        }
        
        m_edgeCollider.SetPoints(new List<Vector2>(){
            new Vector2(hx,hy),
            new Vector2(hx,-hy),
            new Vector2(-hx,-hy),
            new Vector2(-hx,hy),
            new Vector2(hx,hy),
        });

        
    }

}
