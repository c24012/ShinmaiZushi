using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] TextMeshProUGUI[] orderText;
    [SerializeField] GameObject[] orderTextPanel;
    [SerializeField] TextMeshProUGUI[] commentText;
    [SerializeField] GameObject[] commentPanel;
    [SerializeField] Image[] orderGagePanel;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] Animator[] orderPanelsAnim;

    //sounds
    [SerializeField,Header("sounds")] AudioSource newOrderSound;
    [SerializeField] AudioSource clearOrderSound,mistakeOrderSound;

    [SerializeField,Header("ネタ登録")] string[] allNetaId;
    [SerializeField, Header("メニュー登録")] string[] allOrderMenuId;
    [SerializeField, Header("メニュー解答")] List<string> answerNetaIdList = new();
    [SerializeField, Header("皿解答")] List<int> answerDishIdList = new();

    [SerializeField,Tooltip("0:easy,1:normal,2:hard,3:shachiku"),Header("難易度調整")] float[] orderTimeLimit;
    [SerializeField, Tooltip("0:easy,1:normal,2:hard,3:shachiku")] float[] addOrderSpan;
    int orderCombo = 0;
    [SerializeField, Tooltip("0:easy,1:normal,2:hard,3:shachiku")] float[] comboRaite_timeLimit;
    [SerializeField, Tooltip("0:easy,1:normal,2:hard,3:shachiku")] float[] comboRaite_span;
    [SerializeField, Tooltip("0:握り,1:炙り,2:軍艦,3:その他")] float[] eachTimeLimit;

    //Updete//
    bool farstTrigger = false;
    float addOrderSpanTimer;
    

    //NewOrder//
    [NonSerialized] public List<int> orderList = new();
    int noneBoost = 1;
    int allOderCount;

    //OrderTimeLimit//
    List<float> timerList = new();
    List<bool> timerFlag = new();
 

    //ascertainOrder//
    string comment;
    

    private void Awake()
    {
        for(int i = 0; i < 5; i++)
        {
            orderText[i].text = "";
            commentText[i].text = "";
            commentPanel[i].SetActive(false);
            orderTextPanel[i].SetActive(false);
        }
        comboText.text = "Combo:0";
    }

    void Start()
    {

    }

    void Update()
    {
        //新規注文
        if (gameManager.gameStart && !farstTrigger)
        {
            farstTrigger = true;
            Invoke(nameof(NewOrder), 0.5f);
        }
        if (gameManager.gameStart)
        {
            addOrderSpanTimer += Time.deltaTime * (1 + (orderCombo * comboRaite_span[GameDifficulity.gameDifficulty])) * noneBoost;
            if (addOrderSpanTimer > addOrderSpan[GameDifficulity.gameDifficulty])
            {
                NewOrder();
                addOrderSpanTimer = 0;
            }
        }
        //注文がなかったら新規注文タイムを加速
        if(orderList.Count == 0)
        {
            noneBoost = 20;
        }
        else
        {
            noneBoost = 1;
        }

        //各タイマーをカウント
        if (gameManager.gameStart)
        {
            for (int i = 0; i < timerList.Count; i++)
            {
                if (timerFlag[i])
                {
                    timerList[i] -= Time.deltaTime * (1 + (orderCombo * comboRaite_timeLimit[GameDifficulity.gameDifficulty]));
                    CheckOrderTimeLimit(i);
                }
                orderGagePanel[i].fillAmount = timerList[i] / orderTimeLimit[GameDifficulity.gameDifficulty];
            }
        }

        //comboText更新
        comboText.text = "Combo:" + orderCombo;        
    }

    void NewOrder()
    {
        if(orderList.Count < 5)
        {
            int sumpleOrder = 0;
            if(allOderCount < 5)
            {
                bool isCountains_2 = true;
                while (isCountains_2)
                {
                    sumpleOrder = UnityEngine.Random.Range(0, 8);
                    orderList.Add(sumpleOrder);
                    if (orderList.Contains(2)) orderList.Remove(2);
                    else isCountains_2 = false;
                }
            }
            else
            {
                sumpleOrder = UnityEngine.Random.Range(0, allOrderMenuId.Length);
                orderList.Add(sumpleOrder);
            }

            float timeLimitRaite;
            if (sumpleOrder < 9) timeLimitRaite = eachTimeLimit[0];
            else if (sumpleOrder < 12) timeLimitRaite = eachTimeLimit[2];
            else if (sumpleOrder < 18) timeLimitRaite = eachTimeLimit[1];
            else timeLimitRaite = eachTimeLimit[3];

            timerList.Add(orderTimeLimit[GameDifficulity.gameDifficulty] * timeLimitRaite);
            timerFlag.Add(true);
            newOrderSound.Play();
            allOderCount++;
        }
        AddOrderPanel(orderList.Count -1);
    }

    void ClearOrder(int orderIndex)
    {
        orderList.RemoveAt(orderIndex);
        timerList.RemoveAt(orderIndex);
        timerFlag.RemoveAt(orderIndex);
        ClearOrderPanel();
    }

    void ClearOrderPanel()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i < orderList.Count)
            {
                orderText[i].text = allOrderMenuId[orderList[i]];
            }
            else
            {
                orderTextPanel[i].SetActive(false);
                orderText[i].text = "";
            }
        }
    }

    void AddOrderPanel(int index)
    {
        orderText[index].text = allOrderMenuId[orderList[index]];
        orderTextPanel[index].SetActive(true);
        orderPanelsAnim[index].SetTrigger("AddTrigger");
    }

    void CheckOrderTimeLimit(int indexNum)
    {
        if (timerList[indexNum] <= 0)
        {
            timerFlag[indexNum] = false;
            MistakeOrder();
            StartCoroutine(CustomerComment("遅すぎる！", indexNum));
        }
    }

    void AddCombo()
    {
        orderCombo++;
        clearOrderSound.Play();
        if (orderCombo % 10 == 0)
        {
            gameManager.ComboRecovery();
        }
    }

    void MistakeOrder()
    {
        gameManager.CustomersClaim();
        mistakeOrderSound.Play();
        orderCombo = 0;
    }

    public void AscertainOrder(string onNetaId, int dishNum, int searLevel)
    {
        ScoreData.dishCounts[dishNum]++;
        gameManager.DishCountText();
        int findNum = 0;
        for (int i = orderList.Count - 1; i >= 0; i--)
        {
            if (onNetaId == answerNetaIdList[orderList[i]])
            {
                findNum = i;
            }
        }

        if (onNetaId == answerNetaIdList[orderList[findNum]])
        {
            if (searLevel == 0)
            {
                ascertainDish(dishNum, findNum);
            }
            else if (searLevel == 1)
            {
                comment = "炙り足りない！";
                MistakeOrder();
            }
            else if (searLevel == 2)
            {
                ascertainDish(dishNum, findNum);
            }
            else if (searLevel == 3)
            {
                comment = "炙りすぎ！";
                MistakeOrder();
            }
        }
        else if (onNetaId == null)
        {
            comment = "何ものってない！";
            MistakeOrder();
        }
        else
        {
            comment = "注文と違う！";
            MistakeOrder();
        }
        StartCoroutine(CustomerComment(comment, findNum));
    }

    void ascertainDish(int dishNum,int findNum)
    {
        if (dishNum == answerDishIdList[orderList[findNum]])
        {
            comment = "ありがとう！";
            AddCombo();
        }
        else if (dishNum < answerDishIdList[orderList[findNum]])
        {
            comment = "ラッキー！";
            AddCombo();
        }
        else if (dishNum > answerDishIdList[orderList[findNum]])
        {
            comment = "値段が高い！";
            MistakeOrder();
        }
        else
        {
            Debug.LogError("皿の条件エラー");
        }
    }


    IEnumerator CustomerComment(string comment,int findNum)
    {
        timerFlag[findNum] = false;
        commentText[findNum].text = comment;
        commentPanel[findNum].SetActive(true);
        yield return new WaitForSeconds(1f);
        commentText[findNum].text = "";
        commentPanel[findNum].SetActive(false);
        ClearOrder(findNum);
    }
}


