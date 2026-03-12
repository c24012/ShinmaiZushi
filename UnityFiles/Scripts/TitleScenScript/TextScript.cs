using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScript : MonoBehaviour
{
    [SerializeField] GameObject ranking;
    //Image panking;
    // Start is called before the first frame update
    void Start()
    {
        ranking.SetActive(false);// = false;
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    ranking.enabled = true;
        //}
    }

    public void OnMenu()
    {
        ranking.SetActive(true);//ranking.enabled = true;
    }
    public void OffMenu()
    {
        ranking.SetActive(false);//ranking.enabled = false;
    }
}
