using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameStart
{
    public static bool isGameStart = true;
    public static List<int> scoreList = new();
}
public class RankingScript : MonoBehaviour
{
    public TextMeshProUGUI[] highScore = new TextMeshProUGUI[5];
    public void Start()
    {

        if (GameStart.isGameStart)
        {
            ScoreLoad();
            GameStart.isGameStart = false;
        }
        ChangeHighScore();

    }
    

    
    void Update()
    {

    }
    public void ChangeHighScore()
    {
        GameStart.scoreList.Sort((a, b) => b - a);
        if (GameStart.scoreList.Count >= 6)
        {
            GameStart.scoreList.RemoveAt(5);
        }
        if(GameStart.scoreList.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                highScore[i].text = "--";
            }
        }
        else
        {
            for (int i = 0; i < GameStart.scoreList.Count; i++)
            {
                highScore[i].text = GameStart.scoreList[i].ToString();
                PlayerPrefs.SetInt("Rank" + i, GameStart.scoreList[i]);
            }
            for(int i = GameStart.scoreList.Count; i < 5; i++)
            {
                highScore[i].text = "--";
            }
        }
    }

    public void ResetPrefs()
    {
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetInt("Rank" + i, 0);
        }
    }

    void ScoreLoad()
    {
        for (int i = 0; i < 5; i++)
        {
            if(PlayerPrefs.GetInt("Rank" + i, 0) != 0)
            {
                GameStart.scoreList.Add(PlayerPrefs.GetInt("Rank" + i, 0));
            }
        }
    }
}
