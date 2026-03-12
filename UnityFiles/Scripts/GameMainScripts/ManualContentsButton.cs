using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManualContentsButton :MonoBehaviour
{
    [SerializeField] ManualController manualController;

    [SerializeField, Header("îÚÇ‘ÉyÅ[ÉW")] int junpPage;
    
    public void JunpPage()
    {
        manualController.pageNum = junpPage;
        manualController.ChangePageSprite();
    }
}
