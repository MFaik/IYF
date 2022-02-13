using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class EndAnimation : MonoBehaviour
{
    [SerializeField] Transform CircleMask;
    [SerializeField] Vector2 EndCircleSize = new Vector2(1,1);
    [SerializeField] GameObject HearEffects;
    [SerializeField] Image RestartButton;
    Color m_restartButtonColor;
    [SerializeField] TextMeshProUGUI RestartText;
    Camera m_mainCamera;
    void Start() {
        m_mainCamera = Camera.main;

        m_restartButtonColor = RestartButton.color;
        m_restartButtonColor.a = 0;
        RestartButton.color = m_restartButtonColor;
        m_restartButtonColor.a = 1;

        RestartText.alpha = 0;
        
        StartEndAnimation();
    }

    void StartEndAnimation(){
        m_mainCamera.transform.DOMove(transform.position+new Vector3(0,0,-5),2f);
        m_mainCamera.DOOrthoSize(5,2f);
        CircleMask.DOScale(EndCircleSize,2f).SetEase(Ease.InCubic).OnComplete(()=>{
            Instantiate(HearEffects,transform.position,Quaternion.identity,transform);
            DOTween.To(()=> RestartText.alpha, x=> RestartText.alpha = x, 1, 3f).SetEase(Ease.OutCubic);
            DOTween.To(()=> RestartButton.color, x=> RestartButton.color = x, m_restartButtonColor, 3f).SetEase(Ease.OutCubic);
        });
    }

    public void LoadMenuScene(){
        SceneManager.LoadScene(0);
    }
}
