using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManualController : MonoBehaviour
{
    //GetComponent
    [SerializeField] Image manualToc;
    [SerializeField] Image manualImagePanel;
    [SerializeField] Sprite[] manualImages;
    [SerializeField] Sprite[] manualPages = new Sprite[3];
    [SerializeField] AudioSource[] pageSound;

    //inspecterí≤êÆêîíl
    public int pageNum;
    [SerializeField,Header("ç≈ëÂÉyÅ[ÉWêî")] int maxPage;

    //Update//
    [SerializeField] Button manualOpenButton;

    //ChangePageSprite//
    [SerializeField,Tooltip("‡tÇËéûä‘éÊìæóp")] GameObject[] searNetaPrefabs;
    [SerializeField] TextMeshProUGUI[] searTime;
    [SerializeField] TextMeshProUGUI pageCountText;
    [SerializeField] GameObject ContentsManuButtons;

    private void Awake()
    {
        manualToc.sprite = manualPages[0];
        ContentsManuButtons.SetActive(false);
    }

    private void Update()
    {

    }

    public void NextPageButton()
    {
        if(pageNum < maxPage)
        {
            pageNum++;
            ChangePageSprite();
        }
    }

    public void PreviousPageButton()
    {
        if (pageNum > 0)
        {
            pageNum--;
            ChangePageSprite();
        }
    }
    
    public void ContentsManuPageButton()
    {
            pageNum = 0;
            ChangePageSprite();
    }

    public void ChangePageSprite()
    {
        pageSound[Random.Range(0, pageSound.Length)].Play();
        if (pageNum == 0)
        {
            manualToc.sprite = manualPages[0];
            manualImagePanel.enabled = false;
            ContentsManuButtons.SetActive(true);
        }
        else
        {
            ContentsManuButtons.SetActive(false);
        }

        if(pageNum == 6)
        {
            for(int i = 0;i < 3; i++)
            {
                searTime[i].text = "‡tÇË" + searNetaPrefabs[i].GetComponent<NetaController>().searTime[1] + "ïb";
                searTime[i].enabled = true;
            }
        }
        else if(pageNum == 7)
        {
            for(int i = 0;i < 3; i++)
            {
                searTime[i].text = "‡tÇË" + searNetaPrefabs[i + 3].GetComponent<NetaController>().searTime[1] + "ïb";
                searTime[i].enabled = true;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                searTime[i].enabled = false;
            }
        }

        if(pageNum % 2 == 0 && pageNum != 0)
        {
            manualToc.sprite = manualPages[2];
            manualImagePanel.enabled = true;
            manualImagePanel.sprite = manualImages[pageNum];
        }
        else if(pageNum % 2 != 0)
        {
            manualToc.sprite = manualPages[1];
            manualImagePanel.enabled = true;
            manualImagePanel.sprite = manualImages[pageNum];
        }

        pageCountText.text = pageNum + "/" + maxPage;

    }
}
