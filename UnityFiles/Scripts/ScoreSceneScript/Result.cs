using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Result : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] TextMeshProUGUI[] countTexts;
    [SerializeField] TextMeshProUGUI[] maneyTexts;
    [SerializeField] TextMeshProUGUI difficulityBornasText;
    [SerializeField] TextMeshProUGUI resultManey;

    [SerializeField, Header("皿ごとの値段")] int[] priceDishes;
    [SerializeField, Header("廃棄の単価")] int discardPrice;
    [SerializeField, Tooltip("Claim数に対したお金の増減")] int bonus, reduce;
    [SerializeField, Header("難易度ボーナス倍率")] float[] difficulityBornas = new float[4];
    [SerializeField] int[] scoreEvaluation = new int[3];
    float earnings;//ウリアゲー

    [SerializeField] GameObject stanp;
    [SerializeField] Image stanpImage;
    [SerializeField] Sprite[] stanpSprite;
    [SerializeField] GameObject titelButton;
    [SerializeField] float[] waitTimes;
    public int turn = 0 ;
    public int beforeTurn = 0;
    [SerializeField] ParticleSystem stampPtc;

    //sounds
    [SerializeField, Header("sounds")] AudioSource miniScoreSound;
    [SerializeField] AudioSource resultScoreSound, stampSound;

    private void Awake()
    {
        for (int i = 0; i < 6; i++)
        {
            panels[i].SetActive(false);
        }
    }

    void Start()
    {
        titelButton.SetActive(false);
        stanp.SetActive(false);

        //皿の計算
        for (int i = 0; i < 3; i++)
        {
            countTexts[i].text = ScoreData.dishCounts[i] + "枚";
            maneyTexts[i].text = priceDishes[i] * ScoreData.dishCounts[i] + "円";
            earnings += priceDishes[i] * ScoreData.dishCounts[i];
        }

        //廃棄計算
        countTexts[3].text = ScoreData.discardCount + "個";
        maneyTexts[3].text = -discardPrice * ScoreData.discardCount + "円";
        earnings -= ScoreData.discardCount * discardPrice;

        //クレーム計算
        countTexts[4].text = 5 - ScoreData.claimCount + "個";
        if (ScoreData.claimCount == 0)
        {
            maneyTexts[4].text = bonus + "円";
            earnings += bonus;
        }
        else 
        {
            maneyTexts[4].text = -ScoreData.claimCount * reduce + "円";
            earnings -= ScoreData.claimCount * reduce;
        }

        //難易度ボーナス計算
        if(earnings > 0)
        {
            difficulityBornasText.text = "難易度ボーナス\n" + difficulityBornas[GameDifficulity.gameDifficulty].ToString("f1") + "倍";
            earnings *= difficulityBornas[GameDifficulity.gameDifficulty];
        }

        //売り上げ計算
        resultManey.text = "売上金額" + ((int)earnings) + "円";

        //評価スタンプ選択
        if(earnings < scoreEvaluation[0])
        {
            stanpImage.sprite = stanpSprite[0];
        }
        else if(earnings >= scoreEvaluation[0] && earnings < scoreEvaluation[1])
        {
            stanpImage.sprite = stanpSprite[1];
        }
        else if(earnings >= scoreEvaluation[1])
        {
            stanpImage.sprite = stanpSprite[2];
        }
        else
        {
            Debug.LogError("評価エラー");
        }

        StartCoroutine( WaitTime(turn, 1f));
        GameStart.scoreList.Add(((int)earnings));

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            turn++;
        }


        if (beforeTurn != turn)
        {
            beforeTurn = turn;
            
            if(turn < 7)
            {
                if(turn < 5)
                {
                    StartCoroutine(WaitTime(turn, waitTimes[0]));
                    miniScoreSound.Play();
                }  
                else if(turn < 6)
                {
                    StartCoroutine(WaitTime(turn, waitTimes[1]));
                    miniScoreSound.Play();
                }  
                else
                {
                    StartCoroutine(WaitTime(turn, waitTimes[2]));
                    resultScoreSound.Play();
                }
                for (int i = 0; i < 6; i++)
                {
                    if (turn == i + 1)
                    {
                        panels[i].SetActive(true);
                    }
                }
            }
            else if(turn == 7)
            {
                StartCoroutine(WaitTime(turn, waitTimes[3]));
                stampPtc.Play();
                stampSound.Play();
                stanp.SetActive(true);
            }
            else if(turn == 8)
            {
                titelButton.SetActive(true);
            }
        }
    }

    IEnumerator WaitTime(int turnNum,float time)
    {
        yield return new WaitForSeconds(time);
        if(turn == turnNum)
        {
            turn = turnNum + 1;
        }
    }
}
