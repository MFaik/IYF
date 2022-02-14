using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] GameObject MoreSettingsPanel;
    public void LoadGameScene(){
        SceneManager.LoadScene(2);
    }

    public void OpenSettingsPanel(){
        MoreSettingsPanel.SetActive(!MoreSettingsPanel.active);
    }
}
