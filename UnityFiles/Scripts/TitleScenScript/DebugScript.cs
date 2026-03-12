using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScript : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            for(int i = 0; i < 5; i++)
            {
                Debug.Log(GameStart.scoreList[i]);
            }
            
        }
    }
}
