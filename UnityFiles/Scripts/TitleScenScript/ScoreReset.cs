using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreReset : MonoBehaviour
{
    public RankingScript rankingScript;
    [SerializeField] GameObject resetCheck;
    [SerializeField] GameObject okPanel;
    private void Awake()
    {
        resetCheck.SetActive(false);
        okPanel.SetActive(false);
    }
    public void ClearButton()
    {
        GameStart.scoreList.Clear();
        rankingScript.ResetPrefs();
        rankingScript.ChangeHighScore();
        resetCheck.SetActive(false);
        StartCoroutine(PanelOK());
        
    }
    public void ResetButoon()
    {
        resetCheck.SetActive(true);
    }
    public void OutButton()
    {
        resetCheck.SetActive(false);
    }
    IEnumerator PanelOK()
    {
        okPanel.SetActive(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        okPanel.SetActive(false);
    }
}
