using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDifficulity : MonoBehaviour
{
    public static int gameDifficulty = 0;

    [SerializeField] TextMeshProUGUI difficulityText;
    [SerializeField] string[] difficulityName; 
    public void RightButton()
    {
        if(gameDifficulty < 3) gameDifficulty++;
        ChangeDifficulity();
    }
    public void LeftButton()
    {
        if(gameDifficulty > 0) gameDifficulty--;
        ChangeDifficulity();
    }

    void ChangeDifficulity()
    {
        difficulityText.text = difficulityName[gameDifficulty];
    }
}
