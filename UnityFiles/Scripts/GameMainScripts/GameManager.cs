using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

class FrameRateManager : MonoBehaviour
{
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}

public class ScoreData
{
    //各皿の枚数カウント
    public static int[] dishCounts = new int[3] {0,0,0};
    //廃棄食材カウント
    public static int discardCount = 0;
    //クレームカウント
    public static int claimCount = 0;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI centerText;
    [SerializeField] GameObject fadePanel;
    [SerializeField] Animator fadePanelAnim;
    [SerializeField] AudioSource startCountSound, startSound;
    public bool gameStart = false;
    public bool isOpenManual;
    public int claimLife = 5;
    [SerializeField] int[] setingClaimLifes = new int[4];
    [SerializeField] Image[] claimLifeImages = new Image[5];

    //ManualOpenButton,ManualCloseButton//
    [SerializeField] GameObject manualPanel;
    [SerializeField] GameObject manualOpenButton;

    //TimerCount//
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Image timerImage;
    [SerializeField,Header("制限時間(秒)")] int timeLimit;
    float timer;
    int timeRemaining;

    //CustomersClaim//
    [SerializeField] Animator[] claimLifeAnim;

    //DishCountText//
    [SerializeField] TextMeshProUGUI[] dishCountTexts = new TextMeshProUGUI[3];

    //DamageEffect//
    [SerializeField] Image damegePanel;
    [SerializeField] Color damegeColor;

    //デバッグ用//
    [SerializeField, Header("デバッグ用")] bool dontFarstCount;

    private void Awake()
    {
        //UI初期化
        centerText.text = "";
        manualPanel.SetActive(false);
        timerText.text = timeLimit　+ "s";
        timeRemaining = timeLimit;
        fadePanel.SetActive(true);


        //scoreData初期化
        ScoreData.dishCounts = new int[3] { 0, 0, 0 };
        ScoreData.discardCount = 0;
        ScoreData.claimCount = 0;

        //Life難易度ごとに指定
        claimLife = setingClaimLifes[GameDifficulity.gameDifficulty];
        for(int i = 4; i >= claimLife; i--)
        {
            claimLifeAnim[i].enabled = false;
            claimLifeImages[i].enabled = false;
        }

    }
    void Start()
    {
        if (!dontFarstCount)
        {
            StartCoroutine(StartCount());
        }
        else
        {
            gameStart = true;
        }
    }

    void Update()
    {
        //ゲームスタートでタイマーを起動
        if (gameStart)
        {
            TimerCount();
        }

        //クレームライフがなくなったら強制終了
        if(claimLife <= 0)
        {
            claimLife = 0;
            StartCoroutine(GameFinish());
        }

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
        for (int i= 3; i > 0; i--)
        {
            centerText.text = i.ToString();
            startCountSound.Play();
            StartCoroutine(FadeText(centerText, 0.5f));
            yield return new WaitForSeconds(1f);
        }
        startSound.Play();
        centerText.text = "開店";
        gameStart = true;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeText(centerText, 0.5f));
        yield return new WaitForSeconds(1f);
        centerText.text = "";
        
    }

    void TimerCount()
    {
        if (timeRemaining >= 1)
        {
            timer += Time.deltaTime;
            timeRemaining = timeLimit - (int)timer;
            timerImage.fillAmount = (float)timeRemaining / timeLimit;
            timerText.text = timeRemaining + "s";
        }
        else
        {
            timeRemaining = 0;
            timerImage.fillAmount = 0;
            timerText.text = timeRemaining + "s";
            StartCoroutine(GameFinish());
        }
        
    }


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
        claimLife--;
        ScoreData.claimCount++;

        claimLifeAnim[claimLife].SetTrigger("DestroyTrigger");
        StartCoroutine(DamageEffect());
    }
    
    public void ComboRecovery()
    {
        if(claimLife < setingClaimLifes[GameDifficulity.gameDifficulty])
        {
            claimLife++;
            ScoreData.claimCount--;
            damegePanel.enabled = true;
            claimLifeAnim[claimLife].SetTrigger("RecoveryTrigger");
        }
        
    }


    IEnumerator GameFinish()
    {
        gameStart = false;
        centerText.text = "閉店";
        fadePanelAnim.SetTrigger("FinishTrigger");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("ScoreScene");
    }

    public void DishCountText()
    {
        for(int i= 0; i < 3; i++)
        {
            dishCountTexts[i].text = ":" + ScoreData.dishCounts[i];
        }
    }



    //テキストフェードアウト
    IEnumerator FadeText(TextMeshProUGUI text ,float waitTime)//text型とfloat型の時間指定
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
        for(int i = 0; i < 20; i++)
        {
            yield return wait;
            color.a = (20 - i) / 40f;
            damegePanel.color = color;
        }
        damegePanel.enabled = false;
    }
}
