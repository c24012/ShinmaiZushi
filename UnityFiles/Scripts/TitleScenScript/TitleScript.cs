using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    [SerializeField] Animator fadoPanelAnim;
    [SerializeField] GameObject rulePanel;

    private void Awake()
    {
        rulePanel.SetActive(false);
    }
    public void N()
    {
        fadoPanelAnim.SetTrigger("CloseTrigger");
        Invoke(nameof(LoadMainGameScene), 0.5f);
    }

    public void TutorialScene()
    {
        fadoPanelAnim.SetTrigger("CloseTrigger");
        Invoke(nameof(LoadTutorialScene), 0.5f);
    }

    void LoadMainGameScene()
    {
        SceneManager.LoadScene("GameMain");
    }
    
    void LoadTutorialScene()
    {
        SceneManager.LoadScene("TutorialScene");
    }

    public void RulePanelOpenButton()
    {
        rulePanel.SetActive(true);
    }

    public void RulePanelCloseButton()
    {
        rulePanel.SetActive(false);
    }
}
