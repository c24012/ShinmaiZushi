
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrderManager_T : MonoBehaviour
{
    [SerializeField] GameManager_T gameManager;
    [SerializeField] TextMeshProUGUI[] orderText;
    [SerializeField] GameObject[] orderTextPanel;
    [SerializeField] TextMeshProUGUI[] commentText;
    [SerializeField] GameObject[] commentPanel;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] Animator[] orderPanelsAnim;

    //sounds
    [SerializeField, Header("sounds")] AudioSource newOrderSound;
    [SerializeField] AudioSource clearOrderSound, mistakeOrderSound;

    [SerializeField, Header("メニュー登録")] string[] allOrderMenuId;
    [SerializeField, Header("メニュー解答")] List<string> answerNetaIdList = new();
    [SerializeField, Header("皿解答")] List<int> answerDishIdList = new();

    int orderCombo = 0;

    //NewOrder//
    [NonSerialized] public List<int> orderList = new();

    //ascertainOrder//
    string comment;


    private void Awake()
    {
        for (int i = 0; i < 4; i++)
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
        NewOrder();
    }

    void Update()
    {
        //comboText更新
        comboText.text = "Combo:" + orderCombo;
    }

    void NewOrder()
    {
        for (int i = 0; i < 4; i++)
        {
            orderList.Add(i);
            newOrderSound.Play();
            AddOrderPanel(i);
        }
    }


    void AddOrderPanel(int index)
    {
        orderText[index].text = allOrderMenuId[orderList[index]];
        orderTextPanel[index].SetActive(true);
        orderPanelsAnim[index].SetTrigger("AddTrigger");
    }

    void AddCombo()
    {
        orderCombo++;
        clearOrderSound.Play();
    }

    void MistakeOrder()
    {
        gameManager.CustomersClaim();
        mistakeOrderSound.Play();
        orderCombo = 0;
    }

    public void AscertainOrder(string onNetaId, int dishNum, int searLevel)
    {
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

    void ascertainDish(int dishNum, int findNum)
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


    IEnumerator CustomerComment(string comment, int findNum)
    {
        commentText[findNum].text = comment;
        commentPanel[findNum].SetActive(true);
        yield return new WaitForSeconds(1f);
        commentText[findNum].text = "";
        commentPanel[findNum].SetActive(false);
    }
}



