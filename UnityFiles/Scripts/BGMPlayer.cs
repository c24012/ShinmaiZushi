using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioSource testBGM;
    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("BGM").Length >= 2)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(testBGM);
        }
    }
    
}
