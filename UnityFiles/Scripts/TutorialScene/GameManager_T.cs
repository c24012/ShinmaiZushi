using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class FrameRateManager_T : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
public class GameManager_T : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI centerText;
    [SerializeField] GameObject fadePanel;
    [SerializeField] Animator fadePanelAnim;
    [SerializeField] AudioSource startCountSound, startSound;
    public bool isOpenManual;
    public int claimLife = 5;

    //ManualOpenButton,ManualCloseButton//
    [SerializeField] GameObject manualPanel;
    [SerializeField] GameObject manualOpenButton;

    //DamageEffect//
    [SerializeField] Image damegePanel;
    [SerializeField] Color damegeColor;

    private void Awake()
    {
        //UI初期化
        centerText.text = "";
        manualPanel.SetActive(false);
        fadePanel.SetActive(true);
    }

    private void Start()
    {
        StartCoroutine(StartCount());
    }

    private void Update()
    {
        //escでタイトルへ戻る
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    IEnumerator StartCount()
    {
        fadePanelAnim.SetTrigger("StartTrigger");
        yield return new WaitForSeconds(1);        
        startSound.Play();
        centerText.text = "研修開始";
        yield return new WaitForSeconds(1);
        StartCoroutine(FadeText(centerText, 0.5f));
    }

    //マニュアル表示・非表示
    public void ManualOpenButton()
    {
        manualPanel.SetActive(true);
        manualOpenButton.SetActive(false);
        isOpenManual = true;
    }

    public void ManualCloseButton()
    {
        manualPanel.SetActive(false);
        manualOpenButton.SetActive(true);
        isOpenManual = false;
    }

    public void CustomersClaim()
    {
        StartCoroutine(DamageEffect());
    }

    //テキストフェードアウト
    IEnumerator FadeText(TextMeshProUGUI text, float waitTime)//text型とfloat型の時間指定
    {
        WaitForSeconds wait = new WaitForSeconds(waitTime / 10);
        Color fadeColor = text.color;
        for (int i = 0; i < 10; i++)
        {
            fadeColor.a = 1 - (0.1f * i);
            text.color = fadeColor;
            yield return wait;
        }
        text.text = "";
        fadeColor.a = 1;
        text.color = fadeColor;
    }

    IEnumerator DamageEffect()
    {
        WaitForSeconds wait = new(0.01f);
        damegePanel.color = damegeColor;
        damegePanel.enabled = true;
        Color color = damegePanel.color;
        for (int i = 0; i < 20; i++)
        {
            yield return wait;
            color.a = (20 - i) / 40f;
            damegePanel.color = color;
        }
        damegePanel.enabled = false;
    }
}
